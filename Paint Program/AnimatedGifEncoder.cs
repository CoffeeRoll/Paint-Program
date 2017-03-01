using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Windows.Interop;

namespace Paint_Program
{
    class AnimatedGifEncoder {

        private GifBitmapEncoder GifEncoder = new GifBitmapEncoder();

        // <summary>
        // Return the GIF specification version. This always returns "GIF89a"
        // </summary>
        public static String EncoderVersion = "GIF89a";

        // <summary>
        // Get or set a value that indicate if the GIF will repeat the animation after the last frame is shown. The default value is True
        // </summary>
        public static Boolean Repeat = true;

        // <summary>
        // Get or set a collection of metadata string to be embedded in the GIF file. Each string has a max length of 254 
        // characters (Any character above this limit will be truncated). The string will be encoded UTF-7. 
        // </summary>
        public static List<String> MetadataString = new List<String>();

        // <summary>
        //  Get or set the amount of time each frame will be shown (in milliseconds). The default value is 200ms
        // </summary>
        public static int FrameRate = 200;

        // <summary>
        // Add a frame to the encoder frame collection
        // </summary>
        // <param name="Frame">The bitmap to be added</param>
        public void AddFrame(Bitmap Frame) {
            if (Frame != null)
            {
                if (!(Frame.Width == 0) && !(Frame.Height == 0))
                {
                    var bmpSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                      Frame.GetHbitmap(),
                                      IntPtr.Zero,
                                      Int32Rect.Empty,
                                      BitmapSizeOptions.FromEmptyOptions());

                    GifEncoder.Frames.Add(BitmapFrame.Create(bmpSource));
                }
                else
                {
                    throw (new ArgumentException("Argument Frame, The bitmap size cannot be zero"));
                }
            }
            else
            {
                throw (new ArgumentException("Argument Frame cannot be nothing"));
            }
        }


        // <summary>
        // Writes the animated GIF binary to a specified IO.Stream
        // </summary>
        // <param name="Stream">The stream where the binary is to be output. Can be any object type that derives from IO.Stream</param>
        public void Save(Stream stream) {

            var Data = new List<byte>();

            if (GifEncoder.Frames.Count != 0) {

                //Get the raw binary

                MemoryStream MStream = new MemoryStream();

                GifEncoder.Save(MStream);

                Data = MStream.ToArray().ToList();

            }
            else {

                throw (new Exception("Cannot encode the Gif. The frame collection is empty."));

                //Only documented exception is if Frames.count=0
            }


            //Locate the right location where to insert the metadata in the binary

            //This will be just before the first label 0x0021F9 (Graphic Control Extension)

            int MetadataPTR = -1;

            int flag = 0;

            do
            {

                MetadataPTR += 1;


                if (Data[MetadataPTR] == 0) {


                    if (Data[MetadataPTR + 1] == 0x21) {


                        if (Data[MetadataPTR + 2] == 0xF9) {


                            flag = 1;


                        }


                    }


                }


            } while (flag == 0);


            //SET METADATA Repeat

            //This adds an Application Extension: Netscape2.0

            if (Repeat) {

                byte[] Temp = new byte[(int)((Data.Count) - 1 + 19)];


                //label: 0x21, 0xFF + one byte: length(0xB) + NETSCAPE2.0 + one byte: Datalength(0x3) + {1, 0, 0} + Block terminator, 1 byte, 0x00


                byte[] ApplicationExtension = { 0x21, 0xFF, 0xB, 0x4E, 0x45, 0x54, 0x53, 0x43, 0x41, 0x50, 0x45, 0x32, 0x2E, 0x30, 0x3, 0x1, 0x0, 0x0, 0x0 };
                Array.Copy(Data.ToArray(), Temp, MetadataPTR);
                Array.Copy(ApplicationExtension, 0, Temp, MetadataPTR + 1, 19);
                Array.Copy(Data.ToArray(), MetadataPTR + 1, Temp, MetadataPTR + 20, Data.Count - MetadataPTR - 1);
                Data = Temp.ToList();
            }

            //SET METADATA Comments
            //This add a Comment Extension for each string
            if (MetadataString.Count > 0) {
                foreach (String Comment in MetadataString) {
                    if (!String.IsNullOrEmpty(Comment)) {
                        String TheComment;
                    if(Comment.Length > 254) { 
                        TheComment = Comment.Substring(0, 254)
                            }else{ 
                        TheComment = Comment
                            }
                            Dim CommentStringBytes() As Byte = System.Text.UTF7Encoding.UTF7.GetBytes(TheComment)
                    Dim DataString() As Byte = New Byte() { 0x21, 0xFE, CByte(CommentStringBytes.Length)}
                    DataString = DataString.Concat(CommentStringBytes).Concat(New Byte() { 0x0}).ToArray
                Dim Temp(Data.Length - 1 + DataString.Length) As Byte
                            Array.Copy(Data, Temp, MetadataPTR)
                            Array.Copy(DataString, 0, Temp, MetadataPTR + 1, DataString.Length)
                            Array.Copy(Data, MetadataPTR + 1, Temp, MetadataPTR + DataString.Length + 1, Data.Length - MetadataPTR - 1)
                            Data = Temp;
                        }
            }
        }
    
        'SET METADATA frameRate
            'Sets the third and fourth byte of each Graphic Control Extension (5 bytes from each label 0x0021F9)
            For x As Integer = 0 To Data.Count - 1
                If Data(x) = 0 Then
                If Data(x + 1) = 0x21 Then
                    If Data(x + 2) = 0xF9 Then
                        If Data(x + 3) = 4 Then
                            'word, little endian, the hundredths of second to show this frame
                            Dim Bte() As Byte = BitConverter.GetBytes(FrameRate \ 10)
                            Data(x + 5) = Bte(0)
                            Data(x + 6) = Bte(1)
                        End If
                    End If
                End If
            End If
        Next
        stream.Write(Data, 0, Data.Length)
        }

    }
}
