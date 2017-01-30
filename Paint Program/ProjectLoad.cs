using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class ProjectLoad
    {
        SharedSettings settings;

        public ProjectLoad(SharedSettings ss)
        {
            settings = ss;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Le Paint Project|*.lep|All File Types|*.*";
                ofd.Title = "Open a Project File";
                ofd.ShowDialog();


                doOpen(settings, ofd);
                ofd.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error was thrown while opening!" + e.InnerException);
            }
        }

        private void doOpen(SharedSettings ss, OpenFileDialog ofd)
        {
            if (ofd.FileName != "")
            {
                try
                {
                    string baseDir = System.IO.Directory.GetCurrentDirectory();

                    try {
                        DeleteDirectory(baseDir + @"\load");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Couldn't delete 'load' Directory!" + e.InnerException);
                    }

                    List<String> layerNames = new List<String>();
                    List<Bitmap> layerBitmaps = new List<Bitmap>();

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                    DirectoryInfo di = Directory.CreateDirectory("load", securityRules);

                    System.IO.Compression.ZipFile.ExtractToDirectory(ofd.FileName, baseDir + @"\load");

                    using (System.IO.StreamReader sr = new System.IO.StreamReader(baseDir + @"\load\names.txt", Encoding.Default))
                    {
                        string line;
                        // Read the stream to a string, and write the string to the console.
                        while ((line = sr.ReadLine()) != null)
                        {
                            layerNames.Add(line);
                        }

                        sr.Close();
                        sr.Dispose();
                    }

                    int w = 0;
                    int h = 0;

                    for (int n = 0; n < layerNames.Count; n++)
                    {
                        Bitmap temp =(Bitmap)Image.FromFile(baseDir + @"\load\" + layerNames[n] + ".png");

                        w = (temp.Width > w) ? temp.Width : w;
                        h = (temp.Height > h) ? temp.Height : h;
                        layerBitmaps.Add((Bitmap)temp.Clone());
                    }

                    settings.setCanvasWidth(w);
                    settings.setCanvasHeight(h);
                    settings.setLayerBitmaps(layerBitmaps.ToArray());
                    settings.setLayerNames(layerNames.ToArray());
                    settings.setLoadFromSettings(true);

                    layerBitmaps.Clear(); //Clears all Bitmap File References
                    layerNames.Clear(); //Clears Layer NAme File Reference

                }
                catch (Exception e)
                {
                    string message = "An error occurred while opening. \n\n" + e.ToString();
                    MessageBox.Show(message);
                }
            }
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

    }
}
