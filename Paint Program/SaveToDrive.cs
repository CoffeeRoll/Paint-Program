using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class SaveToDrive
    {
        public SaveToDrive(SharedSettings ss)
        {
            try
            {
                Bitmap bm = ss.getBitmapCanvas();
                BackgroundWorker bw = new BackgroundWorker();

                bw.DoWork += (send, args) =>
                {
                    doSave(bm, ss, send, args);
                };

                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error starting thread to save to drive: " + e.Message);
            }
        }

        public void doSave(Bitmap bm, SharedSettings ss, object sender, DoWorkEventArgs args)
        {
            try
            {
                bm.Save("tmpfile.jpg", ImageFormat.Jpeg);

                GoogleDriveUpload.DriveUpload("tmpfile.jpg");

                string message = SharedSettings.getGlobalString("projectsave_saved");
                MessageBox.Show(message);

                System.IO.File.Delete(@"tmpfile.jpg");
            }
            catch (Exception e)
            {
                MessageBox.Show("Issue saving image to drive: " + e.Message);
            }
        }
    }
}
