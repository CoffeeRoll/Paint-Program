using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    public partial class TextSelect : Form
    {
        public string UserText { get; private set; }
        public int FontSize { get; private set; }
        public string FontType { get; private set; }
        public FontFamily[] Families { get; }

        public TextSelect()
        {
            InitializeComponent();
            
            foreach(FontFamily oneFontFamily in FontFamily.Families)
            {
                Fontbox.Items.Add(oneFontFamily.Name);
            }
        }

        private void tSelect_Click(object sender, EventArgs e)
        {
            int size;
            if (!Int32.TryParse(fontSize.Text, out size) || (Int32.TryParse(fontSize.Text, out size) && size <= 0))
            {
                fontSize.BackColor = Color.Pink;
                fontSize.Update();
            }

            if (size > 0)
            {
                this.UserText = userText.Text;
                this.FontSize = size;
                this.FontType = Fontbox.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void Handle_KeyPress(object sender, KeyEventArgs k)
        {
            if (k.KeyCode == Keys.Enter)
            {
                tSelect_Click(sender, null);
            }
        }
    }
}
