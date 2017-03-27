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
            }

            CreateTextBox(e);
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            //if (graphics != null && bMouseDown)
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        pNew = e.Location;
            //        graphics.DrawLine(new Pen(settings.getPrimaryBrushColor()), pOld, pNew);
            //        pOld = pNew;
            //    }
            //    else
            //    {
            //        pNew = e.Location;
            //        graphics.DrawLine(new Pen(settings.getSecondaryBrushColor()), pOld, pNew);
            //        pOld = pNew;
            //    }
            //}
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = false;
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

        public void CreateTextBox(MouseEventArgs e)
        {
            TextBox TB = new TextBox();

            TB.Font = new Font("Arial", 20);
            TB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            TB.Location = e.Location;
        }
    }
}
