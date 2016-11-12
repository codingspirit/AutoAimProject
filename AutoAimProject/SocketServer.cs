using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace AutoAimProject
{
    class SocketServer
    {
        IPAddress serverIP;
        int serverPort;
        IPEndPoint serverIPE;
        TcpListener listener;
        bool isRunning = false;
        public delegate void ServerEventHandler(object sender, EventArgs e);
        public event ServerEventHandler Client_ConnectEvent;//client connect evet to update client combox
        public event ServerEventHandler ReceiveEvent;//received event to update message
        public event ServerEventHandler SendEvent;
        private List<Client> clientList = new List<Client>();
        List<string> clientName = new List<string>();

        public IPAddress ServerIP
        {
            get
            {
                return serverIP;
            }
        }

        public int ServerPort
        {
            get
            {
                return serverPort;
            }

        }

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }

        public List<string> ClientName
        {
            get
            {
                return clientName;
            }
        }

        public SocketServer(IPAddress ip, int port)
        {
            try
            {
                serverIPE = new IPEndPoint(ip, port);
                serverIP = ip;
                serverPort = port;
                clientList = new List<Client>();
                clientName = new List<string>();
            }
            catch (Exception e)
            {
                MessageBox.Show("Can't Create Server:" + e.Message, "Error!");
                throw;
            }
        }
        public bool RunServer()
        {
            if (!isRunning)
            {
                try
                {
                    listener = new TcpListener(serverIP, serverPort);
                    listener.Start(5);//MAX connection
                    isRunning = true;
                    AsyncCallback callback = new AsyncCallback(ConnectCallBack);
                    listener.BeginAcceptSocket(callback, listener);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Can't Start Server:" + e.Message, "Error!");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        public void StopServer()
        {
            if (isRunning)
            {
                listener.Stop();
                //for (int i= clientList.Count-1; i>=0;i--)
                //{
                //    clientList[i].Dispose();
                //    clientList.RemoveAt(i);
                //}
                isRunning = false;
            }
        }
        public void Send(string clientname, byte[] data)
        {
            string msg = "Send Faild";
            try
            {
                AsyncCallback callback = new AsyncCallback(SendCallBack);
                for (int i = 0; i < clientList.Count; i++)
                {
                    if (clientList[i].Name == clientname)
                    {
                        clientList[i].socket.BeginSend(data, 0, data.Length, SocketFlags.None, callback, clientList[i]);
                        msg = String.Format("To[{0}]:{1}", clientList[i].socket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(data));
                        SendEvent(msg, new EventArgs());
                    }

                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Send Faild:" + e.Message, "Error!");
                SendEvent(msg, new EventArgs());
            }
        }
        public static List<string> GetIP()
        {
            List<string> listIP = new List<string>();
            try
            {
                string HostName = Dns.GetHostName();
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)//IPV4
                    {
                        listIP.Add(IpEntry.AddressList[i].ToString());
                    }
                }
                return listIP;
            }
            catch (Exception e)
            {
                MessageBox.Show("Can't Get Your IP:" + e.Message);
                listIP.Clear();
                return listIP;
            }
        }
        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                Socket socket = listener.EndAcceptSocket(ar);
                Client client = new Client(socket);
                if (!clientList.Contains(client))
                {
                    clientList.Add(client);
                    clientName.Add(clientList.Last<Client>().Name);
                    //Sign Event
                    Client_ConnectEvent(clientName, new EventArgs());
                }
                AsyncCallback callback;
                if (isRunning)
                {
                    callback = new AsyncCallback(ConnectCallBack);
                    listener.BeginAcceptSocket(callback, listener);
                }
                client.ClearBuffer();
                callback = new AsyncCallback(ReceiveCallBack);
                client.socket.BeginReceive(client.buffer, 0, client.buffer.Length, SocketFlags.None, callback, client);

            }
            catch (Exception e)
            {
                isRunning = false;
                //MessageBox.Show("Unknown Error:" + e.Message, "Error!");
            }
        }
        private void ReceiveCallBack(IAsyncResult ar)
        {
            Client client = (Client)ar.AsyncState;
            try
            {
                int i = client.socket.EndReceive(ar);
                if (i == 0)//Disconnect
                {
                    clientList.Remove(client);
                    clientName.Remove(client.Name);
                    Client_ConnectEvent(clientName, new EventArgs());
                    return;
                }
                else
                {
                    string data = Encoding.UTF8.GetString(client.buffer, 0, i);
                    data = String.Format("From[{0}]:{1}", client.socket.RemoteEndPoint.ToString(), data);
                    ReceiveEvent(data, new EventArgs());
                    client.ClearBuffer();
                    AsyncCallback callback = new AsyncCallback(ReceiveCallBack);
                    client.socket.BeginReceive(client.buffer, 0, client.buffer.Length, SocketFlags.None, callback, client);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unknown Error:" + e.Message, "Error!");
            }
        }
        private void SendCallBack(IAsyncResult ar)
        {
            Client client = (Client)ar.AsyncState;
            try
            {
                client.socket.EndSend(ar);
            }
            catch (Exception e)
            {
                MessageBox.Show("Send Error:" + e.Message, "Error!");
            }
        }
        class Client
        {
            public Socket socket = null;
            string name;
            public byte[] buffer;

            public string Name
            {
                get
                {
                    return name;
                }
            }
            public Client(Socket s)
            {
                socket = s;
                buffer = new byte[512];
                name = s.RemoteEndPoint.ToString();
            }
            public void ClearBuffer()
            {
                buffer = new byte[512];
            }
            public void Dispose()
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Can't Dispose Client:" + e.Message, "Error!");
                }
                finally
                {
                    socket = null;
                    buffer = null;
                }
            }
        }
    }
}
