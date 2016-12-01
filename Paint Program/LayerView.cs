using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Paint_Program
{
    public partial class LayerView : UserControl
    {

        private List<LayerItem> Layers;
        private PixelFormat pf = PixelFormat.Format32bppArgb;
        private int width, height;
        private int yLayerLocation;
        private SharedSettings ss;

        public LayerView(int w, int h, SharedSettings s)
        {
            InitializeComponent();
            ss = s;
            width = w;
            height = h;
            yLayerLocation = 0;
            LayerItem setup = new LayerItem(w, h, pf, "DEBUG");
            this.Width = setup.Width;
            pLayerDisplay.Scroll += handleScroll;
            pLayerDisplay.MouseWheel += handleMouseWheel;
            Layers = new List<LayerItem>();

            if (ss.getLoadFromSettings() && ss.getLayerBitmaps().Length > 0)
            {

                int layers = ss.getLayerBitmaps().Length;

                for (int n = 0; n < layers; n++)
                {
                    addLayer(ss.getLayerBitmaps()[n], ss.getLayerNames()[n]);
                }
            }
            else
            {
                addLayer();
                ss.setBitmapCurrentLayer(Layers[0].getBitmap());
            }
            redrawLayerItems();
            UpdateLayerInfo();
        }

        private void handleMouseWheel(object sender, MouseEventArgs e)
        {
            redrawLayerItems();
        }

        private void handleScroll(object sender, ScrollEventArgs e)
        {
            redrawLayerItems();
        }

        public void updateActiveLayer()
        {
            //Foreach loop to iterate through all layers
            foreach (LayerItem layer in Layers)
            {
                if (layer.isLayerActive())
                {
                    layer.updatePreview();
                    layer.Refresh();
                }
            }
        }

        public Bitmap getRender()
        {
            Bitmap bit = new Bitmap(width, height, pf);
            Graphics g = Graphics.FromImage(bit);

            for (int t = 0; t < Layers.Count; t++)
            {
                try
                {
                    if (Layers[t].isLayerVisible())
                    {
                        g.DrawImage(Layers[t].getBitmap(), 0, 0);
                
                    }
                }catch(Exception e)
                {
                    Console.WriteLine("Exception in getRender " + e.InnerException);
                }
            }

            return bit;
        }

        public Graphics getActiveLayerGraphics()
        {
            //Searches for the active layer
            foreach(LayerItem layer in Layers)
            {
                if (layer.isLayerActive())
                {
                    return layer.getGraphics();
                }
            }

            //No Active Layer
            return null;
        }

        public Bitmap getActiveLayerBitmap()
        {
            //Searches for the active layer
            foreach (LayerItem layer in Layers)
            {
                if (layer.isLayerActive())
                {
                    return layer.getBitmap();
                }
            }

            //No Active Layer
            return null;
        }

        private int getActiveLayerIndex()
        {
            for(int t = 0; t <= Layers.Count; t++)
            {
                if (Layers[t].isLayerActive())
                {
                    return t;
                }
            }
            return -1;
        }

        private void bAddLayer_Click(object sender, EventArgs e)
        {
            addLayer();
            UpdateLayerInfo();
        }

        private void bRemoveLayer_Click(object sender, EventArgs e)
        {
            removeLayer();
            Layers[Layers.Count - 1].setActive(true);
            UpdateLayerInfo();
        }

        public void UpdateLayerInfoListener()
        {
            UpdateLayerInfo();
        }

        public void addImportImage(Bitmap b)
        {
            foreach (LayerItem layer in Layers)
            {
                layer.setActive(false);
            }

            LayerItem temp = new LayerItem(width, height, pf, Layers.Count.ToString());
            temp.Location = new Point(0, yLayerLocation);
            yLayerLocation += temp.Height + 5;
            temp.setActive(true);
            temp.setOnClick(handleLayerItemClick);
            temp.setBitmap((Bitmap)b.Clone());
            Layers.Add(temp);
            pLayerDisplay.Controls.Add(Layers[Layers.Count - 1]);

            if (Layers.Count > 1)
            {
                bRemoveLayer.Enabled = true;
                bMoveDown.Enabled = true;
                bMoveUp.Enabled = true;
            }

            redrawLayerItems();
        }

        private void addLayer()
        {

            foreach(LayerItem layer in Layers)
            {
                layer.setActive(false);
            }

            LayerItem temp = new LayerItem(width, height, pf, Layers.Count.ToString());
            temp.Location = new Point(0, yLayerLocation);
            yLayerLocation += temp.Height + 5;
            temp.setActive(true);
            temp.setOnClick(handleLayerItemClick);
            
            Layers.Add(temp);
            pLayerDisplay.Controls.Add(Layers[Layers.Count - 1]);

            if (Layers.Count > 1)
            {
                bRemoveLayer.Enabled = true;
                bMoveDown.Enabled = true;
                bMoveUp.Enabled = true;
            }

            redrawLayerItems();
        }

        private void addLayer(Bitmap b, String name)
        {

            foreach (LayerItem layer in Layers)
            {
                layer.setActive(false);
            }

            LayerItem temp = new LayerItem(width, height, pf, name);
            temp.Location = new Point(0, yLayerLocation);
            yLayerLocation += temp.Height + 5;
            temp.setActive(true);
            temp.setOnClick(handleLayerItemClick);
            temp.setBitmap(b);

            Layers.Add(temp);
            pLayerDisplay.Controls.Add(Layers[Layers.Count - 1]);

            if (Layers.Count > 1)
            {
                bRemoveLayer.Enabled = true;
                bMoveDown.Enabled = true;
                bMoveUp.Enabled = true;
            }
        }

        private void removeLayer()
        {
            //Foreach loop to iterate through all layers
            try
            {
                for (int t = Layers.Count - 1; t >= 0; t--)
                {
                    //If there's only one layer, don't remove it
                    if (Layers.Count <= 1)
                    {
                        break;
                    }
                    if (Layers[t].isLayerActive())
                    {
                        //removes the LayerItem from the Display
                        pLayerDisplay.Controls.Remove(Layers[t]);

                        //Removes selected layer from the List
                        Layers.Remove(Layers[t]);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.InnerException);
            }
            redrawLayerItems();

            //Disable the Relevant Buttons if only one layer Exists
            bRemoveLayer.Enabled = (Layers.Count > 1);
            bMoveDown.Enabled = (Layers.Count > 1);
            bMoveUp.Enabled = (Layers.Count > 1);
        }

        private void redrawLayerItems()
        {

            //reset index
            yLayerLocation = 0;

            //Redraws in reverse so most recent layer is always on top of the list
            for(int t = Layers.Count - 1; t >= 0; t--)
            {

                //adds AutoScrollPosition.Y to accomodate for vertical scrolling
                Layers[t].Location = new Point(pLayerDisplay.AutoScrollPosition.X, yLayerLocation + pLayerDisplay.AutoScrollPosition.Y);
                yLayerLocation += Layers[t].Height + 5;
                
            }
            UpdateLayerInfo();
            this.Refresh();
        }

        private void bMoveDown_Click(object sender, EventArgs e)
        {
            int i = getActiveLayerIndex();

            if(i != -1 && i != 0)
            {
                LayerItem temp = Layers[i];
                Layers[i] = Layers[i - 1];
                Layers[i - 1] = temp;
            }
            redrawLayerItems();
        }

        private void bMoveUp_Click(object sender, EventArgs e)
        {
            int i = getActiveLayerIndex();

            if (i != -1 && i != Layers.Count -1)
            {
                LayerItem temp = Layers[i];
                Layers[i] = Layers[i + 1];
                Layers[i + 1] = temp;
            }
            redrawLayerItems();
        }

        private void handleLayerItemClick(object obj, System.EventArgs args)
        {
            if (obj is LayerItem)
            {
                
                LayerItem layer = ((LayerItem)obj);
                if (layer.isLayerActive())
                {
                    layer.setActive(false);
                }
                else
                {
                    foreach (LayerItem l in Layers)
                    {
                        l.setActive(false);
                    }
                    layer.setActive(true);
                    ss.setBitmapCurrentLayer(layer.getBitmap());
                }
                layer.Refresh();
            }
            else if (obj is PictureBox)
            {
                LayerItem layer = (LayerItem)((PictureBox)obj).Parent;
                if (layer.isLayerActive())
                {
                    layer.setActive(false);
                }
                else
                {
                    foreach (LayerItem l in Layers)
                    {
                        l.setActive(false);
                        l.Refresh();
                    }
                    layer.setActive(true);
                }
                layer.Refresh();
            }
            redrawLayerItems();
        }

        private void UpdateLayerInfo()
        {
            List<Bitmap> tempBit = new List<Bitmap>();
            List<String> tempStr = new List<String>();

            foreach (LayerItem l in Layers)
            {
                tempBit.Add(l.getBitmap());
                tempStr.Add(l.getLayerName());
            }

            ss.setLayerBitmaps(tempBit.ToArray());
            ss.setLayerNames(tempStr.ToArray());

        }

        public void GridDraw(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            int yMod = ss.getCanvasHeight();
            int xMod = ss.getCanvasWidth();

          //  if(ss.getGridToggle())
           // {
                for (int y = 5; y < yMod; y+=5)
                    g.DrawLine(p, 0, y, yMod, y);
                for (int x = 1; x < xMod; x+=5)
                    g.DrawLine(p, x , 0, x , xMod);
           // }

        }

    }
}
