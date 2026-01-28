using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CompanyA.Reporting
{
    public  class Report
    {
        int Id { get; set; } = 2001;
        static double annualIncome { get; set; } = 2000000;
        static double annualSales { get; set; } = 120;

        public void Generate()
        {
            Console.WriteLine($"Report \n Name : Company A \n Id = {Id} \n annualIncome = {annualIncome}" +
                $"\n Annual Sales = {annualSales}");

        }
    }
}
