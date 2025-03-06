using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class TestConnection
    {
        static int successfulConnections = 0;   
        static object lockObj = new object();

        static async Task SendRequestTcp()
        {
            try
            {
                using TcpClient client = new TcpClient("127.0.0.1", 8000);
                using NetworkStream stream = client.GetStream();
                string message = "5,7";
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);
                byte[] buffer = new byte[256];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                lock (lockObj)
                {
                    successfulConnections++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підключення: {ex.Message}");
            }
        }

        public static async Task CountTcp()
        {
            int left = 10000, right = 30000, maxConnections = 0;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                successfulConnections = 0;

                Console.WriteLine($"Тестуємо {mid} підключень...");

                var tasks = new List<Task>();
                for (int i = 0; i < mid; i++)
                {
                    tasks.Add(SendRequestTcp());
                }

                await Task.WhenAll(tasks);

                Console.WriteLine($"Успішних підключень: {successfulConnections}");

                if (successfulConnections == mid)
                {
                    maxConnections = mid;
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            Console.WriteLine($"Максимальна кількість одночасних підключень: {maxConnections}");
        }


        static async Task SendRequestUdp()
        {
            try
            {
                using UdpClient client = new UdpClient();
                client.Connect("127.0.0.1", 8000); 

                string message = "5,7";
                byte[] data = Encoding.UTF8.GetBytes(message);

                await client.SendAsync(data, data.Length);

                UdpReceiveResult response = await client.ReceiveAsync();
                string result = Encoding.UTF8.GetString(response.Buffer);

                lock (lockObj)
                {
                    successfulConnections++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підключення: {ex.Message}");
            }
        }


        public static async Task CountUdp()
        {
            int left = 200000, right = 700000, maxConnections = 0;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                successfulConnections = 0;

                Console.WriteLine($"Тестуємо {mid} підключень...");

                var tasks = new List<Task>();
                for (int i = 0; i < mid; i++)
                {
                    tasks.Add(SendRequestUdp());
                }

                await Task.WhenAll(tasks);

                Console.WriteLine($"Успішних підключень: {successfulConnections}");
                if (successfulConnections == mid)
                {
                    maxConnections = mid;
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            Console.WriteLine($"Максимальна кількість одночасних підключень: {maxConnections}");
        }

    }
}
