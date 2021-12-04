﻿using System.Net;
using System.Text;

namespace MyFTPClient;

static class Program
{
    static void Main(string[] args)
    {
        if (int.TryParse(args[1], out int port) && IPAddress.TryParse(args[0], out IPAddress? ip) && int.TryParse(args[2], out int requestCode))
        {
            string ipString = args[0];
            Console.WriteLine($"port {port} and ip {ip} recognised successfully");
            while (true)
            {
                string? path = args[3];
                if (path == null || ip == null)
                {
                    throw new ArgumentNullException("Path should not be NULL");
                }
                var client = new Client(ipString, port);
                if (requestCode == 2)
                {
                    var GetResponse = client.Get(path);
                    Console.WriteLine(Encoding.UTF8.GetString(GetResponse.Data));
                }
                else if (requestCode == 1)
                {
                    var ResultsOfListResponse = client.List(path);
                    Console.Write(ResultsOfListResponse.Count() + " ");
                    foreach (var item in ResultsOfListResponse)
                    {
                        Console.WriteLine(item.Name + " " + item.IsDir + " ");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"port or ip is not recognised");
        }
    }
}
