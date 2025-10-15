using System;
using System.Collections.Generic;

namespace Assign2
{
    public class Fibonacci
    {
   
        private static Dictionary<int, long> memo = new Dictionary<int, long>();

 
        public  long FibMemoized(int n)
        {
    
            
            if (n == 0) return 0;
            if (n == 1) return 1;


            if (memo.ContainsKey(n))
            {
                return memo[n];
            }

            long result = FibMemoized(n - 1) + FibMemoized(n - 2);

 
            memo.Add(n, result);

            return result;
        }

        public  void PrintFibonacci(int n)
        {
            for (int i = 0; i < n; i++) {
                Console.Write($"{FibMemoized(i)} ");
            }
        }

       

        
    }
}
