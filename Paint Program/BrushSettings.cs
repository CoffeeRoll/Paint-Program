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
    public partial class BrushSettings : UserControl
    {
        SharedSettings settings;
        BackgroundWorker bw;

        public BrushSettings(SharedSettings s)
        {
            InitializeComponent();
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
        }

        private void pSec_Click(object sender, EventArgs e)
        {
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
        }

        private void tbSize_ValueChanged(object sender, EventArgs e)
        {
            lSize.Text = "Brush Size: " + tbSize.Value.ToString();
            lSize.Refresh();
            settings.setBrushSize(tbSize.Value);
        }

        private void tbHardness_ValueChanged(object sender, EventArgs e)
        {
            lHard.Text = "Brush Hardness: " + tbHardness.Value.ToString();
            lHard.Refresh();
            settings.setBrushHardness(tbHardness.Value);
        }
    }
}
