using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    class TextTool : ITool
    {
        private Graphics graphics;
        private int width, height;
        private bool bActive, bMouseDown, bInit;

        private Point pOld, pNew;
        private SizeF tSize;

        private Color fontColor;

        public TextTool()
        {

        }

        public void Init()
        {
            graphics = SharedSettings.getActiveGraphics();
            width = SharedSettings.getCanvasWidth();
            height = SharedSettings.getCanvasHeight();
            bActive = false;
            bInit = true;
            bMouseDown = false;

            if (graphics != null)
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            }
        }

        public string GetToolIconPath()
        {
            return @"..\..\Images\text.png";
        }

        public bool isInitalized()
        {
            return bInit;
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                pOld = e.Location;
            }

        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {

        }

        public void OnMouseUp(object sender, MouseEventArgs e)
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
                                fontColor = SharedSettings.getPrimaryBrushColor();
                                break;
                            case MouseButtons.Right:
                                fontColor = SharedSettings.getSecondaryBrushColor();
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

        public Bitmap GetToolLayer()
        {
            return null;
        }

        public bool RequiresLayerData()
        {
            return false;
        }

        public void SetLayerData(Bitmap bit)
        {

        }

        public string GetToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_text");
        }

        public void UpdateInterfaceLayer()
        {
        }
    }
}