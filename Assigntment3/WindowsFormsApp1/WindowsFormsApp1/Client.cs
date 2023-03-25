using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;

namespace WindowsFormsApp1
{
    public partial class Client : Form
    {
        private UdpClient client;
        private Thread listenThread;
        private List<string> messages = new List<string>();
        public Client()
        {
            InitializeComponent();
            client = new UdpClient(0);
            listenThread = new Thread(new ThreadStart(ListenForMessages));
            listenThread.Start();

            listViewChat.View = View.Details;
            listViewChat.Columns.Add("Chat with Server", -2, HorizontalAlignment.Left);
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
            data = timeNow + " Client: " + data;
            byte[] msg;
            msg = System.Text.Encoding.UTF8.GetBytes(data);
            client.Send(msg, msg.Length, "localhost", 1000);

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
                byte[] msg = client.Receive(ref remoteEndPoint);
                string data = Encoding.ASCII.GetString(msg);

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
