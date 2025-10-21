using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverte
{
    internal class TemperatureValidator
    {
        public Boolean Validator(double celsius)
        {
            return celsius >= -273.15 && celsius <= 5500;
        }
    }
}
