using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{

    

    public partial class Canvas : UserControl
    {

        private Display p;

        private Graphics g;

        private int canvasWidth, canvasHeight;

        //Width and height of Parrent
        private int maxWidth, maxHeight;

        //width and height of the vertical and horizontal scroll bars respectivly
        private int scrollWidth, scrollHeight;

        private int tsWidth, menuHeight;

        //Index of the currently active tool
        private int iActiveTool;


        LayerView lv;
        ToolStrip ts;
        BrushSettings bs;

        private List<ITool> Tools;
        private List<ToolStripButton> ToolButtons;

        SharedSettings ss;

        public Canvas(int w, int h, int pw, int ph)
        {
            InitializeComponent();

            ss = new SharedSettings();
            Tools = new List<ITool>();
            ToolButtons = new List<ToolStripButton>();

            canvasWidth = w;
            canvasHeight = h;
            maxWidth = pw;
            maxHeight = ph;

            tsWidth = 50;
            menuHeight = 25;

            scrollWidth = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            scrollHeight = System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight;

            this.Width = canvasWidth;
            this.Height = canvasHeight;

            p = new Display();
            p.Width = canvasWidth;
            p.Height = canvasHeight;
            p.MaximumSize = new Size(canvasWidth, canvasHeight);
            p.Size = new Size(canvasWidth, canvasHeight);
            p.BackgroundImageLayout = ImageLayout.Tile;
            p.MouseDown += handleMouseDown;
            p.MouseUp += handleMouseUp;
            p.MouseMove += handleMouseMove;
            p.Paint += EDisplayPaint;
            
            
            try {
                p.BackgroundImage = Bitmap.FromFile(@"..\..\Images\transparent_texture.jpg");
            }catch(Exception e)
            {
                p.BackColor = Color.White;
            }

            g = p.CreateGraphics();

            this.Controls.Add(p);

            

        }

        public void initCanvas()
        {            
            lv = new LayerView(canvasWidth, canvasHeight);
            lv.Location = new Point(maxWidth - (lv.Width + scrollWidth), maxHeight - (lv.Height + scrollHeight));

            this.Location = new Point((maxWidth / 2) - (this.Width / 2), (maxHeight / 2) - (this.Height / 2));
            this.Parent.Controls.Add(lv);

            ts = new ToolStrip();
            ts.Dock = DockStyle.None;
            ts.Location = new Point(0, menuHeight);
            ts.AutoSize = false;
            ts.Height = maxHeight - menuHeight;
            ts.Width = tsWidth;
            ts.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            ts.ShowItemToolTips = true;

            this.Parent.Controls.Add(ts);
            this.Parent.Resize += handleParentResize;

            bs = new BrushSettings(ss);
            bs.Location = new Point(maxWidth - bs.Width, 0);
            this.Parent.Controls.Add(bs);


            initTools();
        }

        private void initTools()
        {

            Tools.Add(new PencilTool());
            Tools.Add(new BrushTool());
            Tools.Add(new ColorSamplingTool());
            Tools.Add(new ErasoirTool());

            foreach (ITool tool in Tools)
            {
                ToolStripButton temp = new ToolStripButton(Image.FromFile(tool.getToolIconPath()));
                temp.AutoSize = false;
                temp.Width = tsWidth;
                temp.Height = temp.Width;
                temp.Click += handleToolStripItemClick;
                ToolButtons.Add(temp);
                ts.Items.Add(temp);
            }

            
            
            /**/
        }

        private void handleToolStripItemClick(object sender, EventArgs e)
        {
            iActiveTool = ToolButtons.IndexOf((ToolStripButton)sender);
            Tools[iActiveTool].init(lv.getActiveLayerGraphics(), canvasWidth, canvasHeight, ss);
            Console.WriteLine(Tools[iActiveTool].getToolTip());
        }

        private void handleParentResize(object sender, EventArgs e)
        {
            //Updates Parent Width and Height Values
            maxWidth = Parent.Width;
            maxHeight = Parent.Height;
            
            //Moves all the Controls to their new location
            lv.Location = new Point(maxWidth - (lv.Width + scrollWidth), maxHeight - (lv.Height + scrollHeight));
            this.Location = new Point((maxWidth / 2) - (this.Width / 2), (maxHeight / 2) - (this.Height / 2));
            ts.Height = maxHeight - menuHeight;
            bs.Location = new Point(maxWidth - bs.Width, menuHeight);

            //Prevent controls from not redrawing
            this.Parent.Refresh();
        }

        public void handleMouseDown(object sender, MouseEventArgs e)
        {
            //If there is a selected Tool
            if (iActiveTool >= 0)
            {
                Tools[iActiveTool].init(lv.getActiveLayerGraphics(), canvasWidth, canvasHeight, ss);
                if (Tools[iActiveTool].requiresLayerData())
                {
                    Tools[iActiveTool].setLayerData(lv.getActiveLayerBitmap());
                }
            }
            if (iActiveTool >= 0)
                Tools[iActiveTool].onMouseDown(sender, e);
        }

        public void handleMouseUp(object sender, MouseEventArgs e)
        {
            if (iActiveTool >= 0)
                Tools[iActiveTool].onMouseUp(sender, e);
        }

        public void handleMouseMove(object sender, MouseEventArgs e)
        {
            if (iActiveTool >= 0)
                Tools[iActiveTool].onMouseMove(sender, e);
            updateCanvas(g);
        }

        private void EDisplayPaint(object sender, PaintEventArgs e)
        {
            updateCanvas(e.Graphics);

            lv.updateActiveLayer();
        }

        public void updateCanvas(Graphics k)
        {
            Bitmap bit = lv.getRender();
            p.Invalidate();
            k.DrawImage(bit, 0, 0);
            
        }

        public void setBitmap(Bitmap bit)
        {
            p.BackgroundImage = bit;
        }

        public Bitmap getBitmap()
        {
            return (Bitmap)p.BackgroundImage;
        }

    }
}
