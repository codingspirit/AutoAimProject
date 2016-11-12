using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using Emgu.CV.CvEnum;
using System.IO.Ports;

namespace AutoAimProject
{
    public partial class Main : Form
    {
        static public FormHSV HsvSetting;
        static public bool _advancedHsv = false;
        static public bool _advancedPort = false;
        private int camNum = 1;
        private Capture _capture = null;
        private PortSetting portsetting;
        private bool _captureIng = false;
        static bool _sendIng = false;
        private bool _selectEd = false;
        private bool _fire = false;
        private bool _autoaim = false;
        private bool _selectIng = false;
        private Mat captureget = new Mat();
        private Track objTracking = null;
        private Image<Bgr, Byte> frame;
        private BackgroundWorker backProcess;
        private BackgroundWorker sendProcess;
        private Rectangle selectobject;
        private PointF objPoint = new PointF(0, 0);
        private static SerialPort serialPort1 = new SerialPort();
        static string reciveddata;
        public Main()
        {
            InitializeComponent();
            CvInvoke.UseOpenCL = false;
        }
        private void ProcessFrame(object sender, DoWorkEventArgs e)
        {
            if (_captureIng)
            {
                _capture.Retrieve(captureget, 0);
                if (!_selectIng && !_autoaim)
                {
                    imageBoxCam.Image = captureget;
                }
                if (!_selectIng && _autoaim)
                {
                    using (frame = captureget.ToImage<Bgr, Byte>())
                    {
                        if (objTracking == null)
                        {
                            objTracking = new Track(frame, selectobject);
                        }
                        else
                        {
                            RotatedRect result = objTracking.Tracking(frame);
                            frame.Draw(result, new Bgr(255, 0, 255), 2);
                            frame.Draw(new CircleF(result.Center, 5), new Bgr(0, 0, 255), 2);
                            objPoint = result.Center;
                        }
                        _sendIng = true;
                        imageBoxCam.Image = frame;
                        imageBoxHue.Image = objTracking.hue.Resize(imageBoxHue.Width, imageBoxHue.Height, Inter.Area);
                        imageBoxMask.Image = objTracking.mask.Resize(imageBoxMask.Width, imageBoxMask.Height, Inter.Area);
                        imageBoxBack.Image = objTracking.backprojection.Resize(imageBoxBack.Width, imageBoxBack.Height, Inter.Area);
                    }
                }
                if (_selectIng)
                {
                    using (Image<Bgr, Byte> a = captureget.ToImage<Bgr, Byte>())
                    {
                        a.Draw(selectobject, new Bgr(Color.Red), 1);
                        imageBoxCam.Image = a;
                    }
                }
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (_captureIng)
            {
                _capture.Stop();
                _capture.Dispose();
                buttonOpen.Text = "Start Capture";
                cameraSelect.Enabled = true;
                buttonAutoAim.Enabled = false;
                _autoaim = false;
                buttonAutoAim.Text = "Auto Aim";
            }
            else
            {
                CamInit(camNum);
                buttonOpen.Text = "Stop Capture";
                cameraSelect.Enabled = false;
                buttonAutoAim.Enabled = true;
            }
            _captureIng = !_captureIng;
        }
        private void buttonAutoAim_Click(object sender, EventArgs e)
        {
            if (_capture != null && _captureIng && _selectEd)
            {
                if (_autoaim)
                {
                    buttonAutoAim.Text = "Auto Aim";
                }
                else
                {
                    buttonAutoAim.Text = "Stop Aim";
                }
                _autoaim = !_autoaim;
            }
            else
            {
                MessageBox.Show("Target Unchosen!", "Error!");
            }
        }
        private void CamInit(int camNum)
        {
            progressBar1.Value = 0;
            try
            {
                if (_capture != null)
                {
                    _capture.Dispose();
                    _capture = new Capture(camNum);
                }
                else
                {
                    _capture = new Capture(camNum);
                }
                _capture.Start();
                if (backProcess != null)
                {
                    backProcess.Dispose();
                }
                else
                {
                    backProcess = new BackgroundWorker();
                    backProcess.DoWork += ProcessFrame;
                    backProcess.RunWorkerAsync();
                    backProcess.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                    {

                        backProcess.RunWorkerAsync();
                    };
                }
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
            progressBar1.Value = 100;
        }

        private void cameraSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cameraSelect.SelectedIndex == 0)
            {
                camNum = 0;
            }
            else
            {
                camNum = 1;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            cameraSelect.SelectedIndex = 0;
            frame = new Image<Bgr, byte>(Properties.Resources.AA).Resize(0.85, Inter.Area);
            imageBoxCam.Image = frame;
            imageBoxROI.Image = new Image<Bgr, byte>(Properties.Resources.MP).Resize(0.5, Inter.Area);
            imageBoxHue.Image = new Image<Bgr, byte>(Properties.Resources.CM).Resize(0.5, Inter.Area);
            imageBoxMask.Image = new Image<Bgr, byte>(Properties.Resources.SV).Resize(0.5, Inter.Area);
            imageBoxBack.Image = new Image<Bgr, byte>(Properties.Resources.WR).Resize(0.5, Inter.Area);
            buttonAutoAim.Enabled = false;
            progressBar1.Maximum = 100;
            serialPort1.DataReceived += SerialPort1_DataReceived;
        }

        #region SelectObj
        private void imageBoxCam_MouseDown(object sender, MouseEventArgs e)
        {
            if (_captureIng)
            {
                if (selectobject == null)
                {
                    selectobject = new Rectangle();
                }
                selectobject.X = e.X;
                selectobject.Y = e.Y;
                _selectIng = true;
            }
        }

        private void imageBoxCam_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectIng)
            {
                try
                {
                    selectobject.Height = e.Y - selectobject.Y;
                    selectobject.Width = e.X - selectobject.X;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void imageBoxCam_MouseUp(object sender, MouseEventArgs e)
        {
            if (_selectIng)
            {
                if (selectobject.Width > 10 || selectobject.Height > 10)
                {
                    _selectIng = false;
                    _selectEd = true;
                    Mat b = new Mat(captureget, selectobject);
                    imageBoxROI.Image = b.ToImage<Bgr, Byte>().Resize(imageBoxROI.Width, imageBoxROI.Height, Inter.Area);
                    if (objTracking != null)
                    {
                        objTracking = null;
                    }
                }
                else
                {
                    _selectIng = false;
                }
            }
        }
        #endregion

        #region MenuEvent
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item is AboutBox1)
                {
                    item.Activate();
                    return;
                }
            }
            AboutBox1 aboutform = new AboutBox1();
            aboutform.ShowDialog();
        }

