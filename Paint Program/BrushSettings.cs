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
    public partial class BrushSettings : UserControl
    {
        SharedSettings settings;

        public BrushSettings(SharedSettings s)
        {
            InitializeComponent();
            settings = s;

            pPrime.BackColor = settings.getPrimaryBrushColor();
            pSec.BackColor = settings.getSecondaryBrushColor();

        }

        private void pPrime_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pPrime_Click(object sender, EventArgs e)
        {

            if(cdPicker.ShowDialog() == DialogResult.OK)
            {
                pPrime.BackColor = cdPicker.Color;
                settings.setPrimaryBrushColor(cdPicker.Color);
            }
        }

        private void pSec_Click(object sender, EventArgs e)
        {
            if (cdPicker.ShowDialog() == DialogResult.OK)
            {
                pPrime.BackColor = cdPicker.Color;
                settings.setPrimaryBrushColor(cdPicker.Color);
            }
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
