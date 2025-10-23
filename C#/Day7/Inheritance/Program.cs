// See https://aka.ms/new-console-template for more information



using Inheritance;

Console.Write("Animal : ");



Animal animal = new Animal();

animal.Speak();

Console.Write("Dog : ");

Dog dog = new Dog();    
dog.Speak();


Console.Write("Cat : ");
Cat cat = new Cat();
cat.Speak();


Console.WriteLine("\n");

Console.Write("Vehicle : ");

Vehicle vehicle = new Vehicle();
vehicle.ShowType();

Console.Write("Car : ");
Car car = new Car();
car.ShowType();
