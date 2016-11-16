using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

        private void doOpen(SharedSettings ss, OpenFileDialog ofd)
        {
            if (ofd.FileName != "")
            {
                try
                {
                    string baseDir = System.IO.Directory.GetCurrentDirectory();

                    System.IO.Directory.Delete(baseDir + @"\load", true);

                    List<String> layerNames = new List<String>();
                    List<Bitmap> layerBitmaps = new List<Bitmap>();

                    System.IO.Directory.CreateDirectory("load");

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
                    }

                    int w = 0;
                    int h = 0;

                    for (int n = 0; n < layerNames.Count; n++)
                    {
                        Bitmap temp = (Bitmap)Image.FromFile(baseDir + @"\load\" + layerNames[n] + ".png");

                        w = (temp.Width > w) ? temp.Width : w;
                        h = (temp.Height > h) ? temp.Height : h;
                        Console.WriteLine(w + " " + h);
                        layerBitmaps.Add(temp);
                        Console.WriteLine("LAYERS: " + layerBitmaps.Count);
                    }

                    settings.setCanvasWidth(w);
                    settings.setCanvasHeight(h);
                    settings.setLayerBitmaps(layerBitmaps.ToArray());
                    settings.setLayerNames(layerNames.ToArray());
                    settings.setLoadFromSettings(true);
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
