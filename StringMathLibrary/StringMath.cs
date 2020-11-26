using System;
using System.Text;

namespace StringMathLibrary
{
    public class StringMath : StringMathBase
    {
        /// <summary>
        /// Returns positive value of a number.
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        public static string Abs(string one)
        {
            return RemoveNegativeSign(one).Item1;
        }

        /// <summary>
        /// Addition of 2 numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static string Add(string one, string two)
        {
            bool resultNegative = false;
            var (first, firstNegative, firstDecimalDigits) = RemoveSignAndPoint(one);
            var (second, secondNegative, secondDecimalDigits) = RemoveSignAndPoint(two);

            (first, second) = AlignDecimals(first, firstDecimalDigits, second, secondDecimalDigits);
            int maxDecimalDigits = MaxOfTwo(firstDecimalDigits, secondDecimalDigits);

            string result;
            if (!firstNegative && !secondNegative)
                result = AddPositiveNumbers(first, second);
            else if (!firstNegative && secondNegative)
                (result, resultNegative) = RemoveNegativeSign(SubtractPositiveNumbers(first, second));
            else if (firstNegative && !secondNegative)
                (result, resultNegative) = RemoveNegativeSign(SubtractPositiveNumbers(second, first));
            else
                (result, resultNegative) = (AddPositiveNumbers(first, second), true);

            result = InsertDecimalPoint(result, maxDecimalDigits);
            return resultNegative ? "-" + result : result;
        }

        /// <summary>
        /// Subtraction of 2 numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static string Subtract(string one, string two)
        {
            bool resultNegative = false;
            var (first, firstNegative, firstDecimalDigits) = RemoveSignAndPoint(one);
            var (second, secondNegative, secondDecimalDigits) = RemoveSignAndPoint(two);

            (first, second) = AlignDecimals(first, firstDecimalDigits, second, secondDecimalDigits);
            int maxDecimalDigits = MaxOfTwo(firstDecimalDigits, secondDecimalDigits);

            string result;
            if (!firstNegative && !secondNegative)
                (result, resultNegative) = RemoveNegativeSign(SubtractPositiveNumbers(first, second));
            else if (!firstNegative && secondNegative)
                result = AddPositiveNumbers(first, second);
            else if (firstNegative && !secondNegative)
                (result, resultNegative) = (AddPositiveNumbers(first, second), true);
            else
                (result, resultNegative) = RemoveNegativeSign(SubtractPositiveNumbers(second, first));

            result = InsertDecimalPoint(result, maxDecimalDigits);
            return resultNegative ? "-" + result : result;
        }

        /// <summary>
        /// Multiplication of 2 numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static string Multiply(string one, string two)
        {
            var (first, firstNegative, firstDecimalDigits) = RemoveSignAndPoint(one);
            var (second, secondNegative, secondDecimalDigits) = RemoveSignAndPoint(two);
            bool resultNegative = firstNegative ^ secondNegative;

            string result = MultiplyPositiveNumbers(first, second);
            result = InsertDecimalPoint(result, firstDecimalDigits + secondDecimalDigits);

            return (resultNegative && result != "0") ? "-" + result : result;
        }

        /// <summary>
        /// Division of 2 numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="precision">Maximal number of digits after point.</param>
        /// <returns></returns>
        public static string Divide(string one, string two, int precision = 10)
        {
            var (first, firstNegative, firstDecimalDigits) = RemoveSignAndPoint(one);
            var (second, secondNegative, secondDecimalDigits) = RemoveSignAndPoint(two);
            bool resultNegative = firstNegative ^ secondNegative;

            (first, second) = AlignDecimals(first, firstDecimalDigits, second, secondDecimalDigits);

            string resultDouble = DividePositiveNumbersPrecisely(first, second, precision);

            return (resultNegative && resultDouble != "0")? "-" + resultDouble : resultDouble;
        }

        /// <summary>
        /// Finds the greatest common divisor of two natural numbers - the largest number that divides both numbers without reminder.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static string Gcd(string one, string two)
        {
            var (first, _) = RemoveNegativeSign(one);
            var (second, _) = RemoveNegativeSign(two);
            do
            {
                var result = DividePositiveNumbers(first, second);
                first = second;
                second = result.rest;
            }
            while (second != "0");
            return first;
        }

        /// <summary>
        /// Square of a number. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Square(string number)
        {
            var (one, _, decimalDigits) = RemoveSignAndPoint(number);

            string result = MultiplyPositiveNumbers(one, one);

            return InsertDecimalPoint(result, decimalDigits + decimalDigits);
        }

        /// <summary>
        /// Square root of a number. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="precision">Maximal number of digits after point.</param>
        /// <returns></returns>
        public static string Root(string number, int precision = 10)
        {
            int maxSteps = 30; bool negative;
            (number, negative) = RemoveNegativeSign(number);
            string guess = Divide(number, "2", precision);

            while(maxSteps-- > 0)
            {
                string newguess = Divide(number, guess, precision);
                newguess = Add(guess, newguess);
                newguess = Divide(newguess, "2", precision);
                int comp = CompareDecimals(newguess, guess);
                if (comp >= 0) break;
                guess = newguess;
            }
            return negative? "-" + guess : guess;
        }

        /// <summary>
        /// Compares two numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static int Compare(string one, string two)
        {
            return CompareDecimals(one, two);
        }
    }
}
