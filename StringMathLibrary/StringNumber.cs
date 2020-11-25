using System;
using System.Collections.Generic;
using System.Text;

namespace StringMathLibrary
{
    public class StringNumber : StringMathBase
    {
        private string number = "0";
        private bool isNegative = false;
        private bool isPositive = true;

        public bool IsNegative
        {
            get { return isNegative; }
        }

        public bool IsPositive
        {
            get { return isPositive; }
        }

        public StringNumber(string value)
        {
            SetFields(value);
        }

        private StringNumber SetFields(string value)
        {
            if (value.Length > 0)
            {
                number = value;
                isNegative = (value[0] == '-');
                isPositive = !isNegative;
            }
            return this;
        }

        public StringNumber Abs()
        {
            if (IsPositive)
                return this;
            else return SetFields(number.Substring(1));
        }

        public StringNumber Add(StringNumber value)
        {
            string result = StringMath.Add(number, value.ToString());
            
            return SetFields(result);
        }

        public StringNumber Subtract(StringNumber value)
        {
            string result = StringMath.Subtract(number, value.ToString());

            return SetFields(result);
        }

        public StringNumber Multiply(StringNumber value)
        {
            string result = StringMath.Multiply(number, value.ToString());

            return SetFields(result);
        }

        public StringNumber Divide(StringNumber value)
        {
            string result = StringMath.Divide(number, value.ToString());

            return SetFields(result);
        }

        public StringNumber Gcd(StringNumber value)
        {
            string result = StringMath.Gcd(number, value.ToString());

            return new StringNumber(result);
        }

        public override string ToString()
        {
            return number;
        }
    }
}
