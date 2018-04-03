using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Collections;
using System.Drawing.Imaging;

namespace Paint_Program
{
	public partial class Form1 : Form
	{
		Canvas c;

		public Form1()
		{
			InitializeComponent();

			KeyPreview = true;
			this.KeyDown += Form1_KeyDown;

			this.MinimumSize = new System.Drawing.Size(900, 677);

			CultureInfo ci = CultureInfo.CurrentUICulture;

			//Potential Issue is Computer Language isn't supported
			SharedSettings.languageFolderPath = @"..\..\Languages\";
			SharedSettings.language = ci.Name.ToString();

			PopulateLanguages();

			UpdateText();

			//Default Project
			MakeNewProject(500, 500);
		}

		private void PopulateLanguages()
		{
			string[] langs = Directory.GetFiles(SharedSettings.languageFolderPath);

			foreach (string s in langs)
			{
				//Check to make sure file is a resource file
				if (s.EndsWith(".resx"))
				{
					try
					{
						using (ResXResourceReader resxReader = new ResXResourceReader(s))
						{
							foreach (DictionaryEntry entry in resxReader)
							{
								if ((string)entry.Key == "resource_languagename")
								{
									Console.WriteLine("Adding: " + s + " to the list of available languages.");
									ToolStripMenuItem temp = new ToolStripMenuItem
									{
										Text = (string)entry.Value,
										Tag = Path.GetFileNameWithoutExtension(s)
									};
									temp.Click += delegate (object sender, EventArgs e)
									{
										//Sets language to the file name with no extension
										string filename = (string)((ToolStripMenuItem)sender).Tag;
										SharedSettings.language = filename;
										UpdateText();
									};
									tsmiInternational.DropDownItems.Add(temp);
								}
							}
						}
					}
					catch (Exception e)
					{
						Console.WriteLine("Error using the resource file " + s + ".\n\n" + e);
					}
				}
			}
		}

		private void UpdateText()
		{
			//Form Title
			this.Text = SharedSettings.getGlobalString("title");

			//File Menu Items
			tsmiFile.Text = SharedSettings.getGlobalString("file_menu");
			tsmiFile_New.Text = SharedSettings.getGlobalString("file_menu_new");
			tsmiFile_Save.Text = SharedSettings.getGlobalString("file_menu_save");
			tsmiFile_Load.Text = SharedSettings.getGlobalString("file_menu_open");
			tsmiFile_Import.Text = SharedSettings.getGlobalString("file_menu_import");
			tsmiFile_Export.Text = SharedSettings.getGlobalString("file_menu_export");
			tsmiFile_SaveGoogleDrive.Text = SharedSettings.getGlobalString("file_menu_googledrive");

			//Edit Menu Items
			tsmiEdit.Text = SharedSettings.getGlobalString("edit_menu");
			tsmiEdit_Redo.Text = SharedSettings.getGlobalString("edit_menu_redo");
			tsmiEdit_Undo.Text = SharedSettings.getGlobalString("edit_menu_undo");
			tsmiEdit_Resize.Text = SharedSettings.getGlobalString("edit_menu_resize");

			//View Menu Items
			tsmiView.Text = SharedSettings.getGlobalString("view_menu");
			tsmiView_GridLines.Text = SharedSettings.getGlobalString("view_menu_grid");
			tsmiView_GridLines_Auto.Text = SharedSettings.getGlobalString("view_menu_grid_auto");
			tsmiView_ShowTools.Text = SharedSettings.getGlobalString("view_menu_tools");

			//Preferences Menu Items
			tsmiPreferences.Text = SharedSettings.getGlobalString("preferences_menu");
			tsmiPreferences_Watermark.Text = SharedSettings.getGlobalString("preferences_menu_watermark");
			tsmiPreferences_Watermark_SetImage.Text = SharedSettings.getGlobalString("preferences_menu_watermark_set");
			tsmiPreferences_Watermark_ShowWatermark.Text = SharedSettings.getGlobalString("preferences_menu_watermark_show");
			tsmiPreferences_Watermark_Style.Text = SharedSettings.getGlobalString("preferences_menu_watermark_style");
			tsmiPreferences_Watermark_Style_SingleBottom.Text = SharedSettings.getGlobalString("preferences_menu_watermark_style_singlebottom");
			tsmiPreferences_Watermark_Style_SingleCentered.Text = SharedSettings.getGlobalString("preferences_menu_watermark_style_singlecentered");
			tsmiPreferences_Watermark_Style_Tiled.Text = SharedSettings.getGlobalString("preferences_menu_watermark_style_tiled");
			tsmiInternational.Text = SharedSettings.getGlobalString("preferences_menu_international");

			//ToolStrip Item ToolTips
			tsNew.ToolTipText = SharedSettings.getGlobalString("toolstripmenu_new");
			tsOpen.ToolTipText = SharedSettings.getGlobalString("toolstripmenu_import");
			tsSave.ToolTipText = SharedSettings.getGlobalString("toostripmenu_save");

			foreach (Control c in Controls)
			{
				if (c is ITextUpdate)
				{
					((ITextUpdate)c).UpdateText();
				}
			}

			this.Refresh();
		}

