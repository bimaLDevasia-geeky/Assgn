using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyB.Analytics
{
    public class Report
    {
        int Id { get; set; } = 1001;
        static double annualIncome { get; set; } = 4000000;
        static double annualSales { get; set; } = 320;
        public void Generate()
        {
            Console.WriteLine($"Report \n Name : Company B \n Id = {Id} \n annualIncome = {annualIncome}" +
                $"\n Annual Sales = {annualSales}");

        }
    }
}
