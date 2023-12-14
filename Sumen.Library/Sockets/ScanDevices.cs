using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using PhoHa7.Library.Classes.Common;

namespace PhoHa7.Library.Sockets
{
    public class ScanDevices
    {
        private const int BUFSIZE = 32; // Size of receive buffer
        private const int BACKLOG = 5;  // Outstanding connection queue max size
        private int countDevices = 0;
      
        public int CountDevices
        {
            get { return countDevices; }
            set { countDevices = value; }
        }
        string result;

        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        public void StartServer()
        {
            int servPort = 7;
            Socket server = null;

            try
            {
                // Create a socket to accept client connections
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                                    ProtocolType.Tcp);

                server.Bind(new IPEndPoint(IPAddress.Any, servPort));

                server.Listen(BACKLOG);
            }
            catch (SocketException se)
            {
                ClsMsgBox.Loi(se.ErrorCode + ": " + se.Message);
            }

            byte[] rcvBuffer = new byte[BUFSIZE]; // Receive buffer
            int bytesRcvd;                        // Received byte count

            for (; ; )
            { // Run forever, accepting and servicing connections

                Socket client = null;

                try
                {
                    client = server.Accept(); // Get client connection
                    countDevices++;

                    Console.Write("Handling client at " + client.RemoteEndPoint + " - ");

                    // Receive until client closes connection, indicated by 0 return value
                    int totalBytesEchoed = 0;
                    while ((bytesRcvd = client.Receive(rcvBuffer, 0, rcvBuffer.Length,
                                                       SocketFlags.None)) > 0)
                    {
                        client.Send(rcvBuffer, 0, bytesRcvd, SocketFlags.None);
                        totalBytesEchoed += bytesRcvd;
                        result = System.Text.Encoding.ASCII.GetString(rcvBuffer).Trim('\0');
                        MessageBox.Show(String.Format("{0}",result));
                    }

                    

                    client.Close();   // Close the socket. We are done with this client!

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    client.Close();
                }
            }
        }

    }
}
