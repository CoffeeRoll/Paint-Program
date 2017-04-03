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
    public partial class GDriveSaveDialog : Form
    {
        public string fileName { get; private set; }
        public string fileType { get; private set; }

        public GDriveSaveDialog()
        {
            InitializeComponent();
            this.Text = SharedSettings.getGlobalString("gdrivesavedialog_title");
            lbl_FileName.Text = SharedSettings.getGlobalString("gdrivesavedialog_filename");
            lbl_FileType.Text = SharedSettings.getGlobalString("gdrivesavedialog_filetype");
            btn_save.Text = SharedSettings.getGlobalString("gdrivesavedialog_save");
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            fileName = tb_FileName.Text;
            fileType = cb_FileType.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GDriveSaveDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_save_Click(sender, null);
            }
        }
    }
}
