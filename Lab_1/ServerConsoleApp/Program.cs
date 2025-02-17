using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using StringChangeLibrary;


// 1. Створити канал між клієнтом та сервером, створити об’єкт типу HttpChannel
HttpChannel ch = new HttpChannel(5000); // номер порту = 5000 – обрано випадковим чином
 // 2. Зареєструвати канал ch, в методі RegisterChannel() вказано рівень безпеки false
 ChannelServices.RegisterChannel(ch, false);
// 3. Зареєструвати сервіс як WKO
RemotingConfiguration.RegisterWellKnownServiceType(
typeof(ClassLibrary1.Class1),
"MathFunctions.soap",
WellKnownObjectMode.Singleton);
// 4. Вивести повідомлення, що сервіс у стані виконання
Console.WriteLine("MathFunctions service is ready...");
// 5. Зупинити, залишити сервіс у стані виконання, до тих пір, поки не буде
натиснуто клавішу Enter
10
 Console.ReadLine();
