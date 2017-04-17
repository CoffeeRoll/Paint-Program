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
using System.Windows.Media.Imaging;

namespace Paint_Program
{
    class ProjectLoad
    {
        SharedSettings settings;
        string Result;

        public ProjectLoad(SharedSettings ss)
        {
            settings = ss;
            Result = "";

            BackgroundWorker bw = new BackgroundWorker();

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = SharedSettings.getGlobalString("projectopen_dialog_filter");
                ofd.Title = SharedSettings.getGlobalString("projectopen_dialog_title");
                ofd.ShowDialog();

                bw.DoWork += (send, args) =>
                {
                    doOpen(settings, ofd, send, args);
                };
                ofd.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error was thrown while opening!" + e.InnerException);
            }
        }

        public string getResultMessage()
        {
            return Result;
        }

        private void doOpen(SharedSettings ss, OpenFileDialog ofd, object sender, DoWorkEventArgs args)
        {
            if (ofd.FileName != "")
            {
                try
                {
                    if (ofd.FileName.EndsWith(".gif"))
                    {
                        using (System.IO.Stream sr = new FileStream(ofd.FileName,FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            GifBitmapDecoder gbd = new GifBitmapDecoder(sr, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                            List<Bitmap> bmps = new List<Bitmap>();
                            List<string> names = new List<string>();
                            for(int i = 0; i < gbd.Frames.Count; i++)
                            {
                                bmps.Add(BitmapFromSource(gbd.Frames[i]));
                                names.Add(i.ToString());
                            }
                            settings.setCanvasWidth((int)gbd.Frames[0].Width);
                            settings.setCanvasHeight((int)gbd.Frames[0].Height);
                            settings.setLayerBitmaps(bmps.ToArray());
                            settings.setLayerNames(names.ToArray());
                            settings.setLoadFromSettings(true);
                            bmps.Clear();
                            names.Clear();
                        }
                        
                    }
                    else if (ofd.FileName.EndsWith(".lep"))
                    {

                        string baseDir = System.IO.Directory.GetCurrentDirectory();

                        try {
                            DeleteDirectory(baseDir + @"\load");
                        }
                        catch (Exception e)
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
                            Bitmap temp = (Bitmap)Image.FromFile(baseDir + @"\load\" + layerNames[n] + ".png");

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
                        layerNames.Clear(); //Clears Layer Name File Reference
                    }
                    else
                    {
                        SharedSettings.bLoadFromSettings = false;
                        Result = SharedSettings.getGlobalString("projectopen_error");
                        MessageBox.Show(Result);
                        
                    }

                }
                catch (Exception e)
                {
                    Result = SharedSettings.getGlobalString("projectopen_error") + "\n\n" + e.ToString();
                    MessageBox.Show(Result);
                }
            }
        }

        public Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            //convert image format
            var src = new System.Windows.Media.Imaging.FormatConvertedBitmap();
            src.BeginInit();
            src.Source = bitmapsource;
            src.DestinationFormat = System.Windows.Media.PixelFormats.Bgra32;
            src.EndInit();

            //copy to bitmap
            Bitmap bitmap = new Bitmap(src.PixelWidth, src.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            src.CopyPixels(System.Windows.Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bitmap.UnlockBits(data);

            return bitmap;
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
