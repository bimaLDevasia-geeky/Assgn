using Banking.Data;
using Banking.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{

    
    public interface IAccountOperations {
        public  void AddCustomer() { } 
        public void Display() { }

    }


    public class AccountOperations : IAccountOperations
    {
        private readonly BankingDbcontext db = new BankingDbcontext();
        public void AddCustomer(Customer cust)
        {
            db.Customers.Add(cust);
            db.SaveChanges();
        }


        public void Display()
        {
             List<Customer> customers =db.Customers.Include(a=>a.Address).Include(b=>b.accounts).ToList() ;
            Console.WriteLine("Name\t\tStreet\tCity\tPostalCode\tCountry\tAccountNO\tBalance");
            foreach (Customer customer in customers)
            {
                foreach(Account acc in customer.accounts)
                {
                    Console.WriteLine($"{customer.FullName}\t{customer.Address.Street}\t{customer.Address.City}\t" +
                    $"{customer.Address.PostalCode}\t{customer.Address.Country}\t{acc.AccountNumber}\t{acc.Balance}");
                }
                
            }
        }

        public void AddAddress(int cusId,Address address)
        {
            Customer cus = db.Customers.Include(a=>a.Address).FirstOrDefault(c=>c.Id==cusId);

            if (cus!=null)
            {
                if (cus.Address == null)
                {
                    cus.Address = address;
                }
                else
                {
                    cus.Address.PostalCode = address.PostalCode;
                    cus.Address.Country = address.Country;
                    cus.Address.Street = address.Street;
                    cus.Address.City = address.City;
                    cus.Address.State = address.State;
                }

                db.SaveChanges() ;
            }
        }

        public void AddAccount(int cusId,Account acc)
        {
            Customer customer = db.Customers.Where(c => c.Id == cusId).FirstOrDefault();
            if (customer!=null)
            {
                customer.accounts.Add(acc);
                db.SaveChanges() ;

            }
        }

        public void DeleteAccount(int cusId,int accId)
        {
            Account ac = db.Accounts.FirstOrDefault(a => a.CustomerId == cusId && a.Id == accId);
            if (ac!=null)
            {
                db.Accounts.Remove(ac);
                db.SaveChanges() ;
            }
        }

    }
}

