using System.Net.Sockets;
using System.Net;
using System.Text;


Console.OutputEncoding = System.Text.Encoding.Unicode;
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