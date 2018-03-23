using System;
using System.Windows.Forms;

namespace Paint_Program
{
    public partial class ZoomControl : UserControl, ITextUpdate
    {

        double dZoomFactor;

        private double ZOOM_MIN = 1;
        private const double ZOOM_MAX = 3200;

        public ZoomControl()
        {
            dZoomFactor = 100;
            int temp = SharedSettings.getCanvasWidth();
            temp = temp < SharedSettings.getCanvasHeight() ? temp : SharedSettings.getCanvasHeight();


            if(temp > 100)
            {
                ZOOM_MIN = 1; //Prevent sub-pixel scaling
            }
            else if(temp > 10 && temp <= 100)
            {
                ZOOM_MIN = 10; //Prevent sub-pixel scaling
            }
            else
            {
                ZOOM_MIN = 100; //Prevent sub-pixel scaling
            }


            InitializeComponent();
            tbZoom.Text = "100";
            
        }

        private void tbZoom_TextChanged(object sender, EventArgs e)
        {
            try {
                bool flag = true;
                string s = tbZoom.Text;
                foreach (char c in s)
                {
                    if (!char.IsDigit(c))
                    {
                        tbZoom.Text = dZoomFactor.ToString();
                        flag = false;
                    }
                }
                if (flag)
                {
                    double temp = Double.Parse(s);
                    if (temp <= ZOOM_MIN)
                    {
                        dZoomFactor = ZOOM_MIN;
                    }
                    else if (temp > ZOOM_MAX)
                    {
                        dZoomFactor = ZOOM_MAX;
                    }
                    else
                    {
                        dZoomFactor = temp;
                    }

                    Console.WriteLine("Zoom" + ": " + dZoomFactor);
					SharedSettings.setDrawScale((float)dZoomFactor / 100.0f);
                    tbZoom.Text = dZoomFactor.ToString();
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            
        }

        public void setZoom(double d)
        {
            if (d <= ZOOM_MIN)
            {
                dZoomFactor = ZOOM_MIN;
            }
            else if (d > ZOOM_MAX)
            {
                dZoomFactor = ZOOM_MAX;
            }
            else
            {
                dZoomFactor = d;
            }
			
			SharedSettings.setDrawScale((float)dZoomFactor / 100.0f);
            tbZoom.Text = dZoomFactor.ToString();
            ((Form1)this.Parent).updateViews();
        }

        public double getZoomPercentage()
        {
            return dZoomFactor;
        }

        public double getZoomFactor()
        {
            return dZoomFactor / 100.0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void UpdateText()
        {
            lZoom.Text = SharedSettings.getGlobalString("zoom");
        }
    }
}
