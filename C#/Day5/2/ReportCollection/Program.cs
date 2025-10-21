// See https://aka.ms/new-console-template for more information


using CompA = CompanyA.Reporting.Report;
using CompB = CompanyB.Analytics.Report;

Console.WriteLine("Report of Company A is");
CompA report1 = new CompA();
report1.Generate();
Console.WriteLine("\n");
Console.WriteLine("Report of Company B is");
CompB report2 = new CompB();
report2.Generate();