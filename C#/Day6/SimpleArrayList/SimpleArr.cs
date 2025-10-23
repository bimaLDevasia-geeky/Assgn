using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleArrayList
{
    public class SimpleArr
    {
        static ArrayList list = new ArrayList();

        public void AddItems(string name)
        {
            list.Add(name);
        }

        public void Display()
        {
            Console.Write("With foreach -> ");
            foreach (var item in list)
            {
                Console.Write(item+ "\t");
            }

            Console.WriteLine();

            Console.Write("With for ->\t");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i]+ "\t");
            }
        }

        public void RemoveItem(string name)
        {
            if (list.Contains(name))
            list.Remove(name);
            else
                Console.WriteLine($"List doesn't contains {name}");
        }

        public void InsertAtPosition(string name,int pos)
        {
            if (pos < 0 || pos > list.Count)
            {
                Console.WriteLine("Invalid pos");
                return;
            }
            list.Insert(pos-1,name);
        }
    }
}
