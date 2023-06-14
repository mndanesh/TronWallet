using Coinobin.Website.Tron;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("What do you whant to do?");
                Console.WriteLine("1. Generate Wallet");
                Console.WriteLine("2. Get Balance");
                var input = Console.ReadLine();
                try
                {
                    switch (input)
                    {

                        case "1":

                            var wallet = Wallet.Generate();
                            var publickey = wallet.Address;
                            var privatekey = wallet.PrivateKey;
                            Console.WriteLine("Address (Public key): " + publickey);
                            Console.WriteLine("Address (Private key): " + privatekey);
                            Console.WriteLine("Press any key to repeat ...");
                            Console.ReadLine();
                            break;
                        case "2":
                            Console.WriteLine("Enter trc20 address:");
                            var address = Console.ReadLine();
                            var balance = Wallet.GetBalance(address);
                            Console.WriteLine("Wallet balance is: ");
                            Console.WriteLine(balance);
                            Console.WriteLine("Press any key to repeat ...");
                            Console.ReadLine();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured.");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to repeat ...");
                    Console.ReadLine();
                }
            }
        }
    }
}
