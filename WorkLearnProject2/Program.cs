using System;
using Microsoft.Owin.Hosting;

namespace WorkLearnProject2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:9000"))
            {
                Console.WriteLine("Hello guys");
                Console.Read();
            }
        }
    }
}