		private void TsmiFile_New_Click(object sender, EventArgs e)
		{

			using (NewProjectForm NewProjForm = new NewProjectForm())
			{
				if (NewProjForm.ShowDialog(this) == DialogResult.OK)
				{

					ClearControls();

					int w = NewProjForm.CanvasWidth;
					int h = NewProjForm.CanvasHeight;

					MakeNewProject(w, h);
				}
			}
		}

		private void ClearControls()
		{
			for (int i = this.Controls.Count - 1; i >= 0; i--)
			{
				if (!(this.Controls[i] == msMenu) && !(this.Controls[i] == toolStrip1)) //prevents menu destruction
				{
					this.Controls[i].Dispose(); //Needs to be Dispose or will leak memory
				}
			}
		}

		private void MakeNewProject(int w, int h)
		{
			if (c != null)
			{
				c.Trash(); //To help with memory leak issues
			}
			c = new Canvas(w, h, this.Width, this.Height)
			{
				Location = new Point(200, 5)
			};
			this.Controls.Add(c);
			c.InitCanvas();
		}

		private void MakeNewProject()
		{
			c = new Canvas(this.Width, this.Height)
			{
				Location = new Point(200, 5)
			};
			this.Controls.Add(c);
			c.InitCanvas();
			this.Update();
		}

		private void TsmiFile_Save_Click(object sender, EventArgs e)
		{
			//Save Project Function

			try
			{
				c.SetPause(true);
				ProjectSave ps = new ProjectSave();
			}
			catch (Exception err)
			{
				String s = SharedSettings.getGlobalString("error_save_project") + err.ToString();
				MessageBox.Show(new Form() { WindowState = FormWindowState.Maximized, TopMost = true }, s);
			}
			c.SetPause(false);
		}

		private void TsmiFile_Import_Click(object sender, EventArgs e)
		{
			//Import Image
			try
			{
				ImageImport ii = new ImageImport();
			}
			catch (Exception err)
			{
				Console.WriteLine(err.ToString());
			}
		}

		private void TsmiFile_Export_Click(object sender, EventArgs e)
		{
			//Export Image
			FileSave fs = new FileSave();

		}

		private void TsmiEdit_Undo_Click(object sender, EventArgs e)
		{
			//Undo Function
		}

		private void TsmiEdit_Redo_Click(object sender, EventArgs e)
		{
			//Redo Function
		}

		private void TsmiEdit_ImageSize_Click(object sender, EventArgs e)
		{
			//Resize Image Function
		}

		private void TsmiView_Tablet_Click(object sender, EventArgs e)
		{
			//Tablet Mode
		}

		private void TsmiView_ShowTools_Click(object sender, EventArgs e)
		{
			if (tsmiView_ShowTools.Checked)
			{
				c.ShowTools();
			}
			else
			{
				c.HideTools();
			}
		}

		private void TsmiFile_Load_Click(object sender, EventArgs e)
		{
			ProjectLoad pl = new ProjectLoad();

			if (SharedSettings.bLoadFromSettings == false)
			{
				return;
			}
			//SharedSettings.Trash();
			ClearControls();
			MakeNewProject();
		}

