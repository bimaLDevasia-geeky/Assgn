using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    public class PaymentProcesser
    {
        private readonly IPaymentService _paymentService;

        public PaymentProcesser(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public void Process(double amount)
        {
            _paymentService.MakePayment(amount);
        }

    }
}
