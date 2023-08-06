using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBank
{
    public  class Accountinfo : Registration
    {
        public string Accountnumber { get; set; }
        public string Accounttype { get; set; }
        public decimal Accountbalance { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Accountinfo() 
        {
            Transactions = new List<Transaction>();
        }


    }
}
