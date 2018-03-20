using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
	class GreenScreenTool : ITool
	{
		private Graphics graphics;
		private int width, height;
		private bool bActive, bMouseDown, bInit;

		private Bitmap bLayer;

		public void init()
		{
			graphics = SharedSettings.getActiveGraphics();
			width = SharedSettings.getCanvasWidth();
			height = SharedSettings.getCanvasHeight();
			bActive = false;
			bInit = true;
			bMouseDown = false;
		}

		public Bitmap getCanvas()
		{
			//Not used
			return null;
		}

		public string getToolIconPath()
		{
			return @"..\..\Images\greenscreen.png";
		}

		public void onMouseDown(object sender, MouseEventArgs e)
		{
			if (SharedSettings.getBitmapCurrentLayer(true) != null)
			{
				Color c = SharedSettings.getBitmapCurrentLayer(true).GetPixel(e.X, e.Y);

				Console.WriteLine("Color: " + c.ToString());

				if (e.Button == MouseButtons.Left)
				{
					Bitmap Temp = SharedSettings.getBitmapCurrentLayer(true);

					for (int x = 0; x < Temp.Width; x++)
					{
						for (int y = 0; y < Temp.Height; y++)
						{
							Color tmp = Temp.GetPixel(x, y);
							//Console.WriteLine("Distance: " + Distance(tmp.R, c.R, tmp.G, c.G, tmp.B, c.B) + " -- " + SharedSettings.getGreenScreenTolerance());
							if (Distance(tmp.R, tmp.G, tmp.B, c.R, c.G, c.B) <= SharedSettings.getGreenScreenTolerance())
							{
								Temp.SetPixel(x, y, Color.Transparent);
							}
						}
					}

					/* Cringe City, please ignore
					for (int r = -tol; r < tol; r++)
					{
						for (int g = -tol; g < tol; g++)
						{
							for (int b = -tol; b < tol; b++)
							{
								if (!((c.R + r) < 0 || (c.G + g) < 0 || (c.B + b) < 0 || (c.R + r) > 255 || (c.G + g) > 255 || (c.B + b) > 255))
								{
									Color tmpColor = Color.FromArgb(c.R + r, c.G + g, c.B + b);
									
									for (int x = 0; x < Temp.Width; x++)
									{
										for (int y = 0; y < Temp.Height; y++)
										{
											Color tmp = Temp.GetPixel(x, y);
											if (tmp.R == tmpColor.R && tmp.G == tmpColor.G && tmp.B == tmpColor.B)
											{
												Temp.SetPixel(x, y, Color.Transparent);
											}
										}
									}
								}
							}
						}
					}*/
				}
			}
		}

		public void onMouseMove(object sender, MouseEventArgs e)
		{

		}

		public void onMouseUp(object sender, MouseEventArgs e)
		{
			if (graphics != null)
			{
				bMouseDown = false;

			}
		}

		public bool isInitalized()
		{
			return bInit;
		}

		public Bitmap getToolLayer()
		{
			return null;
		}

		public bool requiresLayerData()
		{
			return true;
		}

		public void setLayerData(Bitmap bit)
		{
			bLayer = bit;
		}

		public string getToolTip()
		{
			return SharedSettings.getGlobalString("tooltip_greenscreen");
		}
		public void updateInterfaceLayer()
		{
		}

		private double Distance(int x1, int y1, int z1, int x2, int y2, int z2) {
			return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) + Math.Pow(z2 - z1, 2));
		} 
	}
}
