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

        //Index of the currently active tool
        private int iActiveTool;

        LayerView lv;
        ToolStrip ts;

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

            scrollWidth = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            scrollHeight = System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight;

            this.Width = canvasWidth;
            this.Height = canvasHeight;

            p = new Display();
            p.Width = canvasWidth;
            p.Height = canvasHeight;
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
            ts.Dock = DockStyle.Left;
            ts.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            ts.ShowItemToolTips = true;

            this.Parent.Controls.Add(ts);
            this.Parent.Resize += handleParentResize;

            initTools();
        }

        private void initTools()
        {

            Tools.Add(new PaintBrush());

            foreach (ITool tool in Tools)
            {
                ToolStripButton temp = new ToolStripButton(Image.FromFile(tool.getToolIconPath()));
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
            maxWidth = Parent.Width;
            maxHeight = Parent.Height;
            lv.Location = new Point(maxWidth - (lv.Width + scrollWidth), maxHeight - (lv.Height + scrollHeight));
            this.Location = new Point((maxWidth / 2) - (this.Width / 2), (maxHeight / 2) - (this.Height / 2));
            
        }

        public void handleMouseDown(object sender, MouseEventArgs e)
        {
            if(iActiveTool >= 0)
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