        private void hSVSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item is FormHSV)
                {
                    item.Activate();
                    return;
                }
            }
            HsvSetting = new FormHSV();
            HsvSetting.Show();
            _advancedHsv = true;
        }
        private void comPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            foreach (Form item in Application.OpenForms)
            {
                if (item is PortSetting)
                {
                    item.Activate();
                    return;
                }
            }
            portsetting = new PortSetting();
            portsetting.Show();
            _advancedPort = true;
        }
        private void parameterSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                foreach (Form item in Application.OpenForms)
                {
                    if (item is ParSetting)
                    {
                        item.Activate();
                        return;
                    }
                }
                ParSetting parForm = new ParSetting();
                parForm.Show();
            }
            else
            {
                MessageBox.Show("serialPort Closed!", "Error!");
            }
        }
        private void remoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item is FormRemote)
                {
                    item.Activate();
                    return;
                }
            }
            FormRemote remoteForm = new FormRemote();
            remoteForm.Show();
        }
        #endregion

        #region SerialPort
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] a = new byte[8];
            try
            {
                serialPort1.ReadTo("#");
                serialPort1.Read(a, 0, 8);
                serialPort1.DiscardInBuffer();
                reciveddata = "Out:" + System.Text.Encoding.ASCII.GetString(a, 0, 4) + ',' + System.Text.Encoding.ASCII.GetString(a, 4, 4);
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                try
                {
                    string[] port = SerialPort.GetPortNames();
                    Array.Sort(port);
                    if (!_advancedPort)
                        SetPort(port[0], 115200, 8, 1);
                    serialPort1.Open();
                    if (sendProcess != null)
                    {
                        sendProcess.Dispose();
                    }
                    else
                    {
                        sendProcess = new BackgroundWorker();
                        sendProcess.RunWorkerAsync();
                        sendProcess.DoWork += ProcessSend;
                        sendProcess.RunWorkerCompleted += (object c_sender, RunWorkerCompletedEventArgs c_e) =>
                        {
                            sendProcess.RunWorkerAsync();
                        };
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No Port Found,Please check your ComPort", "Error!");
                }
            }
            else
            {
                serialPort1.Write("@+000+000");
                serialPort1.Close();
            }
            if (serialPort1.IsOpen)
            {
                labelSendState.Text = serialPort1.PortName + "\r" + "Sending";
                labelSendState.Visible = true;
                buttonSend.Text = "Stop Send";
            }
            else
            {
                labelSendState.Visible = false;
                buttonSend.Text = "Send";
            }
        }
        private void buttonFire_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                _fire = true;
            }
            else
            {
                MessageBox.Show("Port Is Closed", "Error!");
            }
        }

        public delegate void SetText(string a, string b, string c);//TextChange delegate
        private void ProcessSend(object sender, DoWorkEventArgs e)
        {
            if (_sendIng)
            {
                String a = "@";
                if (_fire)
                {
                    a = "!";
                    _fire = false;
                }
                int xError = (int)(320 - objPoint.X);
                int yError = (int)(240 - objPoint.Y);
                if (xError == 320 && yError == 240)
                { xError = 0; yError = 0; }
                #region Text delegate
                SetText settext = new SetText(TextChange);
                string center = "Center:" + ((int)objPoint.X).ToString("D3") + "," + ((int)objPoint.Y).ToString("D3");
                string error = "Error:" + xError.ToString("D3") + "," + yError.ToString("D3");
                IAsyncResult aResult = this.BeginInvoke(settext, center, error, reciveddata);
                aResult.AsyncWaitHandle.WaitOne(1);
                this.EndInvoke(aResult);
                #endregion
                if (xError >= 0)
                {
                    a += "+";
                }
                else
                {
                    a += "-";
                }
                a += Math.Abs(xError).ToString("D3");
                if (yError >= 0)
                {
                    a += "+";
                }
                else
                {
                    a += "-";
                }
                a += Math.Abs(yError).ToString("D3");
                serialPort1.Write(a);
                _sendIng = false;
            }
        }

        private void TextChange(string a, string b, string c)//Text delegate
        {
            labelCenter.Text = a;
            labelError.Text = b;
            labelOutpwm.Text = c;
        }

        public static void SetPort(string portname, int baudrate, int databits, int stopbits)
        {
            try
            {
                serialPort1.PortName = portname;
                serialPort1.BaudRate = baudrate;
                serialPort1.DataBits = databits;
                if (stopbits == 1)
                {
                    serialPort1.StopBits = StopBits.One;
                }
                if (stopbits == 2)
                {
                    serialPort1.StopBits = StopBits.Two;
                }
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Open Port.Please Check your Setting", "Error!");
            }
        }//SetPort delegate
        public static void SetParameter(int p, int i, int d)
        {
            string a = "P" + p.ToString("D2") + "I" + i.ToString("D2") + "D" + d.ToString("D2");
            serialPort1.Write(a);
        }
        public static void RemoteControl(string command)
        {
            //if(_sendIng)
            {
                switch (command)
                {
                    case "/LEFT/": serialPort1.Write("[LLLLLLL]"); break;
                    case "/RIGHT/": serialPort1.Write("[RRRRRRR]"); break;
                    case "/UP/": serialPort1.Write("[UUUUUUU]"); break;
                    case "/DOWN/": serialPort1.Write("[DDDDDDD]"); break;
                    case "/FIRE/": serialPort1.Write("[FFFFFFF]"); break;
                }
            }
        }
    }
}
