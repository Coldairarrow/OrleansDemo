using IGrains;
using System;

namespace Client2
{
    class Chat : IChat
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now}:Client2 收到 {message}");
        }
    }
}
