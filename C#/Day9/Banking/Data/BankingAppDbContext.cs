using Banking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Data
{
    public class BankingAppDbContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database= efcore;User=sa;Password=123456789aA;TrustServerCertificate=True");
        }


        public DbSet<Customer>  Customers { get; set; }

    }
}
