using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class SelectionTool : ITool
    {
        public string getToolIconPath()
        {
            return "Selection Tool";
        }

        public Bitmap getToolLayer()
        {
            throw new NotImplementedException();
        }

        public string getToolTip()
        {
            throw new NotImplementedException();
        }

        public void init(Graphics g, int w, int h, SharedSettings s)
        {
            throw new NotImplementedException();
        }

        public bool isInitalized()
        {
            throw new NotImplementedException();
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool requiresLayerData()
        {
            throw new NotImplementedException();
        }

        public void setLayerData(Bitmap bit)
        {
            throw new NotImplementedException();
        }
    }
}
