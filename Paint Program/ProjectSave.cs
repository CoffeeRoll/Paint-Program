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
                sfd.Filter = "Le Paint Project File|*.lep|All Files|*.*";
                sfd.Title = "Save Project";
                sfd.OverwritePrompt = false;
                sfd.ShowDialog();

                bw.DoWork += (send, args) =>
                {
                    doSave(sfd, send, args);
                };

                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

        }

        private void doSave( SaveFileDialog sfd, object sender, DoWorkEventArgs args)
        {
            if (sfd.FileName != "")
            {
                try
                {
                    System.IO.Directory.CreateDirectory("save");

                    Bitmap[] bitArr = settings.getLayerBitmaps();
                    Console.WriteLine("HEY");
                    string baseDir = System.IO.Directory.GetCurrentDirectory();
                    string[] LayerNames = settings.getLayerNames();

                    for(int n = 0; n < bitArr.Length; n++)
                    {
                        bitArr[n].Save(baseDir + "\\save\\" + LayerNames[n] + ".png", ImageFormat.Png);
                    }

                    System.IO.File.WriteAllLines(baseDir + @"\save\names.txt", LayerNames);

                    if (System.IO.File.Exists(sfd.FileName))
                    {
                        System.IO.File.Delete(sfd.FileName);
                    }

                    ZipFile.CreateFromDirectory(baseDir + @"\save", sfd.FileName);

                    System.IO.Directory.Delete(baseDir + @"\save", true);

                    string message = "The file was saved!";
                    MessageBox.Show(message);
                }
                catch (Exception e)
                {
                    string message = "An error occurred while saving. \n\n" + e.ToString();
                    MessageBox.Show(message);
                }


            }
        }
    }
}
