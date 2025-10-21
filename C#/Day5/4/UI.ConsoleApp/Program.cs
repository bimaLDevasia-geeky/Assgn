// See https://aka.ms/new-console-template for more information
using BusinessLogic;
using DataAccess;


UserServices userServices = new UserServices();

Console.WriteLine("Getting User details");

List<UserDetails> userDetails = userServices.getActiveUser();

if (userDetails.Any())
{
    foreach (UserDetails item in userDetails)
    {
        Console.WriteLine($"The User Details are \n" +
            $"ID : {item.Id} \n" +
            $"Name : {item.Name} \n" +
            $"Email : {item.Email}\n");
    }
}
else
    Console.WriteLine("No user Found");


Console.WriteLine("Add new User ");

Console.WriteLine("Enter the name");
string name = Console.ReadLine();

Console.WriteLine("Enter the email");
string email = Console.ReadLine();

Console.WriteLine("Enter the password");
string password = Console.ReadLine();


userServices.AddUser(name, email, password);

Console.WriteLine("\n active user list is\n");
userDetails = userServices.getActiveUser();

foreach (UserDetails item in userDetails)
{
    Console.WriteLine($"The User Details are \n" +
        $"ID : {item.Id} \n" +
        $"Name : {item.Name} \n" +
        $"Email : {item.Email}\n");
}