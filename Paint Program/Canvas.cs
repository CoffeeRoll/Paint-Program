using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WintabDN;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Paint_Program
{
	/// <summary>
	/// Class handling the main rendering of the layers, and interfacing with tools
	/// </summary>
    public partial class Canvas : UserControl, ITextUpdate
    {
        private Display p;
        private Panel pScaled;

        private Graphics g;

        private Bitmap bg;

        private Bitmap Grid;

        private const int GCperFrames = 100;

        private int canvasWidth, canvasHeight;

        //Width and height of Parent
        private int maxWidth, maxHeight;

        //width and height of the vertical and horizontal scroll bars respectively
        private int scrollWidth, scrollHeight;

        private int tsWidth, menuHeight;

        //Index of the currently active tool
        private int iActiveTool;

        private bool isPasued = false;

        LayerView lv;
        ToolStrip ts;
        BrushSettings bs;
        ZoomControl zc;

        TabletInfo ti;

        private List<ITool> Tools;
        public List<ToolStripButton> ToolButtons;
        private bool ToolsShown = true;

        public Canvas(int pw, int ph)
        {
            InitializeComponent();

            canvasWidth = SharedSettings.bitmapCanvas.Width;
            canvasHeight = SharedSettings.bitmapCanvas.Height;
            maxWidth = pw;
            maxHeight = ph;

            this.Width = canvasWidth;
            this.Height = canvasHeight;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        }

        public Canvas(int w, int h, int pw, int ph)
        {
            InitializeComponent();

            canvasWidth = w;
            canvasHeight = h;
            
            maxWidth = pw;
            maxHeight = ph;

            SharedSettings.setCanvasWidth(canvasWidth);
            SharedSettings.setCanvasHeight(canvasHeight);

            this.Width = canvasWidth;
            this.Height = canvasHeight;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

		/// <summary>
		/// Initializes all main components that the canvas interfaces with
		/// </summary>
        public void InitCanvas()
        {
			SharedSettings.Init();

			Tools = new List<ITool>();
            ToolButtons = new List<ToolStripButton>();

            try
            {
                ti = new TabletInfo(HandleTabletData);
                SharedSettings.setTabletConnected(true);
                Console.WriteLine("Tablet Connected.");
            }
            catch (Exception e)
            {
                SharedSettings.setTabletConnected(false);
                Console.WriteLine(e.InnerException);
            }

            tsWidth = 50;
            menuHeight = 25;

            scrollWidth = SystemInformation.VerticalScrollBarWidth;
            scrollHeight = SystemInformation.HorizontalScrollBarHeight;

            lv = new LayerView(canvasWidth, canvasHeight);
            lv.Location = new Point(maxWidth - (lv.Width + scrollWidth), maxHeight - (lv.Height + scrollHeight));

            this.Location = new Point((maxWidth / 2) - (this.Width / 2), (maxHeight / 2) - (this.Height / 2));
            this.Parent.Controls.Add(lv);

			ts = new ToolStrip
			{
				Dock = DockStyle.None,
				Location = new Point(0, menuHeight * 2),
				AutoSize = false,
				Height = maxHeight - menuHeight,
				Width = tsWidth,
				LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow,
				BackColor = Color.FromArgb(128, 128, 128),
				GripStyle = ToolStripGripStyle.Hidden,
				ShowItemToolTips = true
			};

			this.Parent.Controls.Add(ts);
            this.Parent.Resize += HandleParentResize;

            bs = new BrushSettings();
            bs.Location = new Point(maxWidth - bs.Width, menuHeight * 2);
            this.Parent.Controls.Add(bs);

            zc = new ZoomControl();
            zc.Location = new Point(tsWidth, maxHeight - SystemInformation.CaptionHeight - menuHeight- zc.Height);
            this.Parent.Controls.Add(zc);

			p = new Display
			{
				Size = new Size(canvasWidth, canvasHeight)
			};

			//Center the canvas on the screen, but don't allow it to be draw off the screen
			int minXPos = 0;
            int maxXPos = (bs.Location.X / 2) - (p.Width / 2);
            int minYPos = menuHeight;
            int maxYPos = (zc.Location.Y / 2) - (p.Height / 2);

            int ploc_x = (maxXPos < minXPos) ? minXPos : maxXPos;
            int ploc_y = maxYPos < minYPos ? minYPos : maxYPos;
            p.Location = new Point(ploc_x, ploc_y);

            p.MouseDown += HandleMouseDown;
            p.MouseUp += HandleMouseUp;
            p.MouseMove += HandleMouseMove;
            p.Paint += EDisplayPaint;

			pScaled = new Panel
			{
				Size = new Size((lv.Location.X - Location.X) - ts.Width - 30, (zc.Location.Y - Location.Y) - 165),
				MinimumSize = new Size(canvasWidth, canvasHeight),
				Location = new Point(0, 0),
				BackColor = Color.FromArgb(64, 64, 64),
				AutoScroll = true
			};

			this.Location = new Point(ts.Width + 15, SystemInformation.CaptionHeight + 15);
            pScaled.Controls.Add(p);
            p.MouseEnter += delegate { pScaled.Focus(); pScaled.Select(); };

            bg = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format24bppRgb);
            Grid = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format24bppRgb);

            try
            {
                Bitmap bgTile = (Bitmap)Bitmap.FromFile(@"..\..\Images\transparent_texture.jpg");
                using (TextureBrush brush = new TextureBrush(bgTile, WrapMode.Tile))
                {
                    using (Graphics g = Graphics.FromImage(bg))
                    {
                        g.FillRectangle(brush, 0, 0, bg.Width, bg.Height);
                    }
                }
            }
            catch (Exception e)
            {
                Graphics.FromImage(bg).Clear(Color.White);
                p.BackColor = Color.White;
				Console.WriteLine("Tiling Error: " + e.InnerException);
            }

            g = p.CreateGraphics();
            

            this.Controls.Add(pScaled);
            this.SendToBack();

            InitTools();

            pScaled.Size = new Size((lv.Location.X - this.Location.X) - 15, (zc.Location.Y - this.Location.Y) - 15);
            this.Refresh();
        }

		/// <summary>
		/// Adds all tools to the tool bar
		/// </summary>
        private void InitTools()
        {
            Tools.Add(new PencilTool());
            Tools.Add(new BrushTool());
            Tools.Add(new StraightLineTool());
            Tools.Add(new ColorSamplingTool());
            Tools.Add(new EraserTool());
            Tools.Add(new PaintBucketTool());
            Tools.Add(new SelectionTool());
            Tools.Add(new TextTool());
            Tools.Add(new GreenScreenTool());
            Tools.Add(new MoveTool());

            foreach (ITool tool in Tools)
            {
				ToolStripButton temp = new ToolStripButton(Image.FromFile(tool.GetToolIconPath()))
				{
					ToolTipText = tool.GetToolTip(),
					AutoToolTip = false,
					AutoSize = false,
					Width = tsWidth
				};
				temp.Height = temp.Width;
                temp.Click += HandleToolStripItemClick;
                ToolButtons.Add(temp);
                ts.Items.Add(temp);

                temp.MouseEnter += delegate
                {
                    //New object on every mouse over -- not great
                    ToolTip tt = new ToolTip();
                    temp.Tag = tt;

                    //Draw the ToolTip Twice because it doesn't work with one draw -- also not great
                    ((ToolTip)temp.Tag).Show(tool.GetToolTip(), this, temp.Bounds.X + temp.Width, temp.Bounds.Y + temp.Height);
                    ((ToolTip)temp.Tag).Show(tool.GetToolTip(), this, temp.Bounds.X + temp.Width, temp.Bounds.Y + temp.Height);
                };

                temp.MouseLeave += delegate {
                    //ToolTip.Hide doesn't work apparently?
                    //Dispose the object when the mouse moves away to force it to go away -- really not great
                    ((ToolTip)temp.Tag).Dispose();
                };
            }
        }

		/// <summary>
		/// Zooms canvas in by 10%
		/// </summary>
        public void ZoomIn()
        {
            zc.setZoom(zc.getZoomPercentage() + 10);
        }

		/// <summary>
		/// Zooms canvas out by 10%
		/// </summary>
        public void ZoomOut()
        {
            zc.setZoom(zc.getZoomPercentage() - 10);
        }

		/// <summary>
		/// WintabDN event handler to interface with USB drawing tablets
		/// </summary>
		/// <param name="sender">N/A</param>
		/// <param name="e">Tablet event data</param>
        private void HandleTabletData(object sender, MessageReceivedEventArgs e)
        {
			if (SharedSettings.getTabletconnected())
			{
				CWintabData m_wtData = ti.getWintabData();
				UInt32 m_maxPkts = ti.getMaxPackets();

				if (m_wtData == null)
				{
					return;
				}

				try
				{
					if (m_maxPkts == 1)
					{
						uint pktID = (uint)e.Message.WParam;
						WintabPacket pkt = m_wtData.GetDataPacket((uint)e.Message.LParam, pktID);

						if (pkt.pkContext != 0)
						{
							int pressure = (int)pkt.pkNormalPressure;
							SharedSettings.setTabletPressure(pressure);
						}
					}
				}
				catch (Exception err)
				{
					SharedSettings.setTabletConnected(false);
					Console.WriteLine(err.InnerException);
				}
			}

        }

		/// <summary>
		/// Handles tool changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void HandleToolStripItemClick(object sender, EventArgs e)
        {
            iActiveTool = ToolButtons.IndexOf((ToolStripButton)sender);
            Tools[iActiveTool].Init();

            foreach(ToolStripButton b in ToolButtons)
            {
                b.BackColor = Color.Transparent;
            }
            ToolButtons[iActiveTool].BackColor = Color.LightGreen;
        }

		/// <summary>
		/// Updates the positions of the different components in the application
		/// </summary>
		/// <param name="sender">N/A</param>
        public void UpdatePositions(object sender)
        {
            Parent.Refresh();
            HandleParentResize(sender, null);

        }

		/// <summary>
		/// Handles window resize by adjusting the position of all components
		/// </summary>
		/// <param name="sender">The parent control of this Canvas class</param>
		/// <param name="e">N/A</param>
        private void HandleParentResize(object sender, EventArgs e)
        {
            //Fixes Second New Project Null Parent Reference Bug
            this.Parent = (Control) sender;

            //Updates Parent Width and Height Values
            maxWidth = this.Parent.Width;
            maxHeight = this.Parent.Height;

            this.Size = new Size((int)(canvasWidth * zc.getZoomFactor()), (int)(canvasHeight * zc.getZoomFactor()));
            this.Location = new Point(0,0);

            //Moves all the Controls to their new location
            lv.Location = new Point(maxWidth - (lv.Width + scrollWidth), maxHeight - (lv.Height + scrollHeight));
            ts.Height = maxHeight - menuHeight;
            bs.Location = new Point(maxWidth - bs.Width, menuHeight * 2);
            zc.Location = new Point(tsWidth, maxHeight - SystemInformation.CaptionHeight - menuHeight - zc.Height);

            //Center the canvas on the screen, but don't allow it to be draw off the screen
            int minXPos = menuHeight * 3;
            int maxXPos = (bs.Location.X / 2) - (((int)(p.Width)) / 2);
            int minYPos = menuHeight * 3;
            int maxYPos = (zc.Location.Y / 2) - (((int)(p.Height)) / 2);

            int ploc_x = (maxXPos < minXPos) ? minXPos : maxXPos;
            int ploc_y = maxYPos < minYPos ? minYPos : maxYPos;
            p.Location = new Point(ploc_x, ploc_y);

			//Correct for canvas' scroll position to prevent drift
            pScaled.Size = new Size((lv.Location.X - this.HorizontalScroll.Value), (zc.Location.Y - this.VerticalScroll.Value));

            //Prevent controls from not redrawing
            this.Parent.Refresh();
        }

		/// <summary>
		/// Scales the mouse event so that when the canvas is zoomed in the mouse is still in the correct location 
		/// </summary>
		/// <param name="e">MouseEventArgs containing the mouse event data</param>
		/// <returns></returns>
        private MouseEventArgs ScaleMouseEvent(MouseEventArgs e)
        {
            if (!SharedSettings.getActiveSelection())
            {
                if (lv.getActiveLayer().isLayerVisible())
                {
                    return new MouseEventArgs(e.Button, e.Clicks, (int)(e.X / SharedSettings.getDrawScale()), (int)(e.Y / SharedSettings.getDrawScale()), e.Delta);
                }
                else
                {
                    return null;
                }
            }
            else if (Tools[iActiveTool] is MoveTool)
            {
                return new MouseEventArgs(e.Button, e.Clicks, (int)(((e.X - SharedSettings.getSelectionPoint().X)) / SharedSettings.getDrawScale()), (int)(((e.Y - SharedSettings.getSelectionPoint().Y)) / SharedSettings.getDrawScale()), e.Delta);
            }
            else
            {
                Rectangle rect = new Rectangle(SharedSettings.getSelectionPoint(), SharedSettings.getSelectionSize());
                if ((SharedSettings.getActiveSelection() && rect.Contains(e.X, e.Y)) || Tools[iActiveTool] is SelectionTool)
                {
                    return new MouseEventArgs(e.Button, e.Clicks, (int)(((e.X - SharedSettings.getSelectionPoint().X)) / SharedSettings.getDrawScale()), (int)(((e.Y - SharedSettings.getSelectionPoint().Y)) / SharedSettings.getDrawScale()), e.Delta);
                }
                else
                {
                    return null;
                }
            }
        }

		/// <summary>
		/// Handles mouse down events and sends event data to tools
		/// </summary>
		/// <param name="sender">N/A</param>
		/// <param name="e">MouseEventArgs containing data of the mouse event</param>
        public void HandleMouseDown(object sender, MouseEventArgs e)
        {
            
            MouseEventArgs evt = ScaleMouseEvent(e);
            //If there is a selected Tool
            if (iActiveTool >= 0)
            {
                Tools[iActiveTool].Init();
            }
            if (iActiveTool >= 0 && evt != null)
            {
                Tools[iActiveTool].OnMouseDown(sender, evt);
            }
        }

		/// <summary>
		/// Handles mouse up events and sends event data to tools
		/// </summary>
		/// <param name="sender">N/A</param>
		/// <param name="e">MouseEventArgs containing data of the mouse event</param>
		public void HandleMouseUp(object sender, MouseEventArgs e)
        {
            MouseEventArgs evt = ScaleMouseEvent(e);
            if (iActiveTool >= 0 && evt != null)
                Tools[iActiveTool].OnMouseUp(sender, evt);
            lv.UpdateLayerInfoListener();
            bs.CheckChange();
        }

		/// <summary>
		/// Handles mouse move events and sends event data to tools
		/// </summary>
		/// <param name="sender">N/A</param>
		/// <param name="e">MouseEventArgs containing data of the mouse event</param>
		public void HandleMouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs evt = ScaleMouseEvent(e);

			if (iActiveTool >= 0 && evt != null)
			{
				Tools[iActiveTool].OnMouseMove(sender, evt);
			}
            UpdateCanvas(g);
            Parent.Refresh();
        }

		/// <summary>
		/// Calls UpdateGraphics()
		/// </summary>
		/// <param name="sender">Not Used</param>
		/// <param name="e">PaintEventArgs object where the Graphics property is passed to UpdateGraphics()</param>
        private void EDisplayPaint(object sender, PaintEventArgs e)
        {
            if (!isPasued)
            {
                UpdateCanvas(e.Graphics);
                lv.updateActiveLayer();
            }
        }

		/// <summary>
		/// Called periodically to render the image to the screen
		/// </summary>
		/// <param name="graphics">Graphics object of the Bitmap being rendered to</param>
        public void UpdateCanvas(Graphics graphics)
        {

            if(SharedSettings.getBitmapLayerUpdate() != null)
            {
                lv.updateActiveLayer();
            }

            SharedSettings.bitmapCanvas = lv.getRender();
            Bitmap bit2 = (Bitmap)bg.Clone();

            Graphics temp = Graphics.FromImage(bit2);

            temp.DrawImage(SharedSettings.bitmapCanvas, 0, 0);

            Bitmap iitmp = SharedSettings.getImportImage();
            if (iitmp != null)
            {
                lv.addImportImage(iitmp);
            }

            p.Invalidate();

            System.GC.Collect();

            p.Width = (int) (SharedSettings.getDrawScale() * SharedSettings.getCanvasWidth());
            p.Height = (int) (SharedSettings.getDrawScale() * SharedSettings.getCanvasHeight());
            Rectangle source = new Rectangle(0, 0, bit2.Width, bit2.Height);
            Rectangle dest = new Rectangle(0, 0, p.Width, p.Height);

            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;

            if (SharedSettings.getRenderBitmapInterface() && SharedSettings.getInterfaceBitmap() != null)
            {
                Tools[6].UpdateInterfaceLayer();
                temp.DrawImage(SharedSettings.getInterfaceBitmap(), 0, 0);
            }
            
            if (SharedSettings.getActiveSelection() && SharedSettings.getBitmapSelectionArea() != null)
            {
                temp.DrawImage(SharedSettings.getBitmapSelectionArea(), SharedSettings.getSelectionPoint().X, SharedSettings.getSelectionPoint().Y);
                
            }

            if (SharedSettings.getFlattenSelection())
            {
                SharedSettings.setFlattenSelection(false);
                lv.updateActiveLayerSettings();
                foreach (ITool t in Tools)
                {
                    if (t is SelectionTool)
                    {
                        ((SelectionTool)t).Clean();
                    }
                }
            }

            HandleWatermark(temp);

            graphics.DrawImage(bit2, dest, source, GraphicsUnit.Pixel);
            if (SharedSettings.getGridToggle())
            {
                lv.GridDraw(graphics);
            }

            bit2.Dispose();
            if(iitmp != null)
                iitmp.Dispose();
        }

		/// <summary>
		/// Handles rendering the watermark to a Bitmap using the provided Graphics object
		/// </summary>
		/// <param name="graphics">Graphics object used to render the watermark</param>
        public static void HandleWatermark(Graphics graphics)
        {
            if (SharedSettings.bRenderWatermark && SharedSettings.bitmapWatermark != null)
            {
                if (SharedSettings.watermarkStyle == "Tiled")
                {
                    try
                    {
                        Bitmap bgTile = (Bitmap)Image.FromFile(SharedSettings.watermarkPath);
                        using (TextureBrush brush = new TextureBrush(bgTile, WrapMode.Tile))
                        {
                            using (Graphics g = graphics)
                            {
                                g.FillRectangle(brush, 0, 0, SharedSettings.bitmapCanvas.Width, SharedSettings.bitmapCanvas.Height);
                            }
                        }
                    }
                    catch (Exception e)
                    {
						Console.WriteLine("Tiling Watermark Error: " + e.InnerException);
                    }
                }
                if (SharedSettings.watermarkStyle == "Single Center")
                {
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Rectangle src = new Rectangle(0, 0, SharedSettings.bitmapWatermark.Width, SharedSettings.bitmapWatermark.Height);
                    Rectangle dst = new Rectangle(0, 0, SharedSettings.bitmapCanvas.Width, SharedSettings.bitmapCanvas.Height);
                    graphics.DrawImage(SharedSettings.bitmapWatermark, dst, src, GraphicsUnit.Pixel);
                }
                if (SharedSettings.watermarkStyle == "Single Bottom")
                {
                    Rectangle src = new Rectangle(0, 0, SharedSettings.bitmapWatermark.Width, SharedSettings.bitmapWatermark.Height);
                    Rectangle dst = new Rectangle(SharedSettings.bitmapCanvas.Width - SharedSettings.bitmapWatermark.Width,
                        SharedSettings.bitmapCanvas.Height - SharedSettings.bitmapWatermark.Height,
                        SharedSettings.bitmapWatermark.Width,
                        SharedSettings.bitmapWatermark.Height);
                    graphics.DrawImage(SharedSettings.bitmapWatermark, dst, src, GraphicsUnit.Pixel);
                }
            }
        }

		/// <summary>
		/// Sets the pause flag to start or stop rendering of the main canvas
		/// </summary>
		/// <param name="b">
		/// Boolean value, true stops canvas rendering, false continues rendering
		/// </param>
        public void SetPause(bool b)
        {
            isPasued = b;
        }
		
		/// <summary>
		/// Renders all layers to a new Bitmap
		/// </summary>
		/// <returns>Bitmap containing a render of all the layer data</returns>
        public Bitmap GetBitmap()
        {
            //Return the image the user has been working on
            return lv.getRender();
        }

		/// <summary>
		/// Displays the tool strip
		/// </summary>
        public void ShowTools()
        {
            if (!ToolsShown)
            {
                ts.Visible = true;
                
            }
            ToolsShown = true;
            Parent.Refresh();
        }

		/// <summary>
		/// Hides the tool strip
		/// </summary>
        public void HideTools()
        {
            if (ToolsShown)
            {
                ts.Visible = false;
            }

            ToolsShown = false;
        }

		/// <summary>
		/// Calls the Update Text method for all classes that implement ITextUpdate 
		/// </summary>
        public void UpdateText()
        {
            foreach (Control c in Controls)
            {
                if(c is ITextUpdate)
                {
                    ((ITextUpdate) c).UpdateText();
                }
            }
        }

		/// <summary>
		/// Disposes all resources
		/// </summary>
        public void Trash()
        {
            lv.Trash();
			SharedSettings.Trash();
            p.Dispose();
            pScaled.Dispose();
            g.Dispose();
            Tools.Clear();
            this.Dispose();
        }
    }
}
