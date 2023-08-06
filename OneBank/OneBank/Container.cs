using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBank
{
    public static class Container
    {
            public static void Start()
            {
                Bank bank = new Bank();
                Console.WriteLine("Welcome to OneBank. What would you like to do today ?");
                Console.WriteLine(">Press 1 for Registration\n>Press 2 for login\n>Press 3 to exit.");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    var reg = new CreateAccount();
                    reg.RegisterMe(bank);
                }
                else if (choice == "2")
                {
                    var log = new Login();
                    log.LogMe(bank);
                }
                else
                {
                    Console.WriteLine("Thanks for your time with us. Bye.");
                    Environment.Exit(0);
                }
            }
    }
}
