using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Paint_Program
{
    class SaveToDrive
    {
        public SaveToDrive(SharedSettings ss)
        {
            Bitmap bm = ss.getBitmapCanvas();
            //FileStream fs = new FileStream("tmpfile.jpg", FileMode.OpenOrCreate);
            bm.Save("tmpfile.jpg", ImageFormat.Jpeg);

            GoogleDriveUpload.DriveUpload("tmpfile.jpg");

            System.IO.File.Delete(@"tmpfile.jpg");
        }
    }
}
