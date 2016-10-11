using System;
using System.Collections.Generic;
using System.Text;

namespace TouchMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nTouchMe by Zachary Burns (touchme@zackburns.com)\n");
            if (args.Length == 4)
            {
                bool flag;
                DateTime time;
                try
                {
                    flag = bool.Parse(args[2]);
                }
                catch
                {
                    Console.WriteLine("\n\nInvalid third parameter. \n\tIt is '" + args[2] + "', should be 'True' or 'False'. Assuming False");
                    flag = false;
                }
                try
                {
                    time = DateTime.Parse(args[3]);
                }
                catch
                {
                    Console.WriteLine("\n\nInvalid timestamp parameter. \n\tIt is '" + args[3] + "', should be a valid DateTime");
                    Console.WriteLine("\tCannot continue...");
                    return;
                }
                new Touch(args[0], args[1], flag).Go(time);
            }
            else
            {
                PrintUsage();
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("\n\nUsage: touchme.exe <root dir> <pattern> <recursive> <timestamp>");
            Console.WriteLine("Example usage: \"C:\\A Long Path\\Stuff To Touch\" *.* true \"01/02/2003 01:02:03\"");
        }
    }
}
