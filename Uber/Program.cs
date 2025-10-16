// See https://aka.ms/new-console-template for more information



using Uber;

Ride ride1 = new Ride("Raju","Singh",2.4);
Ride ride2 = new Ride("Sasi","John",6);
Ride ride3 = new Ride("Johnson","Irfan",4.2);

Console.WriteLine("Details of Rides\n");
ride1.ShowRideDetails();
ride2.ShowRideDetails();
ride3.ShowRideDetails();


Console.WriteLine("Ride Summary");
Ride.ShowRideSummary();

Console.WriteLine("Changing SurgeMultiplier");
Ride.SetSurgeMultiplier(2);

Ride ride4 = new Ride("ThinkPad", "Manie", 4.2);

Console.WriteLine("Details of new Ride\n");
ride4.ShowRideDetails();

Console.WriteLine("Ride Summary");
Ride.ShowRideSummary();



