namespace csharp_learning.Modules
{
    internal static class TemperatureChange
    {
        public static void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("=== Choose what to convert ===");
                Console.WriteLine("1. Celsius to Fahrenheit");
                Console.WriteLine("2. Fahrenheit to Celsius");
                Console.WriteLine("0. Go back to programs overview");

                Console.Write("Your choice: ");
                string? userChoice = Console.ReadLine();
                Console.WriteLine();

                if (userChoice == "1")
                {
                    while (true)
                    {
                        Console.Write("Celsius: ");

                        if (double.TryParse(Console.ReadLine(), out double celsiusToConvert))
                        {
                            ConvertCelsiusToFahrenheit(celsiusToConvert);
                            break;
                        }
                        else
                        {
                            Console.Write($"Invalid input, type a number \n");
                        }
                    }
                }
                else if (userChoice == "2")
                {
                    while (true)
                    {
                        Console.Write("Fahrenheit: ");

                        if (double.TryParse(Console.ReadLine(), out double fahrenheitToConvert))
                        {
                            ConvertFahrenheitToCelsius(fahrenheitToConvert);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Invalid input, type a number \n");
                        }
                    }
                }
                else if (userChoice == "0")
                {
                    Console.WriteLine($"Going back to overview \n");
                    isRunning = false;
                    break;
                }
            }
        }

        private static void ConvertCelsiusToFahrenheit(double celsius)
        {
            double fahrenheit = (celsius * 1.8) + 32;
            Console.WriteLine($"{celsius} is {fahrenheit:F1} °F \n");
        }

        private static void ConvertFahrenheitToCelsius(double fahrenheit)
        {
            double celsius = (fahrenheit - 32) / 1.8;
            Console.WriteLine($"{fahrenheit} is {celsius:F1} °C \n");
        }
    }
}
