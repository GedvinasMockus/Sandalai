﻿using Microsoft.Owin.Hosting;

using System;

namespace SignalR.Sandalai
{
    public class Program
    {
        static void Main(string[] args)
        {
            var url = "http://192.168.1.182:8081";
            using (WebApp.Start(url))
            {
                Console.WriteLine($"Server running on {url}");
                Console.ReadLine();
            }
        }
    }

}
