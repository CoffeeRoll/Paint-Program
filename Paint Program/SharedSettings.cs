using System;
using System.Collections;
using System.Drawing;
using System.Resources;

namespace Paint_Program
{
    public static class SharedSettings
    {
        public static Color cPrimaryBrushColor { get; set; }

        public static Color cSecondaryBrushColor { get; set; }

        public static float fBrushSize { get; set; }

        public static float fScale { get; set; }

        public static int iBrushHardness { get; set; }

        public static int iTabletPressure { get; set; }

        public static int iMaxTabletPressure { get; set; }

        public static int iMaxTabletWidth { get; set; }

        public static int iMinTabletWidth { get; set; }

        public static int iCanvasWidth { get; set; }

        public static int iCanvasHeight { get; set; }

        public static int iGridWidth { get; set; }

        public static int icurentLayerIndex { get; set; }

        public static int iGreenScreenTolerance { get; set; }

        public static bool bGridToggle { get; set; }

        public static bool bLoadFromSettings { get; set; }

        public static bool bRenderBitmapInterface { get; set; }

        public static bool bRenderWatermark { get; set; }

        public static bool bActiveSelection { get; set; }

        public static bool bFlattenSelection { get; set; }

        public static bool bTabletConnected { get; set; }

        public static string watermarkPath { get; set; }

        public static string watermarkStyle { get; set; }

        public static string languageFolderPath { get; set; }

        public static string language { get; set; }

        public static Bitmap bitmapCanvas { get; set; }

        public static Bitmap bitmapCurrentLayer { get; set; }

        public static Bitmap bitmapImportImage { get; set; }

        public static Bitmap bitmapInterface { get; set; }

        public static Bitmap bitmapSelectionArea { get; set; }

        public static Bitmap bitmapWatermark { get; set; }

        public static Bitmap[] Layers { get; set; }

        public static Bitmap bitmapLayerUpdate { get; set; }

        public static Graphics gActiveGraphics { get; set; }

        public static Graphics gActiveLayerGraphics { get; set; }

        public static String[] LayerNames { get; set; }

        public static Point pSelectionPoint { get; set; }

        public static Size sSelectionSize { get; set; }


        public static void Init()
        {
            cPrimaryBrushColor = Color.Black;

            cSecondaryBrushColor = Color.White;

            fBrushSize = 1.0F;

            iBrushHardness = 255;

            bGridToggle = false;

            //watermarkStyle = "Single Center";

            //No Tablet Input
            iTabletPressure = -1;

            iMaxTabletWidth = 128;

            iMinTabletWidth = 1;

            //standard max pressure
            iMaxTabletPressure = 1023;

			//Sharp green screen tolerance
			iGreenScreenTolerance = 1;

			fBrushSize = 1.0f;
        }
        
        public static void setPrimaryBrushColor(Color c)
        {
            cPrimaryBrushColor = c;
        }

        public static void setSecondaryBrushColor(Color c)
        {
            cSecondaryBrushColor = c;
        }

        public static void setBrushSize(float f)
        {
            fBrushSize = f;
        }

        public static void setBrushHardness(int f)
        {
            iBrushHardness = f;
        }

        public static void setTabletPressure(int p)
        {
            iTabletPressure = p;
        }

        public static void setMaxTabletPressure(int p)
        {
            iMaxTabletPressure = p;
        }

        public static void setMaxTabletWidth(int w)
        {
            iMaxTabletWidth = w;
        }

        public static void setMinTabletWidth(int w)
        {
            iMinTabletWidth = w;
        }

        public static void setCanvasWidth(int w)
        {
            iCanvasWidth = w;
        }

        public static void setCanvasHeight(int h)
        {
            iCanvasHeight = h;
        }

        public static void setGridWidth(int w)
        {
            iGridWidth = w;
        }

        public static void setGridToggle(bool b)
        {
            bGridToggle = b;
        }

        public static void setLoadFromSettings(bool b)
        {
            bLoadFromSettings = b;
        }

        public static void setBitmapCanvas(Bitmap b)
        {
            bitmapCanvas = b;
        }

        public static void setImportImage(Bitmap b)
        {
            bitmapImportImage = b;
        }

        public static void setBitmapCurrentLayer(Bitmap b)
        {
            bitmapCurrentLayer = b;
        }

