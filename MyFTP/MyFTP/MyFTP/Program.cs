﻿global using System;
using System.Net;
namespace MyFTP;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 2 && IPAddress.TryParse(args[0], out IPAddress? ip) && int.TryParse(args[1], out int port))
        {
            var server = new Server(ip, port);
            server.ServerMethod();
            var command = "";
            while (command != "exit")
            {
                Console.WriteLine("To stop server write: \"exit\"");
                command = Console.ReadLine();
            }
            server.Cts.Cancel();
        }
        else
        {
            Console.WriteLine("Program requires two command line options, use them in the following order: ip, port");
        }
    }
}
