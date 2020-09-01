using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace CurrencyConverterService
{   
    public class CurrencyConverter : ICurrencyConverter
    {
        /// <summary>
        /// This method contains logic to manage the inputs and collate the output
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetResult(string number)
        {            
            string result;
            
            if (isInputFormatCorrect(number))
            {
                ParseInput(number, out long dollars, out int cents);                
                result = IsInputOutOfRange(dollars, cents) ? "Input is Out of Range" : GetResult(dollars, cents);
                return result;
            }
            else
            {
                result = "Input in not is correct format";
                return result;
            }
        }

        /// <summary>
        /// Splitting the received input to dollars and cents.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="dollars"></param>
        /// <param name="cents"></param>
        private static void ParseInput(string number, out long dollars, out int cents)
        {
            string[] inputNumber;            
            if (number.Contains(','))
            {
                inputNumber = number.Split(',');
                dollars = long.Parse(inputNumber[0]);
                cents = (inputNumber[1].Length == 1) ? int.Parse(inputNumber[1]) * 10 : int.Parse(inputNumber[1]);
            }
            else
            {
                dollars = long.Parse(number);
                cents = 0;
            }
        }

        /// <summary>
        /// This method manages dollars and cents
        /// </summary>
        /// <param name="dollars"></param>
        /// <param name="cents"></param>
        /// <returns></returns>
        private string GetResult(long dollars, int cents)
        {
            string result = ConvertNumberToWords(dollars);
            if (result == "one")
                result += " dollar";
            else
                result += " dollars";

            if (cents > 0)
                result += " and " + ConvertNumberToWords(cents) + " cents";

            return result;            
        }

        /// <summary>
        /// This method contains logic to convert number to words
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string ConvertNumberToWords(long number)
        {
            if (number == 0) return "zero";
            
            StringBuilder words = new StringBuilder();

            if (number / 1000000 > 0)
            {                
                words.Append(ConvertNumberToWords(number / 1000000) + " million ");
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words.Append(ConvertNumberToWords(number / 1000) + " thousand ");
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words.Append(ConvertNumberToWords(number / 100) + " hundred ");
                number %= 100;
            }

            if (number > 0)
            {               
                string[] unitsCollection = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", };
                string[] tensCollection = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                {
                    words.Append(unitsCollection[number]);
                }
                else
                {
                    words.Append(tensCollection[number / 10]);
                    if ((number % 10) > 0)
                    {
                        words.Append("-" + unitsCollection[number % 10]);
                    }
                }
            }

            return words.ToString();
        }

        /// <summary>
        /// This method checks if the user input is out of range
        /// </summary>
        /// <param name="dollars"></param>
        /// <param name="cents"></param>
        /// <returns></returns>
        private bool IsInputOutOfRange(long dollars, int cents)
        {
            const long minDollarRange = 0;
            const long maxDollarRange = 999999999;            
            const long maxCentsRange = 99;

            if ((dollars >= minDollarRange && dollars <= maxDollarRange) && (cents <= maxCentsRange))
                return false;
            else
                return true;
        }

        /// <summary>
        /// This method checks if the input string contains invalid characters inside them
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool isInputFormatCorrect(string number)
        {
            var regexItem = new Regex("^[0-9,]*$");

            if (regexItem.IsMatch(number))
                return true;
            else
                return false;
        }
    }
}
