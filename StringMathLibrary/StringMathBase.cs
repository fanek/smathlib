using System;
using System.Collections.Generic;
using System.Text;

namespace StringMathLibrary
{
    public class StringMathBase
    {
        protected static string AddPositiveNumbers(string left, string right)
        {
            if (left.Length < right.Length)
            {
                string tmp = left; left = right; right = tmp;
            }
            byte addon = 0;
            char[] result = new char[left.Length + 1];
            for (int i = left.Length - 1; i >= 0; i--)
            {
                int rindex = right.Length - (left.Length - i);
                result[i + 1] = (char)(left[i] + (rindex < 0 ? 0 : right[rindex] - '0'));
                if (addon > 0)
                    result[i + 1] = (char)(result[i + 1] + addon--);
                if (result[i + 1] > '9')
                {
                    addon++;
                    result[i + 1] = (char)(result[i + 1] - 10);
                }
            }
            result[0] = (char)(addon + '0');
            return GetString(result);
        }

        protected static string SubtractPositiveNumbers(string left, string right)
        {
            int comp = Compare(left, right);
            if (comp == -1)
            {
                string tmp = left; left = right; right = tmp;
            }
            byte lend = 0;
            char[] result = new char[left.Length];
            for (int i = left.Length - 1; i >= 0; i--)
            {
                int rindex = right.Length - (left.Length - i);
                result[i] = (char)(left[i] - (rindex < 0 ? 0 : right[rindex] - '0'));
                if (lend > 0)
                    result[i] = (char)(result[i] - lend--);
                if (result[i] < '0')
                {
                    lend++;
                    result[i] = (char)(result[i] + 10);
                }
            }
            return ((comp == -1) ? "-" : "") + GetString(result);
        }

        protected static string MultiplyPositiveNumbers(string left, string right)
        {
            string result = "";

            for (int rIndex = right.Length - 1; rIndex >= 0; rIndex--)
            {
                if (right[rIndex] == '0') continue;
                string value = new string('0', right.Length - rIndex - 1);
                int addon = 0, rDigit = right[rIndex] - '0';
                for (int lIndex = left.Length - 1; lIndex >= 0; lIndex--)
                {
                    int lDigit = left[lIndex] - '0';
                    int tmp = lDigit * rDigit + addon;
                    value = (char)((tmp % 10) + '0') + value;
                    addon = tmp / 10;
                }
                if (addon > 0)
                    value = (char)(addon + '0') + value;
                result = (result == "") ? value : AddPositiveNumbers(result, value);
            }
            return (result == "")? "0" : result;
        }

        protected static (string value, string rest) DividePositiveNumbers(string left, string right)
        {
            if (left == "0")
                return ("0", "0");
            else if (right == "0")
                //return ("Cannot divide by zero", "0");
                throw new DivideByZeroException("Cannot divide by zero");

            int comp = Compare(left, right);
            if (comp == 0)
                return ("1", "0");
            else if (comp < 0)
                return ("0", left);

            string multiplier = "0";
            do
            {
                int numberZeros = AddZeros(left, right, out string rightWithZeros);
                left = SubtractPositiveNumbers(left, rightWithZeros);
                comp = Compare(left, right);
                multiplier = AddPositiveNumbers(multiplier, AddTrailingZeros(numberZeros));
            }
            while (comp > 0);
            if (comp == 0)
                return (AddPositiveNumbers(multiplier, "1"), "0");
            return (multiplier, left);
        }

        protected static string DividePositiveNumbersPrecisely(string left, string right, int precision = 10)
        {
            var result = DividePositiveNumbers(left, right);
            string resultDouble = result.value;

            if (result.rest != "0")
            {
                resultDouble += "."; 
                do
                {
                    result = DividePositiveNumbers(result.rest + "0", right);
                    resultDouble += result.value;
                }
                while (result.rest != "0" && --precision > 0);
            }
            
            return resultDouble;
        }

        protected static (string, bool) RemoveNegativeSign(string value)
        {
            bool negative = false;
            if (value[0] == '-')
            {
                value = value.Substring(1);
                negative = true;
            }
            return (value, negative);
        }

        protected static (string, int) RemoveDecimalPoint(string value)
        {
            int index = value.IndexOf('.'); int decimalDigits = 0;
            if (index > 0)
            {
                decimalDigits = value.Length - index - 1;
                value = value.Remove(index, 1);
            }
            return (value, decimalDigits);
        }

        protected static (string, bool, int) RemoveSignAndPoint(string value)
        {
            bool negative; int decimalDigits;
            (value, negative) = RemoveNegativeSign(value);
            (value, decimalDigits) = RemoveDecimalPoint(value);
            return (value, negative, decimalDigits);
        }

        protected static (string, string) AlignDecimals(string left, int leftDecimalDigits, string right, int rightDecimalDigits)
        {
            if (leftDecimalDigits > rightDecimalDigits)
                right = AddTrailingZeros(leftDecimalDigits - rightDecimalDigits, right);
            else if (leftDecimalDigits < rightDecimalDigits)
                left = AddTrailingZeros(rightDecimalDigits - leftDecimalDigits, left);

            return (left, right);
        }

        protected static string InsertDecimalPoint(string value, int decimalDigits)
        {
            if (decimalDigits > 0)
            {
                if (value.Length <= decimalDigits)
                    value = "0." + new string('0', decimalDigits - value.Length) + value;
                else
                    value = value.Insert(value.Length - decimalDigits, ".");
                value = RemoveTrailingZeros(value);
            }
            return value;
        }

        protected static string RemoveTrailingZeros(string value)
        {
            int index = value.Length - 1;
            while(index >= 0 && value[index] == '0')
            {
                index--;
                if (value[index] == '.')
                {
                    index--; break;
                }
            }
            int totalZeros = value.Length - index - 1;

            return (totalZeros > 0)? value.Remove(value.Length - totalZeros, totalZeros) : value;
        }

        protected static int MaxOfTwo(int left, int right)
        {
            return (left >= right) ? left : right;
        }

        // This function is used for division by subtraction to increase its speed. 
        // When right operand has fewer digits than left, the function adds zeros to the right operand until it can become bigger than the left.
        // Example, division 200 by 2 using subtraction will require 100 operations. With additional zeros it will be a division 200 by 200 = 1 subtraction.
        protected static int AddZeros(string left, string right, out string rightWithZeros)
        {
            int tens = 0;
            rightWithZeros = right;
            if (left.Length > right.Length)
                while (true)
                {
                    string tmp = rightWithZeros + '0';
                    if (Compare(left, tmp) <= 0)
                        return tens;
                    tens++;
                    rightWithZeros = tmp;
                }
            else
                return 0;
        }

        // Adds desired number of zeros at the end of the string
        protected static string AddTrailingZeros(int tens, string value = "1")
        {
            return value + new string('0', tens);
        }

        protected static string GetString(char[] array)
        {
            StringBuilder result = new StringBuilder();
            bool firstNonZero = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '0' && !firstNonZero) continue;
                result.Append(array, i, array.Length - i);
                break;
            }
            return result.Length == 0 ? "0" : result.ToString();
        }

        protected static int Compare(string x, string y)
        {
            if (x.Length == y.Length)
                return CompareEqualLength(x, y);
            else
                return (x.Length > y.Length) ? 1 : -1;
        }

        protected static int CompareEqualLength(string x, string y)
        {
            // only for the same sized strings
            for (int i = 0; i < x.Length; i++)
                if (x[i] > y[i])
                    return 1;
                else if (x[i] < y[i])
                    return -1;
            return 0;
        }
    }
}
