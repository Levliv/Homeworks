﻿using System.Net;
namespace MyFTP;

static class Program
{
    static void Main(string[] args)
    {
        int port;
        string ipString;
        IPAddress? ip;
        string? request;
        if (int.TryParse(args[1], out port) && IPAddress.TryParse(args[0], out ip))
        {
            ipString = args[0];
            Console.WriteLine($"port {port} and ip {ip} recognised successfully");
            while (true)
            {
                Console.WriteLine("Enter your requset, master");
                request = Console.ReadLine();
                if(request == null || ip == null)
                {
                    throw new ArgumentNullException("Request and ip should not be NULL");
                }
                var client = new Client(ipString, port);
                client.ClientRequest(request);
            }
        }
        else
        {
            Console.WriteLine($"port or ip is not recognised");
        }
    }
}
