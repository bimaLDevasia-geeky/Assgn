// See https://aka.ms/new-console-template for more information


using System.Collections.Concurrent;
using TemperatureConverte;

TemperatureConverter temperatureConverter = new TemperatureConverter();


double celsius = 100000;
double fahrenheit = 300;

try
{
    Console.WriteLine(temperatureConverter.CelsiusToFahrenheit(celsius)); 
}
catch(Exception e)
{
    Console.WriteLine("Caught an exception "+ e.Message);
}

try
{
    Console.WriteLine(temperatureConverter.FahrenheitToCelsius(fahrenheit));
}
catch(Exception e)
{
    Console.WriteLine("Caught a exception",e.Message);
}

    //this is not accessible
    //because it is a internal class

    // TemperatureValidator _validator = new TemperatureConverte.TemperatureValidator();
