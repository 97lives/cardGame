using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace cardGame
{
    using System;

    public class InvalidNullInput : Exception
    {
        public InvalidNullInput() : base($"Null given by console")
        {
        }
    }
    public class FrontEnd
    {
        public static void Main()
        {
            BackEnd backend = new BackEnd();

            Console.WriteLine("Enter a list of playing cards (e.g. 2C,5D,KH) separated by ,:");
            try
            {
                string input = Console.ReadLine();

                if (input == null)
                {
                    throw new InvalidNullInput();
                }
                else if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("You have not entered any cards");
                    input = Console.ReadLine();
                }

                int totalScore = backend.CalculateTotalScore(input);
                Console.WriteLine($"Total Score: {totalScore}");
            }
            catch (InvalidNullInput ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidCardRankException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidSuitException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}