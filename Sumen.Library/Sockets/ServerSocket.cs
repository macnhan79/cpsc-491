using System;
using System.Net;
using System.Net.Sockets;
using PhoHa7.Library.Classes.Common;

namespace PhoHa7.Library.Sockets
{
    public class ServerSocket
    {
        public event EventHandler ReceivesMsgEvent;
        delegate void ListenerHandler();
        ListenerHandler listenerHandler;

        const int BUFSIZE = 32; // Size of receive buffer
        const int BACKLOG = 5;  // Outstanding connection queue max size
        int maxClient;
        int clientCount = 0;
        bool isRunning = true;
        MessageSocket msgReceive;

        Socket serverSocket = null;
        int serverPort = 7;
        ClientSocket[] clientSockets;



        public ServerSocket(int pPort, int pMaxClient)
        {
            serverPort = pPort;
            maxClient = pMaxClient;
            clientSockets = new ClientSocket[maxClient];
            //
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, serverPort));
            serverSocket.Listen(BACKLOG);
        }




        #region Method

        void AddClient(Socket pNewClientSocket)
        {
            if (maxClient > clientCount)
            {
                for (int i = 0; i < maxClient; i++)
                {
                    if (clientSockets[i] == null)
                    {
                        clientSockets[i] = new ClientSocket(i, this, pNewClientSocket);
                        clientSockets[i].ReceiveAsync();
                        clientCount++;
                        break;
                    }
                }
            }
        }

        public void StartServer()
        {
            try
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    AddClient(e.AcceptSocket);
                    StartServer();
                });
                serverSocket.AcceptAsync(socketEventArg);
            }
            catch (SocketException se)
            {
                ClsMsgBox.Loi(se.ErrorCode + ": " + se.Message);
            }
        }

        #endregion

        #region Property

        public ClientSocket[] ClientSockets
        {
            get { return clientSockets; }
            set { clientSockets = value; }
        }

        public virtual MessageSocket MsgReceive
        {
            get { return msgReceive; }
            set
            {
                msgReceive = value;
                if (ReceivesMsgEvent != null)
                   ReceivesMsgEvent(this, EventArgs.Empty);
            }
        }

        #endregion


    }
}
