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
        BackgroundWorker bw;

        public BrushSettings()
        {
            InitializeComponent();

            lPrime.Text = SharedSettings.getGlobalString("brushsettings_color_primary");
            lSec.Text = SharedSettings.getGlobalString("brushsettings_color_secondary");
            lSize.Text = SharedSettings.getGlobalString("brushsettings_brush_size");
            lHard.Text = SharedSettings.getGlobalString("brushsettings_brush_hardness");
			
            pPrime.BackColor = SharedSettings.getPrimaryBrushColor();
            pSec.BackColor = SharedSettings.getSecondaryBrushColor();

            tbSize.Value = (int)SharedSettings.getBrushSize();
            tbHardness.Value = (int)SharedSettings.getBrushHardness();
            
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
                    if (cdPicker.ShowDialog(new Form() { TopMost = true }) == DialogResult.OK)
                    {
						SharedSettings.setPrimaryBrushColor(cdPicker.Color);
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
                    if (cdPicker.ShowDialog(new Form() { TopMost = true }) == DialogResult.OK)
                    {
						SharedSettings.setSecondaryBrushColor(cdPicker.Color);
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
			SharedSettings.setBrushSize(tbSize.Value);
        }

        private void tbHardness_ValueChanged(object sender, EventArgs e)
        {
            lHard.Text = SharedSettings.getGlobalString("brushsettings_brush_hardness") + ": " + tbHardness.Value.ToString();
            lHard.Refresh();
			SharedSettings.setBrushHardness(tbHardness.Value);
        }

        public void CheckChange()
        {
            pPrime.BackColor = SharedSettings.getPrimaryBrushColor();
            pSec.BackColor = SharedSettings.getSecondaryBrushColor();
            this.Refresh();
        }

        public void UpdateText()
        {
            lPrime.Text = SharedSettings.getGlobalString("brushsettings_color_primary");
            lSec.Text = SharedSettings.getGlobalString("brushsettings_color_secondary");
            lSize.Text = SharedSettings.getGlobalString("brushsettings_brush_size") + ": " + tbSize.Value.ToString();
            lHard.Text = SharedSettings.getGlobalString("brushsettings_brush_hardness") + ": " + tbHardness.Value.ToString();
        }
    }
}
