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
                kindle.UpdateAllValues();
                Console.WriteLine(kindle.GetStatusUpdate());
                Thread.Sleep(3000);
            }
        }

    }
}