        public static void setLayerBitmaps(Bitmap[] bitArr)
        {
            Layers = new Bitmap[bitArr.Length];
            for(int n = 0; n < bitArr.Length; n++)
            {
                Bitmap temp = (Bitmap)bitArr[n].Clone();
                Layers[n] = temp;
            }
        }

        public static void setLayerNames(String[] names)
        {
            LayerNames = names;
        }

        public static void setDrawScale(float s)
        {
            fScale = s;
        }

        public static void setSelectionPoint(Point p)
        {
            pSelectionPoint = p;
        }

        public static void setSelectionSize(Size s)
        {
            sSelectionSize = s;
        }

        public static void setInterfaceBitmap(Bitmap b)
        {
            bitmapInterface = b;
        }

        public static void setRenderBitmapInterface(bool b)
        {
            bRenderBitmapInterface = b;
        }

        public static void setBitmapSelectionArea(Bitmap b)
        {
            bitmapSelectionArea = b;
        }

        public static void setActiveGraphics(Graphics g)
        {
            gActiveGraphics = g;
            if (bActiveSelection)
            {
                bActiveSelection = false;
            }
        }

        public static void setActiveSelection(bool b)
        {
            bActiveSelection = b;
        }

        public static void setFlattenSelection(bool b)
        {
            bFlattenSelection = b;
        }

        public static void setCurrentLayerIndex(int i)
        {
            icurentLayerIndex = i;
        }

        public static void setTabletConnected(bool b)
        {
            bTabletConnected = b;
        }

        public static void setActiveLayerGraphics(Graphics g)
        {
            gActiveLayerGraphics = g;
        }

        public static void setBitmapLayerUpdate(Bitmap b)
        {
            bitmapLayerUpdate = b;
        }

        public static void setGreenScreenTolerance(int i)
        {
            iGreenScreenTolerance = i;
        }

        public static void setLanguageFolderPath(string s)
        {
            languageFolderPath = s;
        }

        public static void setLanguage(string s)
        {
            language = s;
        }




        public static Color getPrimaryBrushColor()
        {
            return cPrimaryBrushColor;
        }

        public static Color getSecondaryBrushColor()
        {
            return cSecondaryBrushColor;
        }

        public static float getBrushSize()
        {
			//Cannot return a brush size of 0;
            return fBrushSize <= 0 ? 1.0f : fBrushSize;
        }

        public static int getBrushHardness()
        {
			//Cannot have 0 hardness
            return iBrushHardness <= 0 ? 1 : iBrushHardness;
        }

        public static int getTabletPressure()
        {
            return iTabletPressure;
        }

        public static int getMaxTabletPressure()
        {
            return iMaxTabletPressure;
        }

        public static int getMaxTabletWidth()
        {
            return iMaxTabletWidth;
        }

        public static int getMinTabletWidth()
        {
            return iMinTabletWidth;
        }

        public static int getCanvasWidth()
        {
            return iCanvasWidth;
        }

        public static int getCanvasHeight()
        {
            return iCanvasHeight;
        }

        public static int getGridWitdh()
        {
            return iGridWidth;
        }

        public static bool getGridToggle()
        {
            return bGridToggle;
        }

        public static bool getLoadFromSettings()
        {
            return bLoadFromSettings;
        }

        public static Bitmap getBitmapCanvas()
        {
            return bitmapCanvas;
        }

        public static Bitmap getImportImage()
        {
            Bitmap tmp = bitmapImportImage;
            bitmapImportImage = null;
            return tmp;
        }

        public static Bitmap getBitmapCurrentLayer(bool source)
        {
            if (source)
            {
                return bitmapCurrentLayer;
            }
            else
            {
                return(Bitmap) bitmapCurrentLayer.Clone();
            }
        }

        public static Bitmap[] getLayerBitmaps()
        {
            return Layers;
        }

        public static String[] getLayerNames()
        {
            return LayerNames;
        }

        public static float getDrawScale()
        {
            return fScale;
        }

        public static Point getSelectionPoint()
        {
            return pSelectionPoint;
        }

        public static Size getSelectionSize()
        {
            return sSelectionSize;
        }

        public static Bitmap getInterfaceBitmap()
        {
            return bitmapInterface;
        }

