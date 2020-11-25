using System;
using System.Text;

namespace StringMathLibrary
{
    public class StringMath : StringMathBase
    {
        /// <summary>
        /// Addition of 2 numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static string Add(string left, string right)
        {
            bool leftNegative, rightNegative, resultNegative = false;
            int leftDecimalDigits, rightDecimalDigits;

            (left, leftNegative, leftDecimalDigits) = RemoveSignAndPoint(left);
            (right, rightNegative, rightDecimalDigits) = RemoveSignAndPoint(right);

            (left, right) = AlignDecimals(left, leftDecimalDigits, right, rightDecimalDigits);
            int maxDecimalDigits = MaxOfTwo(leftDecimalDigits, rightDecimalDigits);

            string result;
            if (!leftNegative && !rightNegative)
                result = AddPositiveNumbers(left, right);
            else if (!leftNegative && rightNegative)
                (result, resultNegative) = RemoveNegativeSign(SubtractPositiveNumbers(left, right));
            else if (leftNegative && !rightNegative)
                (result, resultNegative) = RemoveNegativeSign(SubtractPositiveNumbers(right, left));
            else
            {
                result = AddPositiveNumbers(left, right);
                resultNegative = true;
            }

            result = InsertDecimalPoint(result, maxDecimalDigits);
            return resultNegative ? "-" + result : result;
        }

        /// <summary>
        /// Subtraction of 2 numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static string Subtract(string left, string right)
        {
            bool leftNegative, rightNegative, resultNegative = false;
            int leftDecimalDigits, rightDecimalDigits;

            (left, leftNegative, leftDecimalDigits) = RemoveSignAndPoint(left);
            (right, rightNegative, rightDecimalDigits) = RemoveSignAndPoint(right);

            (left, right) = AlignDecimals(left, leftDecimalDigits, right, rightDecimalDigits);
            int maxDecimalDigits = MaxOfTwo(leftDecimalDigits, rightDecimalDigits);

            string result;
            if (!leftNegative && !rightNegative)
                (result, resultNegative) = RemoveNegativeSign(SubtractPositiveNumbers(left, right));
            else if (!leftNegative && rightNegative)
                result = AddPositiveNumbers(left, right);
            else if (leftNegative && !rightNegative)
            {
                result = AddPositiveNumbers(left, right);
                resultNegative = true;
            }
            else
                (result, resultNegative) = RemoveNegativeSign(SubtractPositiveNumbers(right, left));

            result = InsertDecimalPoint(result, maxDecimalDigits);
            return resultNegative ? "-" + result : result;
        }

        /// <summary>
        /// Multiplication of 2 numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static string Multiply(string left, string right)
        {
            bool leftNegative, rightNegative, resultNegative;
            int leftDecimalDigits, rightDecimalDigits;

            (left, leftNegative, leftDecimalDigits) = RemoveSignAndPoint(left);
            (right, rightNegative, rightDecimalDigits) = RemoveSignAndPoint(right);
            resultNegative = leftNegative ^ rightNegative;

            string result = MultiplyPositiveNumbers(left, right);
            result = InsertDecimalPoint(result, leftDecimalDigits + rightDecimalDigits);

            return (resultNegative && result != "0") ? "-" + result : result;
        }

        /// <summary>
        /// Division of 2 numbers in strings. Supports negative and decimal numbers. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="precision">Maximal number of digits after point.</param>
        /// <returns></returns>
        public static string Divide(string left, string right, int precision = 10)
        {
            bool leftNegative, rightNegative, resultNegative;
            int leftDecimalDigits, rightDecimalDigits;

            (left, leftNegative, leftDecimalDigits) = RemoveSignAndPoint(left);
            (right, rightNegative, rightDecimalDigits) = RemoveSignAndPoint(right);
            resultNegative = leftNegative ^ rightNegative;

            (left, right) = AlignDecimals(left, leftDecimalDigits, right, rightDecimalDigits);

            string resultDouble = DividePositiveNumbersPrecisely(left, right, precision);

            return (resultNegative && resultDouble != "0")? "-" + resultDouble : resultDouble;
        }

        /// <summary>
        /// Finds the greatest common divisor of two natural numbers - the largest number that divides both numbers without reminder.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static string Gcd(string left, string right)
        {
            (left, _) = RemoveNegativeSign(left);
            (right, _) = RemoveNegativeSign(right);
            do
            {
                var result = DividePositiveNumbers(left, right);
                left = right;
                right = result.rest;
            }
            while (right != "0");
            return left;
        }
    }
}
