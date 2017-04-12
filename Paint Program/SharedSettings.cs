using System;
using System.Collections;
using System.Drawing;
using System.Resources;

namespace Paint_Program
{
    public class SharedSettings
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

        public static Bitmap bitmapBrushTexture { get; set; }

        public static Graphics gActiveGraphics { get; set; }

        public static Graphics gActiveLayerGraphics { get; set; }

        public static String[] LayerNames { get; set; }

        public static Point pSelectionPoint { get; set; }

        public static Size sSelectionSize { get; set; }


        public SharedSettings()
        {
            cPrimaryBrushColor = Color.Black;

            cSecondaryBrushColor = Color.White;

            fBrushSize = 1.0F;

            iBrushHardness = 255;

            bGridToggle = false;

            watermarkStyle = "Single Center";

            //No Tablet Input
            iTabletPressure = -1;

            iMaxTabletWidth = 25;

            iMinTabletWidth = 1;

            //standard max pressure
            iMaxTabletPressure = 1023;
        }
        
        public void setPrimaryBrushColor(Color c)
        {
            cPrimaryBrushColor = c;
        }

        public void setSecondaryBrushColor(Color c)
        {
            cSecondaryBrushColor = c;
        }

        public void setBrushSize(float f)
        {
            fBrushSize = f;
        }

        public void setBrushHardness(int f)
        {
            iBrushHardness = f;
        }

        public void setTabletPressure(int p)
        {
            iTabletPressure = p;
        }

        public void setMaxTabletPressure(int p)
        {
            iMaxTabletPressure = p;
        }

        public void setMaxTabletWidth(int w)
        {
            iMaxTabletWidth = w;
        }

        public void setMinTabletWidth(int w)
        {
            iMinTabletWidth = w;
        }

        public void setCanvasWidth(int w)
        {
            iCanvasWidth = w;
        }

        public void setCanvasHeight(int h)
        {
            iCanvasHeight = h;
        }

        public void setGridWidth(int w)
        {
            iGridWidth = w;
        }

        public void setGridToggle(bool b)
        {
            bGridToggle = b;
        }

        public void setLoadFromSettings(bool b)
        {
            bLoadFromSettings = b;
        }

        public void setBitmapCanvas(Bitmap b)
        {
            bitmapCanvas = b;
        }

        public void setImportImage(Bitmap b)
        {
            bitmapImportImage = b;
        }

        public void setBitmapCurrentLayer(Bitmap b)
        {
            bitmapCurrentLayer = b;
        }

        public void setLayerBitmaps(Bitmap[] bitArr)
        {
            Layers = new Bitmap[bitArr.Length];
            for(int n = 0; n < bitArr.Length; n++)
            {
                Bitmap temp = (Bitmap)bitArr[n].Clone();
                Layers[n] = temp;
            }
        }

        public void setLayerNames(String[] names)
        {
            LayerNames = names;
        }

        public void setDrawScale(float s)
        {
            fScale = s;
        }

        public void setSelectionPoint(Point p)
        {
            pSelectionPoint = p;
        }

        public void setSelectionSize(Size s)
        {
            sSelectionSize = s;
        }

        public void setInterfaceBitmap(Bitmap b)
        {
            bitmapInterface = b;
        }

        public void setRenderBitmapInterface(bool b)
        {
            bRenderBitmapInterface = b;
        }

        public void setBitmapSelectionArea(Bitmap b)
        {
            bitmapSelectionArea = b;
        }

        public void setActiveGraphics(Graphics g)
        {
            gActiveGraphics = g;
            if (bActiveSelection)
            {
                bActiveSelection = false;
            }
        }

        public void setActiveSelection(bool b)
        {
            bActiveSelection = b;
        }

        public void setFlattenSelection(bool b)
        {
            bFlattenSelection = b;
        }

        public void setCurrentLayerIndex(int i)
        {
            icurentLayerIndex = i;
        }

