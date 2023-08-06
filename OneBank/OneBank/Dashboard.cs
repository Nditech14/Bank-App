using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace OneBank
{
    public class Dashboard
    {
 

        public void Menu(Bank bank)
        {
            Console.WriteLine("*Welcome to the menu page*");
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("1. Setup Account");
            Console.WriteLine("2. Check Balance");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Statement Of Account");
            Console.WriteLine("6. Account Info");
            Console.WriteLine("7. Transfer");
            Console.WriteLine("8. Logout");

            Console.WriteLine();

            string option = Console.ReadLine();
            Console.WriteLine(option);

            bool isValidOption = int.TryParse(option, out int selectedOption);
            if (!isValidOption || selectedOption < 1 || selectedOption > 8)
            {
                Console.WriteLine("Invalid input. Please enter a valid menu option (1-8).");
                Menu(bank);
            }
            else
            {
                HandleMenuOption(selectedOption, bank);
            }
        }

        private void HandleMenuOption(int option, Bank bank)
        {
            switch (option)
            {
                case 1:
                    CreateAccount create = new CreateAccount();
                    create.SetupAccount();
                    Menu(bank);
                    break;
                case 2:
                    CheckBalance(bank);
                    Menu(bank); 
                    break;
                case 3:
                    Deposit(bank); 
                    Menu(bank);
                    break;
                case 4:
                    Withdraw(bank);
                    Menu(bank);
                    break;
                case 5:
                    //Console.WriteLine("Below is your account info");
                    CheckStatement(bank);
                    Menu(bank);
                    break;
                case 6:
                    //Console.WriteLine("Below is your statement of account");
                    Print print = new Print();
                    print.Out(bank);
                    Menu(bank);
                    break;
                case 7:
                    //Console.WriteLine("Enter the amount you want to transfer");
                    Transfer(bank);
                    Menu(bank);
                    break;
                case 8:
                    //Print print = new Print();
                    //print.Out();
                    Console.WriteLine("Thank you for banking with us!!!");
                    break;
            }
        }

       

        private void CheckBalance(Bank bank)
        {
            Console.WriteLine("Please enter your Account Number:");
            string accountNumber = Console.ReadLine();

            Accountinfo accountinfo = bank.GetAccountinfos().Find(x=>x.Accountnumber == accountNumber);

            //foreach (Accountinfo account in a)
            //{
                if (accountinfo != null)
                {
                    Console.WriteLine($"Your balance for account number {accountNumber} is {accountinfo.Accountbalance}");
                    return;
                }
            else
            {
                Console.WriteLine("Account number not found. Please try again.");
            }
            
        }

            
        

        private void CheckStatement(Bank bank)
        {
            Console.WriteLine("Please enter your Account Number:");
            string accountNumber = Console.ReadLine();
        Accountinfo accountinfo = bank.GetAccountinfos().Find(x => x.Accountnumber == accountNumber);

        if (accountinfo != null)
        {

                //    foreach (Transaction transaction in accountinfo.Transactions)
                //{

                //    Console.WriteLine($"{transaction.Date}\t{transaction.Description}\t{transaction.Amount}\t{transaction.Balance}");

                //}
                Console.WriteLine("|---------------------|-----------------------------------------------|--------------------------|---------------------|");
                Console.WriteLine("| DATE                | DESCRIPTION                                   | AMOUNT                   | BALANCE             |");
                Console.WriteLine("|---------------------|-----------------------------------------------|--------------------------|---------------------|");

                foreach (Transaction transaction in accountinfo.Transactions)
                {
                    Console.WriteLine($"| {transaction.Date,-10} | {transaction.Description,-45} | {transaction.Amount,-24} | {transaction.Balance,-19} |");
                }

                Console.WriteLine("|----------------------------------------------------------------------------------------------------------------------|");
            }
        else
        {
            Console.WriteLine("Account number not found. Please try again.");
        }


       
        }

        private void Deposit(Bank bank)
        {
            Console.WriteLine("Please enter your Account Number:");
            string accountNumber = Console.ReadLine();

            Console.WriteLine("Enter the amount to deposit:");
            decimal deposit;
            if (!decimal.TryParse(Console.ReadLine(), out deposit) || deposit < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid deposit amount.");
                Deposit(bank);
                return;
            }

            Accountinfo accountinfo = bank.GetAccountinfos().Find(x => x.Accountnumber == accountNumber);
            if (accountinfo != null)
            {
                accountinfo.Accountbalance += deposit;
                Console.WriteLine($"Your account balance for {accountNumber} is {accountinfo.Accountbalance}");

                accountinfo.Transactions.Add(new Transaction
                {
                    Date = DateTime.Now,
                    Description = "Deposit",
                    Amount = deposit,
                    Balance = accountinfo.Accountbalance
                });
                return;
            }
            else
            {
                Console.WriteLine("Account number not found. Please try again.");
            }
        }

        private void Withdraw(Bank bank)
        {
            Console.WriteLine("Please enter your Account Number:");
            string accountNumber = Console.ReadLine();

            Console.WriteLine("Enter the amount you want to Withdraw:");
            decimal withdraw;
            if (!decimal.TryParse(Console.ReadLine(), out withdraw) || withdraw < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid withdrawal amount.");
                Withdraw(bank);
                return;
            }

            Accountinfo accountinfo = bank.GetAccountinfos().Find(x => x.Accountnumber == accountNumber);
            if (accountinfo != null)
            {
                if (withdraw <= accountinfo.Accountbalance)
                {
                    accountinfo.Accountbalance -= withdraw;
                    Console.WriteLine($"Your account balance for {accountNumber} is {accountinfo.Accountbalance}");
                    accountinfo.Transactions.Add(new Transaction
                    {
                        Date = DateTime.Now,
                        Description = "Withdraw",
                        Amount = withdraw,
                        Balance = accountinfo.Accountbalance
                    });
                    return;
                }
                else
                {
                    Console.WriteLine("Insufficient balance. Please enter a valid withdrawal amount.");
                }

                return;
            }
            

            Console.WriteLine("Account number not found. Please try again.");
        }

        private void Transfer(Bank bank)
        {
            Console.WriteLine("Please enter your Account Number:");
            string sourceAccountNumber = Console.ReadLine();

            Console.WriteLine("Please enter Recipient Account Number:");
            string recipientAccountNumber = Console.ReadLine();

            Console.WriteLine("Enter the amount you want to Withdraw:");
            decimal transfer;
            if (!decimal.TryParse(Console.ReadLine(), out transfer) || transfer < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid withdrawal amount.");
                Withdraw(bank);
                return;
            }

            Accountinfo sourceAccountinfo = bank.GetAccountinfos().Find(x => x.Accountnumber == sourceAccountNumber);
            Accountinfo recipientAccountinfo = bank.GetAccountinfos().Find(x => x.Accountnumber == recipientAccountNumber);

            if (sourceAccountinfo != null && recipientAccountinfo != null)
            {
                if (transfer <= sourceAccountinfo.Accountbalance && transfer !< 0)
                {
                    sourceAccountinfo.Accountbalance -= transfer;
                    recipientAccountinfo.Accountbalance += transfer;
                    Console.WriteLine($"Your account balance for {sourceAccountinfo.Accountnumber} is {sourceAccountinfo.Accountbalance}");
                    sourceAccountinfo.Transactions.Add(new Transaction
                    {
                        Date = DateTime.Now,
                        Description = $"transfer to {recipientAccountinfo.Accountnumber}",
                        Amount = transfer,
                        Balance = sourceAccountinfo.Accountbalance
                    });

                    recipientAccountinfo.Transactions.Add(new Transaction
                    {
                        Date = DateTime.Now,
                        Description = $"transfer from {sourceAccountinfo.Accountnumber}",
                        Amount = transfer,
                        Balance = recipientAccountinfo.Accountbalance
                    });
                }
                else
                {
                    Console.WriteLine("Insufficient balance. Please enter a valid withdrawal amount.");
                }

                return;
            }


            Console.WriteLine("Account number not found. Please try again.");
        }

    }
}