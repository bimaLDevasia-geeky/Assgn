// See https://aka.ms/new-console-template for more 



using Banking;
using Banking.Model;
using Microsoft.EntityFrameworkCore.Update;

Address address = new Address();

address.City = "rghdghthala";
address.Street = "rdhu";
address.PostalCode = 688524;
address.Country = "drhia";
address.State = "Kerala";


Account account = new Account();
account.Balance = 56000;
account.AccountNumber = "dtrhyd59283579257878";

Customer customer = new Customer();

customer.Address = address;
customer.Email = "rdyhrn@najgn.com";
customer.PhoneNumber = "12rdyhd0";
customer.FullName = "BimalDyjfjyj";

DateTime dob = DateTime.Parse("2003-02-19");
customer.DateOfBirth = dob;
customer.accounts.Add(account);


AccountOperations accountOperations = new AccountOperations();
accountOperations.AddCustomer(customer);
accountOperations.Display();


Address ad1= new Address() { City="Custica",Street="LivingTon",State="Burke",Country="Belgium",PostalCode=123456 };
accountOperations.AddAddress(2, ad1);

Account acc = new Account() { AccountNumber="245678876543",Balance=2345 };
accountOperations.AddAccount(1, acc);
accountOperations.DeleteAccount(1, 1);
Console.WriteLine("\n\n");
accountOperations.Display();

