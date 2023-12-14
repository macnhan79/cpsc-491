using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for SocketClient
/// </summary>
public class SocketClient
{

    public string ConnectAndSendMsg(string address, int port, string message)
    {
        string response = "";
        // Set up a listener on that address/port
        TcpClient tcpClient = new TcpClient(address, port);
        if (tcpClient != null)
        {
           // message = "Hello there";
            // Translate the passed message into UTF8ASCII and store it as a Byte array.
            byte[] bytes = Encoding.ASCII.GetBytes(message);

            NetworkStream stream = tcpClient.GetStream();

            // Send the message to the connected TcpServer. 
            // The write flushes the stream automatically here
            stream.Write(bytes, 0, bytes.Length);

            // Get the response from the server

            //StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            //try
            //{
            //    response = reader.ReadToEnd();
            //}
            //finally
            //{
            //    // Close the reader
            //    reader.Close();
            //}

            // Close the client
            tcpClient.Close();
        }
        // Return the response text
        return response;
    }




}