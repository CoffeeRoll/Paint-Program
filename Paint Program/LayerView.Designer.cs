namespace Paint_Program
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
            this.SuspendLayout();
            // 
            // bAddLayer
            // 
            this.bAddLayer.Location = new System.Drawing.Point(112, 3);
            this.bAddLayer.Name = "bAddLayer";
            this.bAddLayer.Size = new System.Drawing.Size(40, 40);
            this.bAddLayer.TabIndex = 0;
            this.bAddLayer.Text = "+";
            this.bAddLayer.UseVisualStyleBackColor = true;
            this.bAddLayer.Click += new System.EventHandler(this.bAddLayer_Click);
            // 
            // bRemoveLayer
            // 
            this.bRemoveLayer.Location = new System.Drawing.Point(227, 3);
            this.bRemoveLayer.Name = "bRemoveLayer";
            this.bRemoveLayer.Size = new System.Drawing.Size(40, 40);
            this.bRemoveLayer.TabIndex = 1;
            this.bRemoveLayer.Text = "-";
            this.bRemoveLayer.UseVisualStyleBackColor = true;
            this.bRemoveLayer.Click += new System.EventHandler(this.bRemoveLayer_Click);
            // 
            // pLayerDisplay
            // 
            this.pLayerDisplay.AutoScroll = true;
            this.pLayerDisplay.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pLayerDisplay.Location = new System.Drawing.Point(0, 49);
            this.pLayerDisplay.Name = "pLayerDisplay";
            this.pLayerDisplay.Size = new System.Drawing.Size(360, 501);
            this.pLayerDisplay.TabIndex = 2;
            // 
            // LayerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Crimson;
            this.Controls.Add(this.pLayerDisplay);
            this.Controls.Add(this.bRemoveLayer);
            this.Controls.Add(this.bAddLayer);
            this.Name = "LayerView";
            this.Size = new System.Drawing.Size(360, 550);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bAddLayer;
        private System.Windows.Forms.Button bRemoveLayer;
        private System.Windows.Forms.Panel pLayerDisplay;
    }
}
