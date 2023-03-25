using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Server : Form
    {
        private UdpClient server;
        private Thread listeningThread;
        private List<string> messages = new List<string>();
        private IPEndPoint clientEndPoint;
        public Server()
        {
            InitializeComponent();
            server = new UdpClient(1000);
            listeningThread = new Thread(new ThreadStart(ListenForMessages));
            listeningThread.Start();

            listViewChat.View = View.Details;
            listViewChat.Columns.Add("Chat with Client", -2, HorizontalAlignment.Left);
        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            SentData();
            textBoxSend.Clear();
        }

        private void SentData()
        {
            string data = textBoxSend.Text;

            if (string.IsNullOrEmpty(data))
            {
                MessageBox.Show("Please fill in the box");
                return;
            }            

            string timeNow = "(" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ")";
            data = timeNow + " Server: " + data;
            byte[] msg;
            msg = System.Text.Encoding.UTF8.GetBytes(data);
            server.Send(msg, msg.Length, clientEndPoint);

            Invoke(new Action(() =>
            {
                if (!messages.Contains(data))
                {
                    messages.Add(data);
                }
                DisplayMessages();
            }));
        }

        private void ListenForMessages()
        {
            while (true)
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] msg = server.Receive(ref remoteEndPoint);
                string data = Encoding.ASCII.GetString(msg);

                if (clientEndPoint == null)
                {
                    clientEndPoint = remoteEndPoint;
                }
                Invoke(new Action(() =>
                {
                    if (!messages.Contains(data))
                    {
                        messages.Add(data);
                    }
                    DisplayMessages();
                }));

            }
        }
        
        #region void
        private void DisplayMessages()
        {
            listViewChat.Items.Clear();
            foreach (string message in messages)
            {
                listViewChat.Items.Add(new ListViewItem(new[] { message }));
            }
        }
        #endregion
    }
}
