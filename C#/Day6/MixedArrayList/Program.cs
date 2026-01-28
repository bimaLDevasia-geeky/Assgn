// See https://aka.ms/new-console-template for more 

using System.Collections;


ArrayList MixedList = new ArrayList();

MixedList.Add("John");
MixedList.Add(25);
MixedList.Add(75.5);
MixedList.Add(true);


foreach (var i in MixedList)
{
    Console.WriteLine($"{i} type->{i.GetType()}");
}
