using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Banking_App
{
    public class CreditCardPayment: IPaymentService
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine("Payment done by Credit Card");
        }
    }

    public class UPIPayment:IPaymentService
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine("Payment done by UPI payments");
        }
    }

    public class NetBankingPayment : IPaymentService
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine("Payment done by NetBankingPayment");
        }
    }


}
