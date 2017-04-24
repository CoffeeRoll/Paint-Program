﻿namespace Paint_Program
{
    partial class LayerView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bAddLayer = new System.Windows.Forms.Button();
            this.bRemoveLayer = new System.Windows.Forms.Button();
            this.pLayerDisplay = new System.Windows.Forms.Panel();
            this.bMoveDown = new System.Windows.Forms.Button();
            this.bMoveUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bAddLayer
            // 
            this.bAddLayer.Location = new System.Drawing.Point(2, 2);
            this.bAddLayer.Margin = new System.Windows.Forms.Padding(2);
            this.bAddLayer.Name = "bAddLayer";
            this.bAddLayer.Size = new System.Drawing.Size(27, 26);
            this.bAddLayer.TabIndex = 0;
            this.bAddLayer.Text = "+";
            this.bAddLayer.UseVisualStyleBackColor = true;
            this.bAddLayer.Click += new System.EventHandler(this.bAddLayer_Click);
            // 
            // bRemoveLayer
            // 
            this.bRemoveLayer.Location = new System.Drawing.Point(33, 2);
            this.bRemoveLayer.Margin = new System.Windows.Forms.Padding(2);
            this.bRemoveLayer.Name = "bRemoveLayer";
            this.bRemoveLayer.Size = new System.Drawing.Size(27, 26);
            this.bRemoveLayer.TabIndex = 1;
            this.bRemoveLayer.Text = "-";
            this.bRemoveLayer.UseVisualStyleBackColor = true;
            this.bRemoveLayer.Click += new System.EventHandler(this.bRemoveLayer_Click);
            // 
            // pLayerDisplay
            // 
            this.pLayerDisplay.AutoScroll = true;
            this.pLayerDisplay.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pLayerDisplay.Location = new System.Drawing.Point(0, 32);
            this.pLayerDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.pLayerDisplay.Name = "pLayerDisplay";
            this.pLayerDisplay.Size = new System.Drawing.Size(240, 326);
            this.pLayerDisplay.TabIndex = 2;
            // 
            // bMoveDown
            // 
            this.bMoveDown.Enabled = false;
            this.bMoveDown.Location = new System.Drawing.Point(105, 2);
            this.bMoveDown.Margin = new System.Windows.Forms.Padding(2);
            this.bMoveDown.Name = "bMoveDown";
            this.bMoveDown.Size = new System.Drawing.Size(27, 26);
            this.bMoveDown.TabIndex = 3;
            this.bMoveDown.Text = "▽";
            this.bMoveDown.UseVisualStyleBackColor = true;
            this.bMoveDown.Click += new System.EventHandler(this.bMoveDown_Click);
            // 
            // bMoveUp
            // 
            this.bMoveUp.Enabled = false;
            this.bMoveUp.Location = new System.Drawing.Point(136, 2);
            this.bMoveUp.Margin = new System.Windows.Forms.Padding(2);
            this.bMoveUp.Name = "bMoveUp";
            this.bMoveUp.Size = new System.Drawing.Size(27, 26);
            this.bMoveUp.TabIndex = 4;
            this.bMoveUp.Text = "△";
            this.bMoveUp.UseVisualStyleBackColor = true;
            this.bMoveUp.Click += new System.EventHandler(this.bMoveUp_Click);
            // 
            // LayerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Controls.Add(this.bMoveUp);
            this.Controls.Add(this.bMoveDown);
            this.Controls.Add(this.pLayerDisplay);
            this.Controls.Add(this.bRemoveLayer);
            this.Controls.Add(this.bAddLayer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LayerView";
            this.Size = new System.Drawing.Size(240, 357);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bAddLayer;
        private System.Windows.Forms.Button bRemoveLayer;
        private System.Windows.Forms.Panel pLayerDisplay;
        private System.Windows.Forms.Button bMoveDown;
        private System.Windows.Forms.Button bMoveUp;
    }
}
