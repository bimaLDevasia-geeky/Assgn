// See https://aka.ms/new-console-template for more information
using Banking_App;


Console.WriteLine("SAVINGS");
Console.WriteLine("-------------");

SavingsAccount savingsAccount = new SavingsAccount();

savingsAccount.Deposit(23000);
Console.WriteLine("Balance is "+savingsAccount.GetBalance());
savingsAccount.WithDraw(10000);
Console.WriteLine("Balance is "+savingsAccount.GetBalance());

Console.WriteLine();
Console.WriteLine("CURRENT");
Console.WriteLine("-------------");

CurrentAccount currentAccount = new CurrentAccount();

currentAccount.Deposit(23450);
Console.WriteLine("Balance is " + currentAccount.GetBalance());
currentAccount.WithDraw(2300);
Console.WriteLine("Balance is " + currentAccount.GetBalance());




IPaymentService paymentService = new CreditCardPayment();

PaymentProcesser processor1 = new PaymentProcesser(paymentService);
processor1.Process(23000);

paymentService = new UPIPayment();

PaymentProcesser processor2 = new PaymentProcesser(paymentService);
processor2.Process(23000);

paymentService = new NetBankingPayment();

PaymentProcesser processor3 = new PaymentProcesser(paymentService);
processor3.Process(23000);