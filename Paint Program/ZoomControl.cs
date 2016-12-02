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
    public partial class ZoomControl : UserControl
    {

        double dZoomFactor;

        private double ZOOM_MIN = 1;
        private const double ZOOM_MAX = 3200;
        private SharedSettings settings;

        public ZoomControl(SharedSettings ss)
        {
            dZoomFactor = 100;
            settings = ss;
            int temp = ss.getCanvasWidth();
            temp = temp < ss.getCanvasHeight() ? temp : ss.getCanvasHeight();


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
                string s = tbZoom.Text;
                double temp = Double.Parse(s);
                if(temp <= ZOOM_MIN)
                {
                    dZoomFactor = ZOOM_MIN;
                }
                else if(temp > ZOOM_MAX)
                {
                    dZoomFactor = ZOOM_MAX;
                }
                else
                {
                    dZoomFactor = temp;
                }
                
                Console.WriteLine("Zoom: " + dZoomFactor);
                settings.setDrawScale((float)dZoomFactor/100.0f);
            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            
        }

        public double getZoomPercentage()
        {
            return dZoomFactor;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
