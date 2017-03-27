using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Paint_Program
{
    public partial class BrushSettings : UserControl, ITextUpdate
    {
        SharedSettings settings;
        BackgroundWorker bw;

        public BrushSettings(SharedSettings s)
        {
            InitializeComponent();

            lPrime.Text = SharedSettings.getGlobalString("brushsettings_color_primary");
            lSec.Text = SharedSettings.getGlobalString("brushsettings_color_secondary");
            lSize.Text = SharedSettings.getGlobalString("brushsettings_brush_size");
            lHard.Text = SharedSettings.getGlobalString("brushsettings_brush_hardness");

            settings = s;

            pPrime.BackColor = settings.getPrimaryBrushColor();
            pSec.BackColor = settings.getSecondaryBrushColor();

            tbSize.Value = (int)settings.getBrushSize();
            tbHardness.Value = (int)settings.getBrushHardness();
            
            this.Refresh();

        }

        private void pPrime_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pPrime_Click(object sender, EventArgs e)
        {

            try {
                bw = new BackgroundWorker();
                bw.DoWork += (send, args) =>
                {
                    if (cdPicker.ShowDialog() == DialogResult.OK)
                    {
                        settings.setPrimaryBrushColor(cdPicker.Color);
                    }

                };
                bw.RunWorkerCompleted += (send, args) =>
                {
                    pPrime.BackColor = cdPicker.Color;
                    pPrime.Refresh();
                };
                bw.RunWorkerAsync();
            }catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        private void pSec_Click(object sender, EventArgs e)
        {
            try {
                bw = new BackgroundWorker();
                bw.DoWork += (send, args) =>
                {
                    if (cdPicker.ShowDialog() == DialogResult.OK)
                    {
                        settings.setSecondaryBrushColor(cdPicker.Color);
                    }

                };
                bw.RunWorkerCompleted += (send, args) =>
                {
                    pSec.BackColor = cdPicker.Color;
                    pSec.Refresh();
                };
                bw.RunWorkerAsync();
            }catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        private void tbSize_ValueChanged(object sender, EventArgs e)
        {
            lSize.Text = SharedSettings.getGlobalString("brushsettings_brush_size") + ": " + tbSize.Value.ToString();
            lSize.Refresh();
            settings.setBrushSize(tbSize.Value);
        }

        private void tbHardness_ValueChanged(object sender, EventArgs e)
        {
            lHard.Text = SharedSettings.getGlobalString("brushsettings_brush_hardness") + ": " + tbHardness.Value.ToString();
            lHard.Refresh();
            settings.setBrushHardness(tbHardness.Value);
        }

        public void CheckChange()
        {
            pPrime.BackColor = settings.getPrimaryBrushColor();
            pSec.BackColor = settings.getSecondaryBrushColor();
            this.Refresh();
        }

        public void updateText()
        {
            lPrime.Text = SharedSettings.getGlobalString("brushsettings_color_primary");
            lSec.Text = SharedSettings.getGlobalString("brushsettings_color_secondary");
            lSize.Text = SharedSettings.getGlobalString("brushsettings_brush_size") + ": " + tbSize.Value.ToString();
            lHard.Text = SharedSettings.getGlobalString("brushsettings_brush_hardness") + ": " + tbHardness.Value.ToString();
        }
    }
}
