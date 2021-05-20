using System;
using Serilog;

namespace StoreUI
{
    public class ValidationService : IValidationService
    {
        public int ValidateInt(string prompt)
        {
            int numVal = 0;
            bool repeat = true;

            do
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                try
            {
                numVal = Convert.ToInt32(input);
                repeat = false;
            }
            catch (FormatException)
            {
                Log.Information("User input invalid number");
                Console.WriteLine("Please input a valid number");
            }
            catch (OverflowException) {
                Log.Information("User input an invalid number");
                Console.WriteLine("Please input a valid number");
            }
            } while (repeat);
            return numVal;
        }

        public string ValidateString(string prompt)
        {
            string response;
            bool repeat;
            do
            {
                Console.WriteLine(prompt);
                response = Console.ReadLine();
                repeat = String.IsNullOrWhiteSpace(response);
                if (repeat) {
                    Log.Information("User input an empty string");
                    Console.WriteLine("Please input a non empty string");
                }
            } while (repeat);
            return response;
        }

        public double ValidatePrice(string prompt)
        {
            double numVal = 0;
            Console.WriteLine(prompt);

            while (!double.TryParse(Console.ReadLine(), out numVal)) {
                Log.Information("User input an invalid price");
                Console.WriteLine("Please input a valid price $X.XX");
            }
            return numVal;
        }
    }
}