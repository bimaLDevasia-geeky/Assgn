using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Transactions
{
    public class BorrowTransaction
    { 
        public int TransactionId { get; set; }
        public int Rate { get; set; }
        public  int CustomerId { get; set; }

        public BorrowTransaction() {
            Console.WriteLine("Borrow transaction");
        }
    }
}
