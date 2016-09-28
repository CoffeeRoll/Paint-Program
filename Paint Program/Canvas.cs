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

        public Canvas(int w, int h)
        {
            InitializeComponent();
            this.Width = w;
            this.Height = h;
            p = new Panel();
            p.Width = w;
            p.Height = h;
            p.BackgroundImageLayout = ImageLayout.Tile;
            
            try {
                p.BackgroundImage = Bitmap.FromFile(@"..\..\Images\transparent_texture.jpg");
            }catch(Exception e)
            {
                p.BackColor = Color.White;
            }
            
            this.Controls.Add(p);

        }
    }
}
