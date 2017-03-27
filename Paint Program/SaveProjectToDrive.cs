using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class SaveProjectToDrive
    {
        public SaveProjectToDrive(SharedSettings ss)
        {
            try
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += (send, args) =>
                {
                    doSave(ss, send, args);
                };

                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show("Problem starting save project to drive thread: " + e.Message);
            }
        }

        public void doSave(SharedSettings ss, object sender, DoWorkEventArgs args)
        {
            try
            {
                System.IO.Directory.CreateDirectory("save");

                Bitmap[] bitArr = ss.getLayerBitmaps();
                string baseDir = System.IO.Directory.GetCurrentDirectory();
                string[] LayerNames = ss.getLayerNames();

                for (int n = 0; n < bitArr.Length; n++)
                {
                    bitArr[n].Save(baseDir + "\\save\\" + LayerNames[n] + ".png", ImageFormat.Png);
                }

                System.IO.File.WriteAllLines(baseDir + @"\save\names.txt", LayerNames);

                if (System.IO.File.Exists("tmpfile.lep"))
                {
                    System.IO.File.Delete("tmpfile.lep");
                }

                ZipFile.CreateFromDirectory(baseDir + @"\save", @"tmpfile.lep");

                System.IO.Directory.Delete(baseDir + @"\save", true);
            }
            catch (Exception e)
            {
                string message = SharedSettings.getGlobalString("projectsave_error") + "\n\n" + e.ToString();
                MessageBox.Show(message);
            }

            try
            {
                GoogleDriveUpload.DriveUpload("tmpfile.lep");
                string message = SharedSettings.getGlobalString("projectsave_saved");
                MessageBox.Show(message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Problem with Google Drive - " + e.Message);
            }
            try
            {
                System.IO.File.Delete("tmpfile.lep");
            }
            catch (Exception e)
            {
                MessageBox.Show("Problem deleting temp lep file: " + e.Message);
            }
        }
    }
}
