using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Transactions
{
    public class ReturnTransaction
    {

        public int Id { get; set; }
        public int Rate { get; set; }
        public int CustomerId { get; set; }

        public ReturnTransaction() {
            Console.WriteLine("Return Transaction");
        }

    }
}
