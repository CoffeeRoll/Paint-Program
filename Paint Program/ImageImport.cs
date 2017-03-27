using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace Paint_Program
{
    class ImageImport
    {
        public ImageImport(SharedSettings ss)
        {
            BackgroundWorker bw = new BackgroundWorker();

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = SharedSettings.getGlobalString("imageimport_dialog_filter");
                ofd.Title = SharedSettings.getGlobalString("imageimport_dialog_title");
                ofd.ShowDialog();

                bw.DoWork += (send, args) =>
                {
                    doOpen(ss, ofd, send, args);
                };

                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

        private void doOpen(SharedSettings ss, OpenFileDialog ofd, object sender, DoWorkEventArgs args)
        {
            if (ofd.FileName != "")
            {
                try
                {
                    var ms = new MemoryStream(File.ReadAllBytes(ofd.FileName));
                    Bitmap bm = new Bitmap(Image.FromStream(ms));
                    ss.setImportImage(bm);
                }
                catch (Exception e)
                {
                    string message =  SharedSettings.getGlobalString("importimage_error") + "\n\n" + e.ToString();
                    MessageBox.Show(message);
                }
            }
        }
    }
}
