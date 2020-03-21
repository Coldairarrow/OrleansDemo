using IGrains;
using System;

namespace Client1
{
    class Chat : IChat
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now}:Client1 收到 {message}");
        }
    }
}