        public static bool getRenderBitmapInterface()
        {
            return bRenderBitmapInterface;
        }

        public static Bitmap getBitmapSelectionArea()
        {
            return bitmapSelectionArea;
        }

        public static Graphics getActiveGraphics()
        {
            return gActiveGraphics;
        }

        public static bool getActiveSelection()
        {
            return bActiveSelection;
        }

        public static bool getFlattenSelection()
        {
            return bFlattenSelection;
        }

        public static int getCurrentLayerIndex()
        {
            return icurentLayerIndex;
        }

        public static bool getTabletconnected()
        {
            return bTabletConnected;
        }

        public static Graphics getActiveLayerGraphics()
        {
            return gActiveLayerGraphics;
        }

        public static Bitmap getBitmapLayerUpdate()
        {
            return bitmapLayerUpdate;
        }

        public static int getGreenScreenTolerance()
        {
            return iGreenScreenTolerance;
        }

        public static string getLanguageFolderPath()
        {
            return languageFolderPath;
        }


        public static void setSelection(Bitmap bit, Point p)
        {
            SharedSettings.bitmapSelectionArea = bit;
            SharedSettings.sSelectionSize = new Size(SharedSettings.bitmapSelectionArea.Width, SharedSettings.bitmapSelectionArea.Height);
            SharedSettings.pSelectionPoint = p;
            SharedSettings.bActiveSelection = true;
            SharedSettings.bFlattenSelection = false;
            SharedSettings.bRenderBitmapInterface = true;
            SharedSettings.bitmapCurrentLayer = SharedSettings.bitmapSelectionArea;
            SharedSettings.gActiveGraphics = Graphics.FromImage(SharedSettings.bitmapCurrentLayer);
        }

        public static void flattenSelection()
        {
            bRenderBitmapInterface = false;
            if (bActiveSelection)
            {
                bActiveSelection = false;
                bFlattenSelection = true;
                gActiveLayerGraphics.DrawImage(bitmapSelectionArea, pSelectionPoint);
                gActiveGraphics = gActiveLayerGraphics;
            }
        }

        public static void scrubSelection()
        {
            setActiveSelection(false);
            setFlattenSelection(false);
            setSelectionPoint(new Point(-1,-1));
            setSelectionSize(new Size(-1, -1));
            setActiveGraphics(getActiveLayerGraphics());
            setBitmapSelectionArea(null);
            Console.WriteLine("Scrubbed Selection Info. . .");
        }


        public static string getGlobalString(string key)
        {
            try {
                using (ResXResourceReader resxReader = new ResXResourceReader(languageFolderPath + "\\" + language + ".resx"))
                {
                    foreach (DictionaryEntry entry in resxReader)
                    {
                        if ((string)entry.Key == key)
                        {
                            return (string)entry.Value;
                        }
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine("Error using the language " + language + ".\n" + e);
            }
            return "";
        }

        public static int MapValue(
    int originalStart, int originalEnd, // original range
    int newStart, int newEnd, // desired range
    int value) // value to convert
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (int)(newStart + ((value - originalStart) * scale));
        }

        public static double MapDouble(
    double originalStart, double originalEnd, // original range
    double newStart, double newEnd, // desired range
    double value) // value to convert
        {
            double scale = (newEnd - newStart) / (originalEnd - originalStart);
            return (newStart + ((value - originalStart) * scale));
        }

        public static void Trash()
        {

            //Disposes all Bitmap references to reduce memory leaks
            foreach(Bitmap b in Layers)
            {
                b.Dispose();
            }
            setLoadFromSettings(false);

            if (bitmapCanvas != null)
            {
                bitmapCanvas.Dispose();
            }

            if (bitmapCurrentLayer != null)
            {
                bitmapCurrentLayer.Dispose();
            }

            if (bitmapImportImage != null)
            {
                bitmapImportImage.Dispose();
            }

            if (bitmapInterface != null)
            {
                bitmapInterface.Dispose();
            }

            if (bitmapWatermark != null)
            {
                bitmapWatermark.Dispose();
            }
            bRenderBitmapInterface = false;
            bRenderWatermark = false;
            bLoadFromSettings = false;
            bActiveSelection = false;
            bFlattenSelection = false;
            watermarkPath = "";
        }
    }
}
