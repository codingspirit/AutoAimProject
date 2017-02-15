namespace AutoAimProject
{
    partial class FormHSV
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
            this.trackBarVmin = new System.Windows.Forms.TrackBar();
            this.labelVmin = new System.Windows.Forms.Label();
            this.labelVmax = new System.Windows.Forms.Label();
            this.trackBarVmax = new System.Windows.Forms.TrackBar();
            this.labelSmin = new System.Windows.Forms.Label();
            this.trackBarSmin = new System.Windows.Forms.TrackBar();
            this.trackBarBins = new System.Windows.Forms.TrackBar();
            this.labelBins = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBins)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarVmin
            // 
            this.trackBarVmin.Location = new System.Drawing.Point(49, 9);
            this.trackBarVmin.Name = "trackBarVmin";
            this.trackBarVmin.Size = new System.Drawing.Size(286, 45);
            this.trackBarVmin.TabIndex = 0;
            this.trackBarVmin.Scroll += new System.EventHandler(this.trackBarVmin_Scroll);
            // 
            // labelVmin
            // 
            this.labelVmin.AutoSize = true;
            this.labelVmin.Location = new System.Drawing.Point(14, 14);
            this.labelVmin.Name = "labelVmin";
            this.labelVmin.Size = new System.Drawing.Size(29, 12);
            this.labelVmin.TabIndex = 1;
            this.labelVmin.Text = "Vmin";
            // 
            // labelVmax
            // 
            this.labelVmax.AutoSize = true;
            this.labelVmax.Location = new System.Drawing.Point(14, 60);
            this.labelVmax.Name = "labelVmax";
            this.labelVmax.Size = new System.Drawing.Size(29, 12);
            this.labelVmax.TabIndex = 3;
            this.labelVmax.Text = "Vmax";
            // 
            // trackBarVmax
            // 
            this.trackBarVmax.Location = new System.Drawing.Point(49, 55);
            this.trackBarVmax.Name = "trackBarVmax";
            this.trackBarVmax.Size = new System.Drawing.Size(286, 45);
            this.trackBarVmax.TabIndex = 2;
            this.trackBarVmax.Scroll += new System.EventHandler(this.trackBarVmax_Scroll);
            // 
            // labelSmin
            // 
            this.labelSmin.AutoSize = true;
            this.labelSmin.Location = new System.Drawing.Point(14, 111);
            this.labelSmin.Name = "labelSmin";
            this.labelSmin.Size = new System.Drawing.Size(29, 12);
            this.labelSmin.TabIndex = 5;
            this.labelSmin.Text = "Smin";
            // 
            // trackBarSmin
            // 
            this.trackBarSmin.Location = new System.Drawing.Point(49, 106);
            this.trackBarSmin.Name = "trackBarSmin";
            this.trackBarSmin.Size = new System.Drawing.Size(286, 45);
            this.trackBarSmin.TabIndex = 4;
            this.trackBarSmin.Scroll += new System.EventHandler(this.trackBarSmin_Scroll);
            // 
            // trackBarBins
            // 
            this.trackBarBins.Location = new System.Drawing.Point(49, 157);
            this.trackBarBins.Name = "trackBarBins";
            this.trackBarBins.Size = new System.Drawing.Size(286, 45);
            this.trackBarBins.TabIndex = 6;
            this.trackBarBins.Scroll += new System.EventHandler(this.trackBarBins_Scroll);
            // 
            // labelBins
            // 
            this.labelBins.AutoSize = true;
            this.labelBins.Location = new System.Drawing.Point(14, 166);
            this.labelBins.Name = "labelBins";
            this.labelBins.Size = new System.Drawing.Size(29, 12);
            this.labelBins.TabIndex = 7;
            this.labelBins.Text = "Bins";
            // 
            // FormHSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 198);
            this.Controls.Add(this.labelBins);
            this.Controls.Add(this.trackBarBins);
            this.Controls.Add(this.labelSmin);
            this.Controls.Add(this.trackBarSmin);
            this.Controls.Add(this.labelVmax);
            this.Controls.Add(this.trackBarVmax);
            this.Controls.Add(this.labelVmin);
            this.Controls.Add(this.trackBarVmin);
            this.MinimizeBox = false;
            this.Name = "FormHSV";
            this.Text = "FormHSV";
            this.Load += new System.EventHandler(this.FormHSV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarVmin;
        private System.Windows.Forms.Label labelVmin;
        private System.Windows.Forms.Label labelVmax;
        private System.Windows.Forms.TrackBar trackBarVmax;
        private System.Windows.Forms.Label labelSmin;
        private System.Windows.Forms.TrackBar trackBarSmin;
        private System.Windows.Forms.TrackBar trackBarBins;
        private System.Windows.Forms.Label labelBins;
    }
}