		#region GridLines
		private void TsmiView_GridLines_5_Click(object sender, EventArgs e)
		{

			if (tsmiView_GridLines_5.CheckState == CheckState.Unchecked)
			{
				SharedSettings.setGridToggle(false);
			}
			else
			{
				GridUncheck();
				tsmiView_GridLines_5.CheckState = CheckState.Checked;
				SharedSettings.setGridToggle(true);
				SharedSettings.setGridWidth(5);
			}
		}

		private void TsmiView_GridLines_10_Click(object sender, EventArgs e)
		{

			if (tsmiView_GridLines_10.CheckState == CheckState.Unchecked)
			{
				SharedSettings.setGridToggle(false);
			}
			else
			{
				GridUncheck();
				tsmiView_GridLines_10.CheckState = CheckState.Checked;
				SharedSettings.setGridToggle(true);
				SharedSettings.setGridWidth(10);
			}
		}

		private void TsmiView_GridLines_25_Click(object sender, EventArgs e)
		{

			if (tsmiView_GridLines_25.CheckState == CheckState.Unchecked)
			{
				SharedSettings.setGridToggle(false);
			}
			else
			{
				GridUncheck();
				tsmiView_GridLines_25.CheckState = CheckState.Checked;
				SharedSettings.setGridToggle(true);
				SharedSettings.setGridWidth(25);
			}
		}

		private void TsmiView_GridLines_50_Click(object sender, EventArgs e)
		{

			if (tsmiView_GridLines_50.CheckState == CheckState.Unchecked)
			{
				SharedSettings.setGridToggle(false);
			}
			else
			{
				GridUncheck();
				tsmiView_GridLines_50.CheckState = CheckState.Checked;
				SharedSettings.setGridToggle(true);
				SharedSettings.setGridWidth(50);
			}
		}

		private void TsmiView_GridLines_100_Click(object sender, EventArgs e)
		{

			if (tsmiView_GridLines_100.CheckState == CheckState.Unchecked)
			{
				SharedSettings.setGridToggle(false);
			}
			else
			{
				GridUncheck();
				tsmiView_GridLines_100.CheckState = CheckState.Checked;
				SharedSettings.setGridToggle(true);
				SharedSettings.setGridWidth(100);
			}
		}

		private void TsmiView_GridLines_Auto_Click(object sender, EventArgs e)
		{

			if (tsmiView_GridLines_Auto.CheckState == CheckState.Unchecked)
			{
				GridUncheck();
				tsmiView_GridLines_Auto.CheckState = CheckState.Checked;
				SharedSettings.setGridToggle(true);
				SharedSettings.setGridWidth(-1);
			}
			else
			{
				tsmiView_GridLines_Auto.CheckState = CheckState.Unchecked;
				SharedSettings.setGridToggle(false);

			}
		}

		private void GridUncheck()
		{
			tsmiView_GridLines_5.CheckState = CheckState.Unchecked;
			tsmiView_GridLines_10.CheckState = CheckState.Unchecked;
			tsmiView_GridLines_25.CheckState = CheckState.Unchecked;
			tsmiView_GridLines_50.CheckState = CheckState.Unchecked;
			tsmiView_GridLines_100.CheckState = CheckState.Unchecked;
			tsmiView_GridLines_Auto.CheckState = CheckState.Unchecked;
		}



		#endregion

		private void TsNew_Click(object sender, EventArgs e)
		{
			TsmiFile_New_Click(sender, e);
		}

		private void TsOpen_Click(object sender, EventArgs e)
		{
			TsmiFile_Load_Click(sender, e);
		}

		private void TsSave_Click(object sender, EventArgs e)
		{
			TsmiFile_Save_Click(sender, e);
		}

		private void TsImport_Click(object sender, EventArgs e)
		{
			TsmiFile_Import_Click(sender, e);
		}

		private void TsExport_Click(object sender, EventArgs e)
		{
			TsmiFile_Export_Click(sender, e);
		}

