using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        public int Add(int a ,int b)
        {
            return a + b;
        }
        public int Sub(int a, int b)
        {
            return a - b;
        }
        public int Mult(int a, int b)
        {
            return a * b;
        }
        public int Divide(int a, int b)
        {
            try
            {
                return a / b;
            }
            catch(DivideByZeroException E)
            {
                throw new DivideByZeroException();
            }
        }

    }
}
