using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                remotecommand((string)sender);
            }
            listBoxState.Items.Add((string)sender);
            listBoxState.SelectedIndex = listBoxState.Items.Count - 1;
        }
        private void SocketSended(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                ServerHandler mydelgate = new ServerHandler(SocketSended);
                this.Invoke(mydelgate, new object[] { sender, e });
                return;
            }
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
            server.Send(comboBoxClient.Text, Encoding.UTF8.GetBytes(textBoxSendText.Text));
        }
    }
}
