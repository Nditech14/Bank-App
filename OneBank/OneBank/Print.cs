using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBank
{
    public class Print//:Dashboard
    {
     public  void Out(Bank bank)
        {
            string retrieve = "";
            foreach (Accountinfo acc in bank.accounts)
            {
                retrieve += $"|     {acc.FullName,-15}|   {acc.Accountnumber,-15}|     {acc.Accounttype,-15}|      {acc.Accountbalance,-16}|\n";
            }
            Console.WriteLine("|--------------------|------------------|--------------------|----------------------|");
            Console.WriteLine("|     FULLNAME       |  ACCOUNT NUMBER  |   ACCOUNT TYPE     |   ACCOUNT BALANCE    |");
            Console.WriteLine("|--------------------|------------------|--------------------|----------------------|");
            Console.Write(retrieve);
            Console.WriteLine("------------------------------------------------------------------------------------");
        }

    }


}
