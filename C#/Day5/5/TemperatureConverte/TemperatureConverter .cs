namespace TemperatureConverte
{
    public class TemperatureConverter
    {
        TemperatureValidator _validator = new TemperatureValidator();
        public double CelsiusToFahrenheit(double celsius)
        {
            if(!_validator.Validator(celsius)) {
                throw new ArgumentException("Celsius out of Range", nameof(celsius));
                
            }
            double Fahrenheit = (celsius * 9 / 5) + 32;
            return Fahrenheit;
        }

        public double FahrenheitToCelsius(double fahrenheit)
        {
            double celsius = (fahrenheit - 32) * 5 / 9;
            if (!_validator.Validator(celsius))
            {
                throw new ArgumentException("Celsius Out of Range",nameof(celsius));
            }
            return celsius;
        }
    }
}
