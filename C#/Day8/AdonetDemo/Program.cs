// See https://aka.ms/new-console-template for more information
using AdonetDemo;
using System;

Employee employee = new Employee();


employee.CreateTable();


 employee.Insert("Antony",34000);
employee.Insert("Rahul",10000);
 employee.Insert("Ibrahim",12000);
 employee.Insert("Manu",23000);


Console.WriteLine("------------------After Insert-------------------");
employee.ReadAllData();

Console.WriteLine("------------------After Update-------------------");
employee.UpdateById("Rahul Destro", 15000, 3);
employee.ReadAllData();
System.Console.WriteLine();

Console.WriteLine("------------------After Delete-------------------");
employee.DeleteById(1);
employee.ReadAllData();


employee.DisplayCount();
employee.DisconnectedRead();
employee.UpdateViaDisconnected(4, "zebra", 233);
employee.DisconnectedRead();


Student student = new Student();

student.InsertData("Bimal", "12th");
student.GetStudentViaId(1);
student.InsertIntoStudent("richard", "7th");

student.DeleteStudent(1);
student.UpdateStudent("hurgeer","3rd",2);