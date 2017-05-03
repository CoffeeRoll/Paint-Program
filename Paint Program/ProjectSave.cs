using System;
using System.IO.Compression;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace Paint_Program
{
    public class ProjectSave
    {

        SharedSettings settings;

        public ProjectSave(SharedSettings ss)
        {
            settings = ss;
            BackgroundWorker bw = new BackgroundWorker();

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = SharedSettings.getGlobalString("projectsave_dialog_filter");
                sfd.Title = SharedSettings.getGlobalString("projectsave_dialog_title");
                sfd.OverwritePrompt = false;
                sfd.ShowDialog();

                bw.DoWork += (send, args) =>
                {
                    doSave(sfd);
                };

                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

        }

        private void doSave(SaveFileDialog sfd)
        {
            if (sfd.FileName != "")
            {
                try
                {
                    System.IO.Directory.CreateDirectory("save");

                    Bitmap[] bitArr = settings.getLayerBitmaps();
                    string baseDir = System.IO.Directory.GetCurrentDirectory();
                    string[] LayerNames = settings.getLayerNames();

                    for (int n = 0; n < bitArr.Length; n++)
                    {
                        bitArr[n].Save(baseDir + "\\save\\" + LayerNames[n] + ".png", ImageFormat.Png);
                    }

                    System.IO.File.WriteAllLines(baseDir + @"\save\names.txt", LayerNames);

                    if (SharedSettings.bitmapWatermark != null)
                    {
                        System.IO.Directory.CreateDirectory("save\\watermark");
                        SharedSettings.bitmapWatermark.Save("save\\watermark\\watermark.png", ImageFormat.Png);
                        string[] watermarkInfo = {SharedSettings.bRenderWatermark.ToString(), SharedSettings.watermarkStyle};
                        System.IO.File.WriteAllLines(baseDir + @"\\save\\watermark\\watermarkdata.txt", watermarkInfo);
                    }

                    if (System.IO.File.Exists(sfd.FileName))
                    {
                        System.IO.File.Delete(sfd.FileName);
                    }

                    ZipFile.CreateFromDirectory(baseDir + @"\save", sfd.FileName);

                    System.IO.Directory.Delete(baseDir + @"\save", true);

                    string message = SharedSettings.getGlobalString("projectsave_saved");
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, message);
                }
                catch (Exception e)
                {
                    string message = SharedSettings.getGlobalString("projectsave_error") + "\n\n" + e.ToString();
                    MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, message);
                }


            }
        }
    }
}
