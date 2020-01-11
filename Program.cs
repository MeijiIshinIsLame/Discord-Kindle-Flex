using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace kindleflex
{
    class Program
    {
        static void Main(string[] args)
        {  
            Kindle kindle = new Kindle();

            while (true)
            {
                string percentComplete = kindle.GetPercentComplete();
                Console.WriteLine(percentComplete);
                Thread.Sleep(1000);
            }
        }
    }
}
