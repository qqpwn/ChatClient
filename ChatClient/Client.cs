using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClient
{

    class Client
    {

        static void Main(string[] args)
        {
            ExecuteClient();

        }

        static void ExecuteClient()
        {

            try
            {

                //IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = IPAddress.Parse("192.168.0.197");
                IPEndPoint iPEndPoint = new IPEndPoint(ipAddr, 80);

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    sender.Connect(iPEndPoint);

                    Console.WriteLine("Socket conneted to -> {0} ", sender.RemoteEndPoint.ToString());

                    byte[] messageSent = Encoding.ASCII.GetBytes("Din mor er en luder");
                    int bytesSent = sender.Send(messageSent);

                    byte[] messageReceived = new byte[1024];

                    int bytesRecv = sender.Receive(messageReceived);
                    Console.WriteLine("Message from server -> {0}", Encoding.ASCII.GetString(messageReceived, 0, bytesRecv));

                    if (Console.ReadLine() == "q")
                    {
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }
                   

                }
                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString()); ;
                }
                catch (SocketException anr)
                {
                    Console.WriteLine("SocketException : {0}", anr.ToString());
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Unexpected Exception {0}", e.ToString());
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString()); ;
            }
        }


    }
}