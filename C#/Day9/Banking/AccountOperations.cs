using Banking.Data;
using Banking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class AccountOperations
    {

        BankingAppDbContext db = new BankingAppDbContext();
        public  void  AddCustomer(string fullName,string email,string phoneNumber,DateTime dateOfBirth,string address)
        {
            Customer customer = db.Customers.Where(x => x.Email == email).FirstOrDefault();

                if(customer == null)
                 {
                Customer c = new Customer() 
                {
                    FullName =fullName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    DateOfBirth = dateOfBirth,
                    Address = address,
                    CreatedAt = DateTime.Now,
                };
                 db.Customers.Add(c);
                db.SaveChanges();
            }
                else
                {
                Console.WriteLine("Email Exist ");
            }
        }

        public  void UpdateData(int id,string address)
        {
            Customer cus =  db.Customers.Where(x=>x.Id == id).FirstOrDefault();
            if(cus == null)
            {
                Console.WriteLine("The Customer doesnt exist");
                return;
            }

                cus.Address = address;
                db.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            Customer cus = db.Customers.Where(x => x.Id == id).FirstOrDefault();
            if(cus == null)
            {
                Console.WriteLine("THe user doesn't exist ");
                return;
            }
            db.Customers.Remove(cus);
            db.SaveChanges();
        }

        public void DisplayCustomer()
        {
            List<Customer> CustomerList = db.Customers.ToList();

            if (CustomerList.Count == 0)
            {
                Console.WriteLine("No customers found in the database.");
                return;
            }
            Console.WriteLine("\n--- All Customer Details ---");
            foreach (var customer in CustomerList)
            {
                Console.Write($"ID: {customer.Id} \t");
                Console.Write($"  Name: {customer.FullName} \t");
                Console.Write($"  Email: {customer.Email}  \t");
                Console.Write($"  Phone: {customer.PhoneNumber}  \t");
                Console.Write($"  Address: {customer.Address}  \t");

                Console.Write($"  Datecreated: {customer.CreatedAt:yyyy-MM-dd} \t");
                Console.Write($"  DOB: {customer.DateOfBirth:yyyy-MM-dd} \n\n ");
            }
                Console.Write("-----------------------------");
        }
    }
}
