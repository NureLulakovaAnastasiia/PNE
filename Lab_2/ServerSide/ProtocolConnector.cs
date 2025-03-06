using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public static class ProtocolConnector
    {
        public static void OpenTcp()
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
            server.Start();
            Console.WriteLine("Сервер запущено. Очікування підключення...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Клієнт підключився.");

                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string[] numbers = receivedData.Split(',');
                if (numbers.Length == 2 && int.TryParse(numbers[0], out int num1) && int.TryParse(numbers[1], out int num2))
                {
                    int sum = num1 + num2;
                    Console.WriteLine($"Отримано: {num1} + {num2} = {sum}");

                    byte[] response = Encoding.UTF8.GetBytes(sum.ToString());
                    stream.Write(response, 0, response.Length);
                }
                else
                {
                    Console.WriteLine("Некоректний формат даних.");
                }

                stream.Close();
                client.Close();

            }
        }

        public static void OpenUdp()
        {
            UdpClient server = new UdpClient(8000);
            Console.WriteLine("Сервер запущено. Очікування повідомлень...");

            while (true)
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 8000);
                byte[] receivedBytes = server.Receive(ref clientEndPoint);
                string receivedData = Encoding.UTF8.GetString(receivedBytes);

                Console.WriteLine($"Отримано від клієнта ({clientEndPoint.Address}:{clientEndPoint.Port}): {receivedData}");

                string[] numbers = receivedData.Split(',');
                if (numbers.Length == 2 && int.TryParse(numbers[0], out int num1) && int.TryParse(numbers[1], out int num2))
                {
                    int sum = num1 + num2;
                    Console.WriteLine($"Отримано: {num1} + {num2} = {sum}");

                    byte[] response = Encoding.UTF8.GetBytes(sum.ToString());
                    server.Send(response, response.Length, clientEndPoint);
                }
                else
                {
                    Console.WriteLine("Некоректний формат даних.");
                }
            }
        }
    }
}
