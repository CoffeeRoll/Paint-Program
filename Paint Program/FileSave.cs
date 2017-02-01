using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

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
                sfd.Filter = "Bitmap Image|*.bmp|GIF Image|*.gif|Icon Image|*.ico|JPeg Image|*.jpg|PNG Image|*.png|TIFF Image|*.tiff";
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

                    switch (sfd.FilterIndex)
                    {
                        case 1:
                            bm.Save(fs, ImageFormat.Bmp);
                            break;
                        case 2:
                            bm.Save(fs, ImageFormat.Gif);
                            break;
                        case 3:
                            bm.Save(fs, ImageFormat.Icon);
                            break;
                        case 4:
                            bm.Save(fs, ImageFormat.Jpeg);
                            break;
                        case 5:
                            bm.Save(fs, ImageFormat.Png);
                            break;
                        case 6:
                            bm.Save(fs, ImageFormat.Tiff);
                            break;
                    }

                    string message = "The file was saved!";
                    MessageBox.Show(message);
                    fs.Close();
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