        public void setTabletConnected(bool b)
        {
            bTabletConnected = b;
        }

        public void setActiveLayerGraphics(Graphics g)
        {
            gActiveLayerGraphics = g;
        }

        public void setBitmapLayerUpdate(Bitmap b)
        {
            bitmapLayerUpdate = b;
        }

        public void setGreenScreenTolerance(int i)
        {
            iGreenScreenTolerance = i;
        }

        public void setLanguageFolderPath(string s)
        {
            languageFolderPath = s;
        }

        public void setLanguage(string s)
        {
            language = s;
        }




        public Color getPrimaryBrushColor()
        {
            return cPrimaryBrushColor;
        }

        public Color getSecondaryBrushColor()
        {
            return cSecondaryBrushColor;
        }

        public float getBrushSize()
        {
            return fBrushSize;
        }

        public int getBrushHardness()
        {
            return iBrushHardness;
        }

        public int getTabletPressure()
        {
            return iTabletPressure;
        }

        public int getMaxTabletPressure()
        {
            return iMaxTabletPressure;
        }

        public int getMaxTabletWidth()
        {
            return iMaxTabletWidth;
        }

        public int getMinTabletWidth()
        {
            return iMinTabletWidth;
        }

        public int getCanvasWidth()
        {
            return iCanvasWidth;
        }

        public int getCanvasHeight()
        {
            return iCanvasHeight;
        }

        public int getGridWitdh()
        {
            return iGridWidth;
        }

        public bool getGridToggle()
        {
            return bGridToggle;
        }

        public bool getLoadFromSettings()
        {
            return bLoadFromSettings;
        }

        public Bitmap getBitmapCanvas()
        {
            return bitmapCanvas;
        }

        public Bitmap getImportImage()
        {
            Bitmap tmp = bitmapImportImage;
            bitmapImportImage = null;
            return tmp;
        }

        public Bitmap getBitmapCurrentLayer(bool source)
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

        public Bitmap[] getLayerBitmaps()
        {
            return Layers;
        }

        public String[] getLayerNames()
        {
            return LayerNames;
        }

        public float getDrawScale()
        {
            return fScale;
        }

        public Point getSelectionPoint()
        {
            return pSelectionPoint;
        }

        public Size getSelectionSize()
        {
            return sSelectionSize;
        }

        public Bitmap getInterfaceBitmap()
        {
            return bitmapInterface;
        }

        public bool getRenderBitmapInterface()
        {
            return bRenderBitmapInterface;
        }

        public Bitmap getBitmapSelectionArea()
        {
            return bitmapSelectionArea;
        }

        public Graphics getActiveGraphics()
        {
            return gActiveGraphics;
        }

        public bool getActiveSelection()
        {
            return bActiveSelection;
        }

        public bool getFlattenSelection()
        {
            return bFlattenSelection;
        }

        public int getCurrentLayerIndex()
        {
            return icurentLayerIndex;
        }

        public bool getTabletconnected()
        {
            return bTabletConnected;
        }

        public Graphics getActiveLayerGraphics()
        {
            return gActiveLayerGraphics;
        }

        public Bitmap getBitmapLayerUpdate()
        {
            return bitmapLayerUpdate;
        }

        public int getGreenScreenTolerance()
        {
            return iGreenScreenTolerance;
        }

        public string getLanguageFolderPath()
        {
            return languageFolderPath;
        }




        public void scrubSelection()
        {
            setActiveSelection(false);
            setFlattenSelection(false);
            setSelectionPoint(new Point(-1,-1));
            setSelectionSize(new Size(-1, -1));
            setActiveGraphics(getActiveLayerGraphics());
            setBitmapSelectionArea(null);
            Console.WriteLine("Scrubed Selection Info. . .");
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

        public void Trash()
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

            if(bitmapImportImage != null)
            {
                bitmapInterface.Dispose();
            }
            if(bitmapBrushTexture != null)
            {
                bitmapBrushTexture.Dispose();
            }
        }
    }
}