		private void SetImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog sfd = new OpenFileDialog
				{
					Filter = SharedSettings.getGlobalString("watermarkdialog_filter"),
					Title = SharedSettings.getGlobalString("watermarkdialog_title")
				};
				sfd.ShowDialog();

				SharedSettings.watermarkPath = sfd.FileName;
				SharedSettings.bitmapWatermark = new Bitmap(sfd.FileName);
			}
			catch (Exception err)
			{
				Console.WriteLine(err.InnerException);
			}

			if (SharedSettings.watermarkPath != null)
			{
				tsmiPreferences_Watermark_ShowWatermark.Enabled = true;
				tsmiPreferences_Watermark_Style.Enabled = true;
			}
		}

		private void ShowWatermarkToolStripMenuItem_Click(object sender, EventArgs e)
		{

			if (tsmiPreferences_Watermark_ShowWatermark.Checked == false)
			{

				SharedSettings.bitmapWatermark = (Bitmap)Image.FromFile(SharedSettings.watermarkPath);
				SharedSettings.bitmapWatermark.SetResolution(SharedSettings.iCanvasWidth, SharedSettings.iCanvasHeight);
				SharedSettings.bRenderWatermark = true;
				tsmiPreferences_Watermark_ShowWatermark.Checked = true;

			}
			else
			{
				SharedSettings.bRenderWatermark = false;
				tsmiPreferences_Watermark_ShowWatermark.Checked = false;
			}
		}

		private void TsmiPreferences_Watermark_SingleCenter_Click(object sender, EventArgs e)
		{
			SharedSettings.watermarkStyle = "Single Center";
			tsmiPreferences_Watermark_Style_SingleCentered.Checked = true;
			tsmiPreferences_Watermark_Style_SingleBottom.Checked = false;
			tsmiPreferences_Watermark_Style_Tiled.Checked = false;
		}

		private void TsmiPreferences_Watermark_SingleBottom_Click(object sender, EventArgs e)
		{
			SharedSettings.watermarkStyle = "Single Bottom";
			tsmiPreferences_Watermark_Style_SingleCentered.Checked = false;
			tsmiPreferences_Watermark_Style_SingleBottom.Checked = true;
			tsmiPreferences_Watermark_Style_Tiled.Checked = false;
		}

		private void TsmiPreferences_Watermark_Tiled_Click(object sender, EventArgs e)
		{
			SharedSettings.watermarkStyle = "Tiled";
			tsmiPreferences_Watermark_Style_SingleCentered.Checked = false;
			tsmiPreferences_Watermark_Style_SingleBottom.Checked = false;
			tsmiPreferences_Watermark_Style_Tiled.Checked = true;
		}

		private void TsmiFile_SaveGoogleDrive_Click(object sender, EventArgs e)
		{
			c.SetPause(true);
			using (GDriveSaveDialog gDrive = new GDriveSaveDialog())
			{
				if (gDrive.ShowDialog(this) == DialogResult.OK)
				{
					string fileName = gDrive.fileName;
					string fileType = gDrive.fileType;

					SaveToDrive sd = new SaveToDrive(fileName, fileType);
				}
			}
			c.SetPause(false);
		}

		public void UpdateViews()
		{
			if (c != null)
			{
				c.UpdatePositions(this);
			}
		}


		#region Shortcuts
		/// <summary>
		/// Handles key down events and processes keyboard shortcut combinations <br>
		/// <table>
		/// <tr><td>Command</td><td>Behavior</td></tr>
		/// <tr><td>CTRL + N</td><td>New Project</td></tr>
		/// <tr><td>CTRL + S</td><td>Save Project</td></tr>
		/// <tr><td>CTRL + O</td><td>Open Project</td></tr>
		/// <tr><td>CTRL + I</td><td>Import Image</td></tr>
		/// <tr><td>CTRL + E</td><td>Export Image</td></tr>
		/// <tr><td>CTRL + G</td><td>Export to Google Drive</td></tr>
		/// <tr><td>CTRL + C</td><td>Copy Selection</td></tr>
		/// <tr><td>CTRL + X</td><td>Cut Selection</td></tr>
		/// <tr><td>CTRL + V</td><td>Paste Image</td></tr>
		/// <tr><td>CTRL + =</td><td>Zoom In 10%</td></tr>
		/// <tr><td>CTRL + -</td><td>Zoom Out 10%</td></tr>
		/// </table>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			//CTRL + N for New Project
			if (e.Control && e.KeyCode == Keys.N)
			{
				TsmiFile_New_Click(this, null);
			}
			//CTRL + S for Save Project
			if (e.Control && e.KeyCode == Keys.S)
			{
				TsmiFile_Save_Click(this, null);
			}
			//CTRL + O for open Project
			if (e.Control && e.KeyCode == Keys.O)
			{
				TsmiFile_Load_Click(this, null);
			}
			//CTRL + I for Import Image
			if (e.Control && e.KeyCode == Keys.I)
			{
				TsmiFile_Import_Click(this, null);
			}
			//CTRL + E for Export Image
			if (e.Control && e.KeyCode == Keys.E)
			{
				TsmiFile_Export_Click(this, null);
			}
			//CTRL + G for Export to Google Drive
			if (e.Control && e.KeyCode == Keys.G)
			{
				TsmiFile_SaveGoogleDrive_Click(this, null);
			}
			//CTRL + C for Copy Selection
			if (e.Control && e.KeyCode == Keys.C)
			{
				if (SharedSettings.bitmapSelectionArea != null)
				{
					CopySelectionToClipboard();
				}
			}
			//CTRL + X for Cut Selection
			if (e.Control && e.KeyCode == Keys.X)
			{
				if (SharedSettings.bitmapSelectionArea != null)
				{
					CopySelectionToClipboard();
					SharedSettings.bitmapSelectionArea = null;
					SharedSettings.bRenderBitmapInterface = false;
					SharedSettings.bActiveSelection = false;
					SharedSettings.bFlattenSelection = true;
					SharedSettings.gActiveGraphics = SharedSettings.gActiveLayerGraphics;
				}
			}
			//CTRL + V for Paste Clipboard
			if (e.Control && e.KeyCode == Keys.V)
			{
				if (Clipboard.GetImage() != null)
				{
					SharedSettings.flattenSelection();
					Bitmap temp = (Bitmap)GetClipboardImage();
					Bitmap temp2 = new Bitmap(temp.Width, temp.Height, PixelFormat.Format32bppArgb);
					Graphics.FromImage(temp2).DrawImage(temp, 0, 0);
					temp.Dispose();
					SharedSettings.setSelection((Bitmap)temp2.Clone(), new Point(0, 0));
					temp.Dispose();
				}
			}
			//CTRL + = for zoom in
			if (e.Control && e.KeyCode == Keys.Oemplus)
			{
				if (c != null)
				{
					c.ZoomIn();
				}
			}
			//CTRL + - for zoom out
			if (e.Control && e.KeyCode == Keys.OemMinus)
			{
				if (c != null)
				{
					c.ZoomOut();
				}
			}
		}

		private void CopySelectionToClipboard()
		{
			IDataObject data = new DataObject();
			MemoryStream ms = new MemoryStream();
			SharedSettings.bitmapSelectionArea.Save(ms, ImageFormat.Png);
			data.SetData(DataFormats.Bitmap, SharedSettings.bitmapCurrentLayer);
			data.SetData("PNG", false, ms);

			Clipboard.Clear();
			Clipboard.SetImage(SharedSettings.bitmapCurrentLayer);
			Clipboard.SetDataObject(data, true);

		}

		private Image GetClipboardImage()
		{
			// Try to paste PNG data.
			if (Clipboard.ContainsData("PNG"))
			{
				Object png_object = Clipboard.GetData("PNG");
				if (png_object is MemoryStream)
				{
					MemoryStream png_stream = png_object as MemoryStream;
					return Image.FromStream(png_stream);
				}
			}

			// Try to paste bitmap data.
			if (Clipboard.ContainsImage())
			{
				return Clipboard.GetImage();
			}

			// We couldn't find anything useful. Return null.
			return null;
		}
		#endregion
	}
}
