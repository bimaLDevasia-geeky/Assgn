using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Accunt
    {
        private long AccountNumber { get; set; }
        private string AccountHolder { get; set; }
        private long Balance { get; set; }


        public Accunt(long Number,string Name)
        {
            this.AccountNumber = Number;
            this.AccountHolder = Name;
            this.Balance = 0;
        }

        public Accunt() :this(0000,"User")
        { 
        }

        public void AddDeposit(long money)
        {
            this.Balance += money;
        }

        public void DisplayDetails()
        {
            Console.WriteLine("The Account Number is "+ AccountNumber);
            Console.WriteLine("The Account Holder is "+ AccountHolder);
            Console.WriteLine("The Balance is "+ Balance);

        }
    }
}
