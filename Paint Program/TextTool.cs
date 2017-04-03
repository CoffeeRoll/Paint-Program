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

        private Color fontColor;

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
                        int fontSize = TxtSelect.FontSize;
                        string fontType = TxtSelect.FontType;

                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                fontColor = settings.getPrimaryBrushColor();
                                break;
                            case MouseButtons.Right:
                                fontColor = settings.getSecondaryBrushColor();
                                break;
                            default:
                                fontColor = Color.Black;
                                break;
                        }

                        SolidBrush brush = new SolidBrush(fontColor);

                        graphics.DrawString(text, new Font(fontType, fontSize), brush, textRect);
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

        public string getToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_text");
        }

        public void updateInterfaceLayer()
        {
        }
    }
}