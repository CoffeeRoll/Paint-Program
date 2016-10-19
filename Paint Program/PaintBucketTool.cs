//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Paint_Program
//{
//    class PaintBucketTool : ITool
//    {
//        private Graphics graphics;
//        private int width, height;
//        private SharedSettings settings;
//        private bool bActive, bMouseDown, bInit;

//        private Point pOld, pNew;

//        public PaintBucketTool()
//        {
//        }

//        public void init(Graphics g, int w, int h, SharedSettings s)
//        {
//            graphics = g;
//            width = w;
//            height = h;
//            settings = s;
//            bActive = false;
//            bInit = true;
//            bMouseDown = false;

//            if (graphics != null)
//            {
//                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
//            }
//        }

//        public string getToolIconPath()
//        {
//            return @"..\..\Images\bucket.png";
//        }

//        public string getToolTip()
//        {
//            return "Paint Bucket Tool";
//        }

//        public void onMouseDown(object sender, MouseEventArgs e)
//        {
//            if (graphics != null)
//            {
//                bMouseDown = true;
//                pOld = e.Location;
//                if (e.Button == MouseButtons.Left)
//                {
//                    Bitmap b = settings.getBitmapCurrentLayer(true);
//                }

//            }
//        }

//        public void onMouseMove(object sender, MouseEventArgs e)
//        {

//        }

//        public void onMouseUp(object sender, MouseEventArgs e)
//        {
//            if (graphics != null)
//            {
//                bMouseDown = false;

//            }
//        }

//        public bool isInitalized()
//        {
//            return bInit;
//        }

//        public Bitmap getToolLayer()
//        {
//            return null;
//        }

//        public bool requiresLayerData()
//        {
//            return false;
//        }

//        public void setLayerData(Bitmap bit)
//        {
//        }
//    }
//}
