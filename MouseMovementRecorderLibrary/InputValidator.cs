using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMovementRecorderLibrary
{
    public static class InputValidator
    {
        public static (bool, int) ValidatePositiveInteger(string input)
        {
            string failMessage = "Not a valid input. Enter a positive whole number.";
            
            if (input == null || input.Length == 0 || !int.TryParse(input, out int result) || result <= 0)
            {
                Console.WriteLine(failMessage);
                return (false, -1);
            }
            return (true, result);
        }

        public static (bool, string) ValidateStringContainsAcceptableInput(string input, List<string> correctInputs)
        {
            string failMessage = "Not a valid input. Try again.";
           
            if (correctInputs.Contains(input.Trim().ToUpper()))
            {
                return (true, input.Trim().ToUpper());
            }
            
            else
            {
                Console.WriteLine(failMessage);
                return (false, "");
            }
        }
    }
}
