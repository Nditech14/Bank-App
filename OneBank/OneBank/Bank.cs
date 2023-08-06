using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBank
{
    public class Bank
    {
        public List<Accountinfo> accounts { get; set; }

        public Bank() 
        {
            accounts = new List<Accountinfo>();
        }

        public List<Accountinfo> GetAccountinfos()
        {
            return accounts;
        }

        public void AddAccount(Accountinfo account)
        {
            accounts.Add(account);
        }

        public void Menu()
        {
            throw new NotImplementedException();
        }
    }
}
