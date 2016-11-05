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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarVmax = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarSmin = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmin)).BeginInit();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vmin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vmax";
            // 
            // trackBarVmax
            // 
            this.trackBarVmax.Location = new System.Drawing.Point(49, 55);
            this.trackBarVmax.Name = "trackBarVmax";
            this.trackBarVmax.Size = new System.Drawing.Size(286, 45);
            this.trackBarVmax.TabIndex = 2;
            this.trackBarVmax.Scroll += new System.EventHandler(this.trackBarVmax_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Smin";
            // 
            // trackBarSmin
            // 
            this.trackBarSmin.Location = new System.Drawing.Point(49, 106);
            this.trackBarSmin.Name = "trackBarSmin";
            this.trackBarSmin.Size = new System.Drawing.Size(286, 45);
            this.trackBarSmin.TabIndex = 4;
            this.trackBarSmin.Scroll += new System.EventHandler(this.trackBarSmin_Scroll);
            // 
            // FormHSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 150);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBarSmin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarVmax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarVmin);
            this.MinimizeBox = false;
            this.Name = "FormHSV";
            this.Text = "FormHSV";
            this.Load += new System.EventHandler(this.FormHSV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarVmin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarVmax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarSmin;
    }
}