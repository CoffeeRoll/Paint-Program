using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class SaveToDrive
    {
        public SaveToDrive(SharedSettings ss, string fileName, string fileType)
        {
            try
            {
                Bitmap bm = ss.getBitmapCanvas();
                BackgroundWorker bw = new BackgroundWorker();

                if (fileType == "LePaint Project File | *.lep")
                {
                    bw.DoWork += (send, args) =>
                    {
                        doSaveProject(ss, fileName, send, args);
                    };
                }
                else if (fileType == "Animated GIF | *.gif")
                {
                    bw.DoWork += (send, args) =>
                    {
                        doSaveAnimGif(fileName, send, args);
                    };
                }
                else {
                    bw.DoWork += (send, args) =>
                    {
                        doSave(bm, fileName, fileType, send, args);
                    };
                }

                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error starting thread to save to drive: " + e.Message);
            }
        }

        public void doSave(Bitmap bm, string filename, string filetype, object sender, DoWorkEventArgs args)
        {
            try
            {
                string ext = ".png";
                ImageFormat fmt = ImageFormat.Png;
                switch (filetype)
                {
                    case ("Jpeg | *.jpg"):
                        ext = ".jpg";
                        fmt = ImageFormat.Jpeg;
                        break;
                    case ("PNG | *.png"):
                        // don't need to change anything
                        break;
                    case ("GIF | *.gif"):
                        ext = ".gif";
                        fmt = ImageFormat.Gif;
                        break;
                    case ("ICON | *.ico"):
                        ext = ".ico";
                        fmt = ImageFormat.Icon;
                        break;
                    case ("TIFF | *.tiff"):
                        ext = ".tiff";
                        fmt = ImageFormat.Tiff;
                        break;
                    case ("Bitmap | *.bmp"):
                        ext = ".bmp";
                        fmt = ImageFormat.Bmp;
                        break;
                    default:
                        // on error, default to png
                        break;
                }

                bm.Save(filename + ext, fmt);

                GoogleDriveUpload.DriveUpload(filename + ext);

                string message = SharedSettings.getGlobalString("projectsave_saved");
                MessageBox.Show(message);

                System.IO.File.Delete(filename + ext);
            }
            catch (Exception e)
            {
                MessageBox.Show(SharedSettings.getGlobalString("gdrivesavedialog_error") + e.Message);
            }
        }

        public void doSaveAnimGif(string filename, object sender, DoWorkEventArgs args)
        {
            FileStream fs = new FileStream(filename + ".gif", FileMode.OpenOrCreate);
            using (var stream = new MemoryStream())
            {
                using (var encoder = new GifEncoder(stream, null, null, 12))
                {
                    for (int i = 0; i < SharedSettings.Layers.Length; i++)
                    {
                        var image = (Image)(SharedSettings.Layers[i] as Bitmap).Clone();
                        encoder.AddFrame(image, 0, 0, TimeSpan.FromSeconds(0));
                    }

                    stream.Position = 0;
                    stream.WriteTo(fs);
                }
            }
            fs.Close();

            try
            {
                GoogleDriveUpload.DriveUpload(filename + ".gif");
                string message = SharedSettings.getGlobalString("projectsave_saved");
                MessageBox.Show(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(SharedSettings.getGlobalString("gdrivesavedialog_error") + e.Message);
            }
            try
            {
                System.IO.File.Delete(filename + ".gif");
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem deleting temp gif file: " + e.Message);
            }
        }

        public void doSaveProject(SharedSettings ss, string filename, object sender, DoWorkEventArgs args)
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

                if (System.IO.File.Exists(filename + ".lep"))
                {
                    System.IO.File.Delete(filename + ".lep");
                }

                ZipFile.CreateFromDirectory(baseDir + @"\save", filename + ".lep");

                System.IO.Directory.Delete(baseDir + @"\save", true);
            }
            catch (Exception e)
            {
                string message = SharedSettings.getGlobalString("projectsave_error") + "\n\n" + e.ToString();
                MessageBox.Show(message);
            }

            try
            {
                GoogleDriveUpload.DriveUpload(filename + ".lep");
                string message = SharedSettings.getGlobalString("projectsave_saved");
                MessageBox.Show(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(SharedSettings.getGlobalString("gdrivesavedialog_error") + e.Message);
            }
            try
            {
                System.IO.File.Delete(filename + ".lep");
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem deleting temp lep file: " + e.Message);
            }
        }
    }
}
