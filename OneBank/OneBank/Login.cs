using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBank
{
    public  class Login
    {
         public void LogMe(Bank bank)
        {
            Console.WriteLine("-------ONEBANK LOGIN PORTAL----------");
            Console.Write("Enter your email :>>");
            string myemail = Console.ReadLine();

            Console.Write("Enter your password :>>");
            string mypass = Console.ReadLine();

            Accountinfo accountinfo = bank.GetAccountinfos().Find(b=>b.Email == myemail && b.Password == mypass);

            if(accountinfo != null)
            {
                Console.WriteLine("Congrats!, you are Logged in.");
                var dash = new Dashboard();
                dash.Menu(bank);
            }
            else
            {
                Console.WriteLine("Incorrect Credentials");
                Container.Start();
                
            }


        }
    }
}
