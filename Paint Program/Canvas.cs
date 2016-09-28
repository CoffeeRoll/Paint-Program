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

        private Panel p;

        private int canvasWidth, canvasHeight;

        //Width and height of Parrent
        private int maxWidth, maxHeight;

        LayerView lv;

        public Canvas(int w, int h, int pw, int ph)
        {
            InitializeComponent();

            canvasWidth = w;
            canvasHeight = h;
            maxWidth = pw;
            maxHeight = ph;

            this.Width = canvasWidth;
            this.Height = canvasHeight;
            p = new Panel();
            p.Width = canvasWidth;
            p.Height = canvasHeight;
            p.BackgroundImageLayout = ImageLayout.Tile;
            
            try {
                p.BackgroundImage = Bitmap.FromFile(@"..\..\Images\transparent_texture.jpg");
            }catch(Exception e)
            {
                p.BackColor = Color.White;
            }
            
            this.Controls.Add(p);

        }

        public void initCanvas()
        {            
            lv = new LayerView(canvasWidth, canvasHeight);
            lv.Location = new Point(maxWidth - lv.Width, maxHeight - lv.Height);
            this.Parent.Controls.Add(lv);
        }

        public void setOnClick(System.EventHandler func)
        {
            this.Click += func;
            p.Click += func;
        }

        public void updateCanvas()
        {
            Graphics g;
            Bitmap b = this.getBitmap();
            g = Graphics.FromImage(b);
            g.DrawImage(lv.getRender(), 0, 0);
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
