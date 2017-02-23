using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace AutoAimProject
{
    public partial class FormRemote : Form
    {
        SocketServer server = null;
        private delegate void ServerHandler(object sender, EventArgs e);
        private delegate void RemoteCommand(string command);
        public FormRemote()
        {
            InitializeComponent();
        }

        private void FormRemote_Load(object sender, EventArgs e)
        {
            List<string> ListIP = SocketServer.GetIP();
            foreach (string item in ListIP)
            {
                comboBoxIP.Items.Add(item);
            }
            textBoxPort.Text = "2222";
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                server = new SocketServer(IPAddress.Parse(comboBoxIP.Text), int.Parse(textBoxPort.Text));
                server.Client_ConnectEvent += new SocketServer.ServerEventHandler(AddClient);
                server.ReceiveEvent += new SocketServer.ServerEventHandler(SocketReceived);
                server.SendEvent += new SocketServer.ServerEventHandler(SocketSended);
                Main.FrameProcessed += new Main.FrameEventHandler(FrameReceived);
            }
            if (server != null && server.IsRunning == false)
            {
                if (server.RunServer())
                {
                    buttonRun.Enabled = false;
                }
            }
        }
        private void AddClient(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                ServerHandler mydelgate = new ServerHandler(AddClient);
                this.Invoke(mydelgate, new object[] { sender, e });
                return;
            }
            comboBoxClient.Items.Clear();
            foreach (string item in (List<string>)sender)
            {
                comboBoxClient.Items.Add(item);
            }
            if (comboBoxClient.Items.Count == 0)
                comboBoxClient.SelectedIndex = -1;
            else
                comboBoxClient.SelectedIndex = 0;
        }
        private void SocketReceived(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                ServerHandler mydelgate = new ServerHandler(SocketReceived);
                this.Invoke(mydelgate, new object[] { sender, e });
                return;
            }
            if (((string)sender).Contains("/"))
            {
                RemoteCommand remotecommand = new RemoteCommand(Main.RemoteControl);
                int s1 = ((string)sender).IndexOf("/");
                int s2 = ((string)sender).LastIndexOf("/");
                string cmd = ((string)sender).Substring(s1, s2 - s1 + 1);
                remotecommand(cmd);
            }
            listBoxState.Items.Add((string)sender);
            listBoxState.SelectedIndex = listBoxState.Items.Count - 1;
        }
        private void FrameReceived(object sender, EventArgs e)
        {
            if (server.IsRunning)
            {
                if (InvokeRequired)
                {
                    ServerHandler mydelgate = new ServerHandler(FrameReceived);
                    this.Invoke(mydelgate, new object[] { sender, e });
                    return;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    //(sender as Bitmap).Save(stream, ImageFormat.Bmp);
                    (sender as Bitmap).Save(stream, ImageFormat.Jpeg);
                    byte[] imagebyte = new byte[stream.Length];
                    imagebyte = stream.ToArray();
                    server.Send(comboBoxClient.Text, Encoding.UTF8.GetBytes("#!#" + imagebyte.Length.ToString()));//packge head + file length
                    server.Send(comboBoxClient.Text, imagebyte);
                }
            }
        }
        private void SocketSended(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                ServerHandler mydelgate = new ServerHandler(SocketSended);
                this.Invoke(mydelgate, new object[] { sender, e });
                return;
            }
            if(!((string)sender).Contains("filelength")&& !((string)sender).Contains("#!#"))
            listBoxState.Items.Add((string)sender);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (server.IsRunning)
            {
                server.StopServer();
                buttonRun.Enabled = true;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            server.Send(comboBoxClient.Text, Encoding.UTF8.GetBytes("#!#" + textBoxSendText.Text.Length.ToString("D5")));
            server.Send(comboBoxClient.Text, Encoding.UTF8.GetBytes(textBoxSendText.Text));
        }
        public static byte[] ZipByte(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }
            MemoryStream ms = new MemoryStream();
            using (DeflaterOutputStream zStream = new DeflaterOutputStream(ms))
            {
                zStream.Write(bytes, 0, bytes.Length);
                zStream.Finish();
                zStream.Close();
            }

            byte[] result = ms.ToArray();
            ms.Close();
            return result;
        }
    }
}
