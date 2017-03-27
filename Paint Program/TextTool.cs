using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class TextTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bActive, bMouseDown, bInit;

        private Point pOld, pNew;
        private SizeF tSize;

        public TextTool()
        {

        }

        public void init(SharedSettings s)
        {
            graphics = s.getActiveGraphics();
            width = s.getCanvasWidth();
            height = s.getCanvasHeight();
            settings = s;
            bActive = false;
            bInit = true;
            bMouseDown = false;

            if (graphics != null)
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            }
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\text.png";
        }

        public bool isInitalized()
        {
            return bInit;
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
            }
            
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {

        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = false;
                pNew = e.Location;

                tSize.Height = Math.Abs(pOld.X - pNew.X);
                tSize.Width = Math.Abs(pOld.Y - pNew.Y);

                RectangleF textRect = new RectangleF(pOld, tSize);

                using (TextSelect TxtSelect = new TextSelect())
                {
                    if (TxtSelect.ShowDialog() == DialogResult.OK)
                    {
                        string text = TxtSelect.UserText;

                        graphics.DrawString(text, new Font("Arial", 14), Brushes.Black, textRect);
                    }

                }


            }
        }

        public Bitmap getToolLayer()
        {
            return null;
        }

        public bool requiresLayerData()
        {
            return false;
        }

        public void setLayerData(Bitmap bit)
        {
            
        }
        
    }
}
