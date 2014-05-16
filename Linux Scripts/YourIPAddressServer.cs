using System;
using System.Collections.Generic;
using System.Text;

namespace YourIPAddressServer
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.IPAddress ip_to_bind_to = System.Net.IPAddress.Parse("--------IP ADDRESS HERE-------------");
            System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(ip_to_bind_to,-------------PORT----------------);
            listener.Start();
            while (true)
            {
                System.Net.Sockets.Socket socket = listener.AcceptSocket();
                string ip = socket.RemoteEndPoint.ToString();
                System.IO.File.WriteAllLines("-----------------DIRECTORY AND FILE SAVE LOCATION---------------------", new string[] {System.DateTime.Now + "\t" + ip});
                //just send ip address back to user and close connection
                socket.Send(new ASCIIEncoding().GetBytes(socket.RemoteEndPoint.ToString()));
                socket.Close();
            }
            listener.Stop();
        }
    }
}
