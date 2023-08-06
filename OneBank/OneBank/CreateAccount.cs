using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OneBank
{
    public class CreateAccount
    {   
        public string fullName;
        public string password;
        public string email;

        public static bool ValidatingInput(string input, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        public void RegisterMe(Bank bank)
        {
            
            Console.WriteLine("Start your registration Process here.");
            Console.WriteLine("Dear Customer please enter your fullname: ");
            string fullName = Console.ReadLine();
            while(!ValidatingInput(fullName, @"^[A-Z][a-zA-Z\s]+$"))
            {
                Console.WriteLine("Please input a valid fullname: ");
                fullName = Console.ReadLine();
            }
         
                //Fullname += FName;
                //Password += pwd;

                Console.WriteLine("Please enter your Email Address: ");
                string email = Console.ReadLine();
            while (!ValidatingInput(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                Console.WriteLine("Please input a valid Email Address: ");
                email = Console.ReadLine();
            }
            Console.WriteLine("Please enter your perferred password: ");
            string password = Console.ReadLine();
            while (!ValidatingInput(password, @"^(?=*[@#&%^&!])(?=.*[a-ZA-Z0-9]).{6}$"))
            {
                Console.WriteLine("INVALID INPUT! - Please enter a valid your perferred password: ");
                password = Console.ReadLine();
            }
            //Email += mail;
            var accountDetails = SetupAccount();

                Accountinfo accountinfo = new Accountinfo
                {
                    FullName = fullName,
                    Password = password,
                    Email = email,
                    Accounttype = (string)accountDetails[1],
                    Accountnumber = (string)accountDetails[0],
                    Accountbalance = (decimal)accountDetails[3],
               
                };
            

                bank.AddAccount(accountinfo);
            
                accountinfo.Transactions.Add(new Transaction
                {
                     Date = DateTime.Now,
                    Description = "Initial Deposit",
                     Amount = (decimal)accountDetails[2],
                     Balance = accountinfo.Accountbalance
                });
            Console.WriteLine("Congratulations! your registration was successful\nYour details have been saved.");


            Console.WriteLine("Do you want to login ? Y or N");
            string response = Console.ReadLine();
            if (response == "y" || response == "Y")
            {
                var log = new Login();
                log.LogMe(bank);
            }
            else
            {
                Container.Start();
            }
        }

        public List<object> SetupAccount()
        {
            string accountType = "";
            string accountNumber = "";
            decimal accountBalance = 0;

            Console.WriteLine("Select Account type:");
            Console.WriteLine("1. Current Account");
            Console.WriteLine("2. Savings Account");

            string accountTypeInput = Console.ReadLine();
            bool isValidAccountType = int.TryParse(accountTypeInput, out int selectedAccountType);
            if (!isValidAccountType || (selectedAccountType != 1 && selectedAccountType != 2))
            {
                Console.WriteLine("Invalid input. Please enter a valid account type (1 or 2).");
                SetupAccount();
                //return;
            }

            if (selectedAccountType == 1)
            {
                accountType = "Current";

                Random random = new Random();
                accountNumber = random.Next(1000000000, 2099999999).ToString();
                Console.WriteLine($"Your {accountType} Account number is: {accountNumber}");
                Console.WriteLine($"Your {accountType} account balance is: {accountBalance}");

            }
            if (selectedAccountType == 2)
            {
                accountType = "Savings";

                Random random = new Random();
                accountNumber = random.Next(1000000000, 2099999999).ToString();
                Console.WriteLine($"Your {accountType} Account number is: {accountNumber}");
                Console.WriteLine($"Your {accountType} account balance is: {accountBalance}");
            }

                Console.WriteLine("Make an initial deposit:");
                decimal deposit;
                if (!decimal.TryParse(Console.ReadLine(), out deposit) || deposit < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid deposit amount.");
                    SetupAccount();
                    //return;
                }

                accountBalance += deposit;
                Console.WriteLine($"Your initial deposit is: {deposit}");

                //Accountinfo all = new Accountinfo(FullName, accountNumber, accountType, accountBalance);
                //Program.AllInfo.Add(all);
                return new List<object> { accountNumber, accountType, deposit, accountBalance };
            
        }
    }
}
 