using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide
{
    public static class ClientConnector
    {

        public static void TcpClientConnector()
        {
            while (true)
            {
                try
                {
                    TcpClient client = new TcpClient("127.0.0.1", 8000);
                    NetworkStream stream = client.GetStream();

                    Console.Write("Введіть перше число: ");
                    int num1 = int.Parse(Console.ReadLine());
                    Console.Write("Введіть друге число: ");
                    int num2 = int.Parse(Console.ReadLine());

                    string message = $"{num1},{num2}";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    byte[] buffer = new byte[256];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine($"Отримана сума: {result}");

                    stream.Close();
                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
                Console.ReadLine();
            }
        }


        public static void UdpClientConnector()
        {
            UdpClient client = new UdpClient();
            client.Connect("127.0.0.1", 8000);

            while (true)
            {
                try
                {

                    Console.Write("Введіть перше число: ");
                    int num1 = int.Parse(Console.ReadLine());
                    Console.Write("Введіть друге число: ");
                    int num2 = int.Parse(Console.ReadLine());


                    string message = $"{num1},{num2}";
                    byte[] data = Encoding.UTF8.GetBytes(message);


                    client.Send(data, data.Length);


                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                    byte[] response = client.Receive(ref serverEndPoint);
                    string result = Encoding.UTF8.GetString(response);


                    Console.WriteLine($"Отримана сума: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }

                Console.ReadLine();
            }
        }
    }
}
