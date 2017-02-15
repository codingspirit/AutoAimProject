namespace AutoAimProject
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.buttonOpen = new System.Windows.Forms.Button();
            this.cameraSelect = new System.Windows.Forms.ComboBox();
            this.controlArea = new System.Windows.Forms.GroupBox();
            this.buttonFire = new System.Windows.Forms.Button();
            this.labelSendState = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonAutoAim = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxCam = new System.Windows.Forms.GroupBox();
            this.imageBoxCam = new Emgu.CV.UI.ImageBox();
            this.groupBoxProcess = new System.Windows.Forms.GroupBox();
            this.labelOutpwm = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.labelCenter = new System.Windows.Forms.Label();
            this.imageBoxBack = new Emgu.CV.UI.ImageBox();
            this.imageBoxMask = new Emgu.CV.UI.ImageBox();
            this.imageBoxHue = new Emgu.CV.UI.ImageBox();
            this.imageBoxROI = new Emgu.CV.UI.ImageBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.advancedSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSVSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parameterSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCrossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlArea.SuspendLayout();
            this.groupBoxCam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCam)).BeginInit();
            this.groupBoxProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxROI)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOpen.Location = new System.Drawing.Point(6, 110);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(155, 58);
            this.buttonOpen.TabIndex = 3;
            this.buttonOpen.Text = "Start Capture";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // cameraSelect
            // 
            this.cameraSelect.FormattingEnabled = true;
            this.cameraSelect.Items.AddRange(new object[] {
            "Camera 0",
            "Camera 1"});
            this.cameraSelect.Location = new System.Drawing.Point(77, 14);
            this.cameraSelect.Name = "cameraSelect";
            this.cameraSelect.Size = new System.Drawing.Size(75, 20);
            this.cameraSelect.TabIndex = 4;
            this.cameraSelect.SelectedIndexChanged += new System.EventHandler(this.cameraSelect_SelectedIndexChanged);
            // 
            // controlArea
            // 
            this.controlArea.Controls.Add(this.buttonFire);
            this.controlArea.Controls.Add(this.labelSendState);
            this.controlArea.Controls.Add(this.progressBar1);
            this.controlArea.Controls.Add(this.buttonAutoAim);
            this.controlArea.Controls.Add(this.buttonSend);
            this.controlArea.Controls.Add(this.label1);
            this.controlArea.Controls.Add(this.buttonOpen);
            this.controlArea.Controls.Add(this.cameraSelect);
            this.controlArea.Location = new System.Drawing.Point(672, 351);
            this.controlArea.Name = "controlArea";
            this.controlArea.Size = new System.Drawing.Size(330, 178);
            this.controlArea.TabIndex = 5;
            this.controlArea.TabStop = false;
            this.controlArea.Text = "Control Area";
            // 
            // buttonFire
            // 
            this.buttonFire.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonFire.Image = global::AutoAimProject.Properties.Resources.FIRE;
            this.buttonFire.Location = new System.Drawing.Point(167, 44);
            this.buttonFire.Name = "buttonFire";
            this.buttonFire.Size = new System.Drawing.Size(155, 60);
            this.buttonFire.TabIndex = 10;
            this.buttonFire.UseVisualStyleBackColor = true;
            this.buttonFire.Click += new System.EventHandler(this.buttonFire_Click);
            // 
            // labelSendState
            // 
            this.labelSendState.AutoSize = true;
            this.labelSendState.ForeColor = System.Drawing.Color.Lime;
            this.labelSendState.Location = new System.Drawing.Point(165, 13);
            this.labelSendState.Name = "labelSendState";
            this.labelSendState.Size = new System.Drawing.Size(47, 12);
            this.labelSendState.TabIndex = 9;
            this.labelSendState.Text = "Sending";
            this.labelSendState.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(222, 13);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // buttonAutoAim
            // 
            this.buttonAutoAim.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAutoAim.Location = new System.Drawing.Point(167, 110);
            this.buttonAutoAim.Name = "buttonAutoAim";
            this.buttonAutoAim.Size = new System.Drawing.Size(155, 58);
            this.buttonAutoAim.TabIndex = 7;
            this.buttonAutoAim.Text = "Auto Aim";
            this.buttonAutoAim.UseVisualStyleBackColor = true;
            this.buttonAutoAim.Click += new System.EventHandler(this.buttonAutoAim_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSend.Location = new System.Drawing.Point(6, 44);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(155, 60);
            this.buttonSend.TabIndex = 6;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Cam Select";
            // 
            // groupBoxCam
            // 
            this.groupBoxCam.Controls.Add(this.imageBoxCam);
            this.groupBoxCam.Location = new System.Drawing.Point(12, 26);
            this.groupBoxCam.Name = "groupBoxCam";
            this.groupBoxCam.Size = new System.Drawing.Size(654, 503);
            this.groupBoxCam.TabIndex = 6;
            this.groupBoxCam.TabStop = false;
            this.groupBoxCam.Text = "Cam Area";
            // 
            // imageBoxCam
            // 
            this.imageBoxCam.Cursor = System.Windows.Forms.Cursors.Cross;
            this.imageBoxCam.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.RightClickMenu;
            this.imageBoxCam.Location = new System.Drawing.Point(9, 17);
            this.imageBoxCam.Name = "imageBoxCam";
            this.imageBoxCam.Size = new System.Drawing.Size(640, 480);
            this.imageBoxCam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBoxCam.TabIndex = 2;
            this.imageBoxCam.TabStop = false;
            this.imageBoxCam.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBoxCam_MouseDown);
            this.imageBoxCam.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBoxCam_MouseMove);
            this.imageBoxCam.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBoxCam_MouseUp);
            // 
            // groupBoxProcess
            // 
            this.groupBoxProcess.Controls.Add(this.labelOutpwm);
            this.groupBoxProcess.Controls.Add(this.labelError);
            this.groupBoxProcess.Controls.Add(this.labelCenter);
            this.groupBoxProcess.Controls.Add(this.imageBoxBack);
            this.groupBoxProcess.Controls.Add(this.imageBoxMask);
            this.groupBoxProcess.Controls.Add(this.imageBoxHue);
            this.groupBoxProcess.Controls.Add(this.imageBoxROI);
            this.groupBoxProcess.Location = new System.Drawing.Point(672, 26);
            this.groupBoxProcess.Name = "groupBoxProcess";
            this.groupBoxProcess.Size = new System.Drawing.Size(330, 319);
            this.groupBoxProcess.TabIndex = 7;
            this.groupBoxProcess.TabStop = false;
            this.groupBoxProcess.Text = "Process Frame";
            // 
            // labelOutpwm
            // 
            this.labelOutpwm.AutoSize = true;
            this.labelOutpwm.Location = new System.Drawing.Point(220, 304);
            this.labelOutpwm.Name = "labelOutpwm";
            this.labelOutpwm.Size = new System.Drawing.Size(0, 12);
            this.labelOutpwm.TabIndex = 8;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(112, 304);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(0, 12);
            this.labelError.TabIndex = 7;
            // 
            // labelCenter
            // 
            this.labelCenter.AutoSize = true;
            this.labelCenter.Location = new System.Drawing.Point(4, 304);
            this.labelCenter.Name = "labelCenter";
            this.labelCenter.Size = new System.Drawing.Size(0, 12);
            this.labelCenter.TabIndex = 6;
            // 
            // imageBoxBack
            // 
            this.imageBoxBack.Location = new System.Drawing.Point(167, 156);
            this.imageBoxBack.Name = "imageBoxBack";
            this.imageBoxBack.Size = new System.Drawing.Size(155, 130);
            this.imageBoxBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBoxBack.TabIndex = 5;
            this.imageBoxBack.TabStop = false;
            // 
            // imageBoxMask
            // 
            this.imageBoxMask.Location = new System.Drawing.Point(6, 156);
            this.imageBoxMask.Name = "imageBoxMask";
            this.imageBoxMask.Size = new System.Drawing.Size(155, 130);
            this.imageBoxMask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBoxMask.TabIndex = 4;
            this.imageBoxMask.TabStop = false;
            // 
            // imageBoxHue
            // 
            this.imageBoxHue.Location = new System.Drawing.Point(167, 20);
            this.imageBoxHue.Name = "imageBoxHue";
            this.imageBoxHue.Size = new System.Drawing.Size(155, 130);
            this.imageBoxHue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBoxHue.TabIndex = 3;
            this.imageBoxHue.TabStop = false;
            // 
            // imageBoxROI
            // 
            this.imageBoxROI.Location = new System.Drawing.Point(6, 20);
            this.imageBoxROI.Name = "imageBoxROI";
            this.imageBoxROI.Size = new System.Drawing.Size(155, 130);
            this.imageBoxROI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBoxROI.TabIndex = 2;
            this.imageBoxROI.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedSettingToolStripMenuItem,
            this.parameterSettingToolStripMenuItem,
            this.remoteToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.showCrossToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // advancedSettingToolStripMenuItem
            // 
            this.advancedSettingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comPortToolStripMenuItem,
            this.hSVSettingToolStripMenuItem});
            this.advancedSettingToolStripMenuItem.Name = "advancedSettingToolStripMenuItem";
            this.advancedSettingToolStripMenuItem.Size = new System.Drawing.Size(121, 21);
            this.advancedSettingToolStripMenuItem.Text = "Advanced Setting";
            // 
            // comPortToolStripMenuItem
            // 
            this.comPortToolStripMenuItem.Name = "comPortToolStripMenuItem";
            this.comPortToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.comPortToolStripMenuItem.Text = "Com Port Setting";
            this.comPortToolStripMenuItem.Click += new System.EventHandler(this.comPortToolStripMenuItem_Click);
            // 
            // hSVSettingToolStripMenuItem
            // 
            this.hSVSettingToolStripMenuItem.Name = "hSVSettingToolStripMenuItem";
            this.hSVSettingToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.hSVSettingToolStripMenuItem.Text = "HSV Setting";
            this.hSVSettingToolStripMenuItem.Click += new System.EventHandler(this.hSVSettingToolStripMenuItem_Click);
            // 
            // parameterSettingToolStripMenuItem
            // 
            this.parameterSettingToolStripMenuItem.Name = "parameterSettingToolStripMenuItem";
            this.parameterSettingToolStripMenuItem.Size = new System.Drawing.Size(124, 21);
            this.parameterSettingToolStripMenuItem.Text = "Parameter Setting";
            this.parameterSettingToolStripMenuItem.Click += new System.EventHandler(this.parameterSettingToolStripMenuItem_Click);
            // 
            // remoteToolStripMenuItem
            // 
            this.remoteToolStripMenuItem.Name = "remoteToolStripMenuItem";
            this.remoteToolStripMenuItem.Size = new System.Drawing.Size(65, 21);
            this.remoteToolStripMenuItem.Text = "Remote";
            this.remoteToolStripMenuItem.Click += new System.EventHandler(this.remoteToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // showCrossToolStripMenuItem
            // 
            this.showCrossToolStripMenuItem.Name = "showCrossToolStripMenuItem";
            this.showCrossToolStripMenuItem.Size = new System.Drawing.Size(88, 21);
            this.showCrossToolStripMenuItem.Text = "Show Cross";
            this.showCrossToolStripMenuItem.Click += new System.EventHandler(this.showCrossToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1018, 533);
            this.Controls.Add(this.groupBoxProcess);
            this.Controls.Add(this.groupBoxCam);
            this.Controls.Add(this.controlArea);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "AutoAim Ver Beta By Coding Spirit";
            this.Load += new System.EventHandler(this.Main_Load);
            this.controlArea.ResumeLayout(false);
            this.controlArea.PerformLayout();
            this.groupBoxCam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCam)).EndInit();
            this.groupBoxProcess.ResumeLayout(false);
            this.groupBoxProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxROI)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.ComboBox cameraSelect;
        private System.Windows.Forms.GroupBox controlArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAutoAim;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.GroupBox groupBoxCam;
        private System.Windows.Forms.GroupBox groupBoxProcess;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Emgu.CV.UI.ImageBox imageBoxROI;
        private Emgu.CV.UI.ImageBox imageBoxHue;
        private System.Windows.Forms.Button buttonFire;
        private System.Windows.Forms.Label labelSendState;
        private Emgu.CV.UI.ImageBox imageBoxMask;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem advancedSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hSVSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private Emgu.CV.UI.ImageBox imageBoxBack;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label labelCenter;
        private System.Windows.Forms.Label labelOutpwm;
        private System.Windows.Forms.ToolStripMenuItem parameterSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteToolStripMenuItem;
        private Emgu.CV.UI.ImageBox imageBoxCam;
        private System.Windows.Forms.ToolStripMenuItem showCrossToolStripMenuItem;
    }
}

