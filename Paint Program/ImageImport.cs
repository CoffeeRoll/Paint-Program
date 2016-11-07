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
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "All File Types|*.*|Bitmap Image|*.bmp|GIF Image|*.gif|Icon Image|*.ico|JPeg Image|*.jpg|PNG Image|*.png|TIFF Image|*.tiff";
                sfd.Title = "Open an Image File";
                sfd.ShowDialog();

                bw.DoWork += (send, args) =>
                {
                    doOpen(ss, sfd, send, args);
                };

                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

        private void doOpen(SharedSettings ss, SaveFileDialog sfd, object sender, DoWorkEventArgs args)
        {
            if (sfd.FileName != "")
            {
                try
                {
                    var ms = new MemoryStream(File.ReadAllBytes(sfd.FileName));
                    Bitmap bm = new Bitmap(Image.FromStream(ms));
                    ss.setBitmapCanvas(bm);
                }
                catch (Exception e)
                {
                    string message = "An error occurred while opening. \n\n" + e.ToString();
                    MessageBox.Show(message);
                }
            }
        }
    }
}
