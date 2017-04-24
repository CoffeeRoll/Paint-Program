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
        string Result;

        public ProjectLoad()
        {
            Result = "";

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = SharedSettings.getGlobalString("projectopen_dialog_filter");
                ofd.Title = SharedSettings.getGlobalString("projectopen_dialog_title");
                ofd.ShowDialog();

                doOpen(ofd);

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

        private void doOpen(OpenFileDialog ofd)
        {

            Console.WriteLine("Attempting to open: " + ofd.FileName);

            if (ofd.FileName != "")
            {
                try
                {
                    if (ofd.FileName.EndsWith(".gif"))
                    {
                        using (System.IO.Stream sr = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            GifBitmapDecoder gbd = new GifBitmapDecoder(sr, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                            List<Bitmap> bmps = new List<Bitmap>();
                            List<string> names = new List<string>();
                            for (int i = 0; i < gbd.Frames.Count; i++)
                            {
                                bmps.Add(BitmapFromSource(gbd.Frames[i]));
                                names.Add(i.ToString());
                            }
                            SharedSettings.iCanvasWidth = ((int)gbd.Frames[0].Width);
                            SharedSettings.iCanvasHeight = ((int)gbd.Frames[0].Height);
                            SharedSettings.Layers = (bmps.ToArray());
                            SharedSettings.LayerNames = (names.ToArray());
                            SharedSettings.bLoadFromSettings = (true);
                            bmps.Clear();
                            names.Clear();
                        }

                    }
                    else if (ofd.FileName.EndsWith(".lep"))
                    {
                        Console.WriteLine(ofd.FileName);
                        string baseDir = System.IO.Directory.GetCurrentDirectory();

                        try
                        {
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

                        try
                        {
                            if (Directory.Exists(baseDir + @"\load\watermark"))
                            {
                                //Console.WriteLine("Watermark Found");
                                SharedSettings.bitmapWatermark = (Bitmap)Bitmap.FromFile(baseDir + @"\load\watermark\watermark.png").Clone();
                                SharedSettings.watermarkPath = baseDir + @"\load\watermark\watermark.png";
                                List<string> watermarkdata = new List<string>();
                                using (System.IO.StreamReader sr = new System.IO.StreamReader(baseDir + @"\load\watermark\watermarkdata.txt", Encoding.Default))
                                {
                                    string line;
                                    
                                    // Read the stream to a string, and write the string to the console.
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        Console.WriteLine(line);
                                        watermarkdata.Add(line);
                                    }
                                    sr.Close();
                                    sr.Dispose();
                                }

                                bool show;
                                Boolean.TryParse(watermarkdata[0], out show);
                                SharedSettings.bRenderWatermark = show;
                                SharedSettings.watermarkStyle = watermarkdata[1];
                            }
                        }
                        catch (Exception err)
                        {
                            SharedSettings.bitmapWatermark = null;
                            Console.WriteLine("Exception Thrown in Watermark Loading" + err.InnerException);
                        }
                        
                        SharedSettings.iCanvasWidth = w;
                        SharedSettings.iCanvasHeight = h;
                        SharedSettings.Layers = layerBitmaps.ToArray();
                        SharedSettings.LayerNames = layerNames.ToArray();
                        SharedSettings.bLoadFromSettings = true;

                        Console.WriteLine(SharedSettings.Layers.Count());

                        layerBitmaps.Clear(); //Clears all Bitmap File References
                        layerNames.Clear(); //Clears Layer Name File Reference
                        Console.WriteLine(SharedSettings.Layers.Count());

                    }

                }
                catch (Exception e)
                {
                    Result = SharedSettings.getGlobalString("projectopen_error") + "\n\n" + e.ToString();
                    //MessageBox.Show(Result);
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
