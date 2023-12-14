using System;
using System.Net.Sockets;
using System.Text;

namespace PhoHa7.Library.Sockets
{
    public class ClientSocket
    {
        Socket clientSocket;
        ServerSocket serverSocket;
        MessageSocket msgReceive;
        const int MAX_BUFFER_SIZE = 2048;
        int index;

        public ClientSocket(int pIndex, ServerSocket pServerSocket, Socket pClientSocket)
        {
            serverSocket = pServerSocket;
            clientSocket = pClientSocket;
            msgReceive = new MessageSocket();
            index = msgReceive.Index = pIndex;
        }


        public void Send(string pMsg)
        {
            SocketAsyncEventArgs receiveAsyncEvent = new SocketAsyncEventArgs();
            receiveAsyncEvent.RemoteEndPoint = clientSocket.RemoteEndPoint;
            receiveAsyncEvent.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);
            receiveAsyncEvent.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                string response = e.SocketError.ToString();
                if (!clientSocket.Connected)
                {
                    serverSocket.ClientSockets[index] = null;
                }
            });
            byte[] payload = Encoding.UTF8.GetBytes(pMsg);
            receiveAsyncEvent.SetBuffer(payload, 0, payload.Length);
            clientSocket.SendAsync(receiveAsyncEvent);
        }

        public void ReceiveAsync()
        {
            SocketAsyncEventArgs receiveAsyncEvent = new SocketAsyncEventArgs();
            receiveAsyncEvent.RemoteEndPoint = clientSocket.RemoteEndPoint;
            receiveAsyncEvent.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);
            receiveAsyncEvent.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                if (e.SocketError == SocketError.Success)
                {
                    msgReceive.Message = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                    msgReceive.Message = msgReceive.Message.Trim('\0');
                }
                else
                {
                    msgReceive.Message = e.SocketError.ToString();
                }
                ReceiveAsync();
                serverSocket.MsgReceive = msgReceive;
            });
            clientSocket.ReceiveAsync(receiveAsyncEvent);
        }






    }
}
