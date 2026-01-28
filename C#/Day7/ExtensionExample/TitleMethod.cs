using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionExample
{
    public static class TitleMethod
    {
        public static string ToTitleCase(this String str)
        {
            if(string.IsNullOrEmpty(str))
                return str;
            str = str.ToLower();

            List<string> words = str.Split(' ').ToList();
            string lastword = words.Last();

            string res = "";
            foreach (string word in words)
            {
                string lowerString = word.ToLower();

                string correctedString = char.ToUpper(lowerString[0])+lowerString.Substring(1);
                res = res+ correctedString;
                if (word!=lastword) {
                res = res+ " ";
                }
            }
            return res;

        }
    }
}
