using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace Paint_Program
{
    public class FileSave
    {
        public FileSave(SharedSettings ss)
        {
            Bitmap bm = ss.getBitmapCanvas();   // Get the image from the bitmap object
            BackgroundWorker bw = new BackgroundWorker();

            try {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.OverwritePrompt = false;
                sfd.Filter = SharedSettings.getGlobalString("filesave_filter");
                sfd.Title = "Save an Image File";
                sfd.ShowDialog();

                bw.DoWork += (send, args) =>
                {
                    doSave(bm, sfd, send, args);
                };

                bw.RunWorkerAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

        }
        //test comment, please ignore
        private void doSave(Bitmap bm, SaveFileDialog sfd, object sender, DoWorkEventArgs args)
        {
            if (sfd.FileName != "")
            {
                try
                {
                    System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();

                    if (sfd.CheckFileExists)
                    {
                        Console.WriteLine(sfd.FileName);
                        System.IO.File.Delete(sfd.FileName);
                    }
                        switch (sfd.FilterIndex)
                        {
                            case 1:
                                bm.Save(fs, ImageFormat.Bmp);
                                break;
                            case 2:
                                bm.Save(fs, ImageFormat.Gif);
                                break;
                            case 3:
                                saveGIFAnimation(fs);
                                break;
                            case 4:
                                bm.Save(fs, ImageFormat.Icon);
                                break;
                            case 5:
                                bm.Save(fs, ImageFormat.Jpeg);
                                break;
                            case 6:
                                bm.Save(fs, ImageFormat.Png);
                                break;
                            case 7:
                                bm.Save(fs, ImageFormat.Tiff);
                                break;
                        }

                        string message = SharedSettings.getGlobalString("filesave_saved");
                        MessageBox.Show(message);
                        fs.Close();
                }
                catch (Exception e)
                {
                    string message = SharedSettings.getGlobalString("filesave_error") + "\n\n" + e.ToString();
                    MessageBox.Show(message);
                }
            }
        }
        
        private void saveGIFAnimation(System.IO.FileStream fs)
        {
            using (var stream = new MemoryStream())
            {
                using (var encoder = new AnimatedGifEncoder(stream, null, null, 12))
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

        }
    }
}
