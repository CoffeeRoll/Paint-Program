using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class SaveProjectToDrive
    {
        public SaveProjectToDrive(SharedSettings ss)
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

                if (System.IO.File.Exists("%APPDATA%\tmpfile.lep"))
                {
                    System.IO.File.Delete("%APPDATA%\tmpfile.lep");
                }

                ZipFile.CreateFromDirectory(baseDir + @"\save", @"%APPDATA%\tmpfile");

                System.IO.Directory.Delete(baseDir + @"\save", true);

                string message = SharedSettings.getGlobalString("projectsave_saved");
                MessageBox.Show(message);
            }
            catch (Exception e)
            {
                string message = SharedSettings.getGlobalString("projectsave_error") + "\n\n" + e.ToString();
                MessageBox.Show(message);
            }


            GoogleDriveUpload.DriveUpload("%APPDATA%\tmpfile.lep");

            System.IO.File.Delete("%APPDATA%\tmpfile.lep");

        }
    }
}
