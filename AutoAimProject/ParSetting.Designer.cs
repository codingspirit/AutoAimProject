namespace AutoAimProject
{
    partial class ParSetting
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
            this.buttonSet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarD = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarI = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarP = new System.Windows.Forms.TrackBar();
            this.labelP = new System.Windows.Forms.Label();
            this.labelI = new System.Windows.Forms.Label();
            this.labelD = new System.Windows.Forms.Label();
            this.checkBoxSet = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarP)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(152, 164);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(75, 23);
            this.buttonSet.TabIndex = 0;
            this.buttonSet.Text = "Set";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "D";
            // 
            // trackBarD
            // 
            this.trackBarD.Location = new System.Drawing.Point(45, 109);
            this.trackBarD.Name = "trackBarD";
            this.trackBarD.Size = new System.Drawing.Size(286, 45);
            this.trackBarD.TabIndex = 10;
            this.trackBarD.Scroll += new System.EventHandler(this.trackBarScroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "I";
            // 
            // trackBarI
            // 
            this.trackBarI.Location = new System.Drawing.Point(45, 58);
            this.trackBarI.Name = "trackBarI";
            this.trackBarI.Size = new System.Drawing.Size(286, 45);
            this.trackBarI.TabIndex = 8;
            this.trackBarI.Scroll += new System.EventHandler(this.trackBarScroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "P";
            // 
            // trackBarP
            // 
            this.trackBarP.Location = new System.Drawing.Point(45, 12);
            this.trackBarP.Name = "trackBarP";
            this.trackBarP.Size = new System.Drawing.Size(286, 45);
            this.trackBarP.TabIndex = 6;
            this.trackBarP.Scroll += new System.EventHandler(this.trackBarScroll);
            // 
            // labelP
            // 
            this.labelP.AutoSize = true;
            this.labelP.Location = new System.Drawing.Point(331, 17);
            this.labelP.Name = "labelP";
            this.labelP.Size = new System.Drawing.Size(41, 12);
            this.labelP.TabIndex = 12;
            this.labelP.Text = "label4";
            // 
            // labelI
            // 
            this.labelI.AutoSize = true;
            this.labelI.Location = new System.Drawing.Point(331, 65);
            this.labelI.Name = "labelI";
            this.labelI.Size = new System.Drawing.Size(41, 12);
            this.labelI.TabIndex = 13;
            this.labelI.Text = "label5";
            // 
            // labelD
            // 
            this.labelD.AutoSize = true;
            this.labelD.Location = new System.Drawing.Point(331, 113);
            this.labelD.Name = "labelD";
            this.labelD.Size = new System.Drawing.Size(41, 12);
            this.labelD.TabIndex = 14;
            this.labelD.Text = "label6";
            // 
            // checkBoxSet
            // 
            this.checkBoxSet.AutoSize = true;
            this.checkBoxSet.Location = new System.Drawing.Point(233, 171);
            this.checkBoxSet.Name = "checkBoxSet";
            this.checkBoxSet.Size = new System.Drawing.Size(72, 16);
            this.checkBoxSet.TabIndex = 15;
            this.checkBoxSet.Text = "Keep Set";
            this.checkBoxSet.UseVisualStyleBackColor = true;
            this.checkBoxSet.CheckedChanged += new System.EventHandler(this.checkBoxSet_CheckedChanged);
            // 
            // ParSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 199);
            this.Controls.Add(this.checkBoxSet);
            this.Controls.Add(this.labelD);
            this.Controls.Add(this.labelI);
            this.Controls.Add(this.labelP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBarD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarP);
            this.Controls.Add(this.buttonSet);
            this.Name = "ParSetting";
            this.Text = "ParSetting";
            this.Load += new System.EventHandler(this.ParSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarP;
        private System.Windows.Forms.Label labelP;
        private System.Windows.Forms.Label labelI;
        private System.Windows.Forms.Label labelD;
        private System.Windows.Forms.CheckBox checkBoxSet;
    }
}