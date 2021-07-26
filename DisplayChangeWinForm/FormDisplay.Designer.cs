namespace DisplayChangeWinForm
{
    partial class FormDisplay
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbDisplays = new System.Windows.Forms.ComboBox();
            this.lvDisplayModes = new System.Windows.Forms.ListView();
            this.DeviceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DisplayWidth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DisplayHeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Frequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BitsPerPel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // cbDisplays
            // 
            this.cbDisplays.FormattingEnabled = true;
            this.cbDisplays.Location = new System.Drawing.Point(12, 12);
            this.cbDisplays.Name = "cbDisplays";
            this.cbDisplays.Size = new System.Drawing.Size(600, 21);
            this.cbDisplays.TabIndex = 0;
            this.cbDisplays.SelectedIndexChanged += new System.EventHandler(this.CbDisplays_SelectedIndexChanged);
            // 
            // lvDisplayModes
            // 
            this.lvDisplayModes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DeviceName,
            this.DisplayWidth,
            this.DisplayHeight,
            this.Frequency,
            this.BitsPerPel});
            this.lvDisplayModes.FullRowSelect = true;
            this.lvDisplayModes.GridLines = true;
            this.lvDisplayModes.Location = new System.Drawing.Point(12, 39);
            this.lvDisplayModes.Name = "lvDisplayModes";
            this.lvDisplayModes.Size = new System.Drawing.Size(600, 390);
            this.lvDisplayModes.TabIndex = 1;
            this.lvDisplayModes.UseCompatibleStateImageBehavior = false;
            this.lvDisplayModes.View = System.Windows.Forms.View.Details;
            this.lvDisplayModes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvDisplayModes_MouseDoubleClick);
            // 
            // DeviceName
            // 
            this.DeviceName.Text = "Name";
            this.DeviceName.Width = 115;
            // 
            // DisplayWidth
            // 
            this.DisplayWidth.Text = "Width";
            this.DisplayWidth.Width = 115;
            // 
            // DisplayHeight
            // 
            this.DisplayHeight.Text = "Height";
            this.DisplayHeight.Width = 115;
            // 
            // Frequency
            // 
            this.Frequency.Text = "Frequency";
            this.Frequency.Width = 115;
            // 
            // BitsPerPel
            // 
            this.BitsPerPel.Text = "Bits";
            this.BitsPerPel.Width = 115;
            // 
            // FormDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.lvDisplayModes);
            this.Controls.Add(this.cbDisplays);
            this.Name = "FormDisplay";
            this.Text = "Display Form";
            this.Load += new System.EventHandler(this.FormDisplay_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDisplays;
        private System.Windows.Forms.ListView lvDisplayModes;
        private System.Windows.Forms.ColumnHeader DeviceName;
        private System.Windows.Forms.ColumnHeader DisplayWidth;
        private System.Windows.Forms.ColumnHeader DisplayHeight;
        private System.Windows.Forms.ColumnHeader Frequency;
        private System.Windows.Forms.ColumnHeader BitsPerPel;
    }
}

