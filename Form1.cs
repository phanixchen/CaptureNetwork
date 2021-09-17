using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace CaptureNetwork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Thread t;
        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(run);
            t = new Thread(ts);
            t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                socket.Disconnect(false);
            }
            catch
            {
            }

            try
            {
                t.Abort();
            }
            catch
            {
            }
        }

        private Socket socket;
        private void run()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("10.91.26.216", 21);

            int nextBlocksize = 0;

            NetworkStream s = new NetworkStream(socket);

            byte[] b = new byte[10];
            byte[] blen = new byte[4];
            int index =0;
            while (true)
            {
                if (socket.Available < 4)
                    continue;

                s.Read(blen, 0, 4);

                nextBlocksize = BitConverter.ToInt32(blen, 0);


                Console.WriteLine(nextBlocksize);

                //s.Read(b, index, 10);
                //index = index + 10;
                //char [] cArray= System.Text.Encoding.ASCII.GetString(b).ToCharArray();

                //Console.WriteLine(cArray);
            }
        }
    }
}
