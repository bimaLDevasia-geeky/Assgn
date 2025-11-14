// See https://aka.ms/new-console-template for more information
using Banking;
using Microsoft.Identity.Client;



AccountOperations accountOperations = new AccountOperations();

DateTime date1 = DateTime.Parse("1987-04-12");
DateTime date2 = DateTime.Parse("1990-08-25");
DateTime date3 = DateTime.Parse("1985-11-03");
DateTime date4 = DateTime.Parse("1992-07-18");
DateTime date5 = DateTime.Parse("1989-02-14");

accountOperations.AddCustomer("John Smith", "john.smith@email.com", "857", date1, "123 Main St, New York, USA");
accountOperations.AddCustomer("Maria Gonzalez", "maria.gonzalez@gmail.com", "1601", date2, "45 Calle Mayor, Madrid, Spain");
accountOperations.AddCustomer("Liam O’Connor", "liam.oconnor@outlook.com", "-907779", date3, "89 Abbey Rd, London, UK");
accountOperations.AddCustomer("Sophia Müller", "sophia.mueller@gmail.com", "2345780", date4, "22 Berliner Str, Berlin, Germany");
accountOperations.AddCustomer("Ethan Brown", "ethan.brown@yahoo.com", "1374", date5, "17 King St, Sydney, Australia");

accountOperations.DisplayCustomer();

accountOperations.UpdateData(3,"yornille");
Console.WriteLine("\nAfter Updating \n");

accountOperations.DisplayCustomer();

Console.WriteLine("\nAfter Deleting \n");

accountOperations.DeleteCustomer(3);
accountOperations.DisplayCustomer();