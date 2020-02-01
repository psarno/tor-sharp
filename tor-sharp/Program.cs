using System;
using System.Diagnostics;
using System.Net;

namespace Tor
{
    public class Program
    {
        private static void Main()
        {

            const string TOR_PATH = @"D:\Tor\tor.exe";

            string external_ip = new WebClient().DownloadString("https://icanhazip.com/");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Process.Start(TOR_PATH, "--HTTPTunnelPort 4711");

            var proxy = new WebProxy(IPAddress.Loopback.ToString(), 4711);
            var client = new WebClient { Proxy = proxy };
            var bytes = client.DownloadData("https://canihazip.com/s");
            string tor_ip = System.Text.Encoding.UTF8.GetString(bytes);

            ShowOutput(external_ip, tor_ip);

            Console.ReadKey();

        }

        /// <summary>
        /// Displays the current public IP address and the TOR IP address colorized to the console
        /// </summary>
        /// <param name="external_ip">The default external IP address</param>
        /// <param name="tor_ip">The TOR assigned IP address.</param>
        private static void ShowOutput(string external_ip, string tor_ip)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Your public IP Address is {external_ip}");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Your TOR IP Address is ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(tor_ip);

        }
    }
}
