using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    public partial class BrushSelector : UserControl
    {

        List<Bitmap> bips;

        public BrushSelector()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.Height = 50;
            populateBrushes();
            
        }

        private void populateBrushes()
        {
            string[] brushes = Directory.GetFiles(@"..\..\Brushes");
            if (brushes.Count() > 0)
            {
                bips = new List<Bitmap>();
                for (int t = 0; t < brushes.Count(); t++)
                {
                    Console.WriteLine(brushes[t]);
                    bips.Add((Bitmap)Image.FromFile(brushes[t]));
                    ToolStripMenuItem temp = new ToolStripMenuItem();
                    temp.AutoSize = true;
                    temp.ImageScaling = ToolStripItemImageScaling.None;
                    temp.Image = (Bitmap)bips.ElementAt(t).Clone();
                    temp.Height = 45;
                    temp.Width = 45;
                    temp.Click += delegate
                    {
                        SharedSettings.bitmapBrushTexture = (Bitmap) (temp.Image);
                    };
                    tsBrushes.ImageScalingSize = new Size(45, 45);
                    tsBrushes.Items.Add(temp);
                }
                tsBrushes.Refresh();
                SharedSettings.bitmapBrushTexture = (Bitmap)bips.ElementAt(0).Clone();
                bips.Clear();
                brushes = null;
                
            }
        }

        public void Trash()
        {

        }
    }
}
