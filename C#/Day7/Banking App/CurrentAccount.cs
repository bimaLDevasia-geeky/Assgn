using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    public class CurrentAccount
    {
        private double _balance = 0;

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                _balance = _balance + amount;
                Console.WriteLine("Amount Added to Current Account");
            }
        }

        public void WithDraw(double amount)
        {
            if (amount > _balance)
            {
                Console.WriteLine("Insufficient Balance");
                return;
            }
            _balance -= amount;
            Console.WriteLine($"{amount} is Withdrawn from Account");
        }

        public double GetBalance()
        {
            return _balance;
        }
    }
}
