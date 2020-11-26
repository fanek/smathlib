using System;
using Xunit;

namespace StringMathLibrary.Tests
{
    public class StringNumbers
    {
        [Theory]
        [InlineData("0", "0")]
        [InlineData("-11", "11")]
        [InlineData("20", "20")]
        [InlineData("-2.5", "2.5")]
        public void Abs(string number, string expected)
        {
            StringNumber one = new StringNumber(number);
            StringNumber result = one.Abs();

            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("1", "0", "1")]
        [InlineData("-1", "0", "-1")]
        [InlineData("10", "-15", "-5")]
        [InlineData("-10", "-15", "-25")]
        [InlineData("-15", "10", "-5")]
        [InlineData("11", "99", "110")]
        [InlineData("99", "99", "198")]
        [InlineData("1.25", "27.7", "28.95")]
        [InlineData("1.25", "27.75", "29")]
        [InlineData("1.25", "-27.75", "-26.5")]
        [InlineData("-1.25", "27.75", "26.5")]
        public void Addition(string left, string right, string expected)
        {
            StringNumber one = new StringNumber(left);
            StringNumber two = new StringNumber(right);
            StringNumber result = one.Add(two);

            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("1", "0", "1")]
        [InlineData("-1", "0", "-1")]
        [InlineData("0", "1", "-1")]
        [InlineData("10", "15", "-5")]
        [InlineData("-10", "-15", "5")]
        [InlineData("-15", "-10", "-5")]
        [InlineData("10", "-15", "25")]
        [InlineData("11", "99", "-88")]
        [InlineData("99", "99", "0")]
        [InlineData("1.25", "27.7", "-26.45")]
        [InlineData("1.25", "27.75", "-26.5")]
        [InlineData("1.25", "-27.75", "29")]
        [InlineData("-1.25", "27.75", "-29")]
        public void Subtraction(string left, string right, string expected)
        {
            StringNumber one = new StringNumber(left);
            StringNumber two = new StringNumber(right);
            StringNumber result = one.Subtract(two);

            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("1", "0", "0")]
        [InlineData("0", "1", "0")]
        [InlineData("-1", "0", "0")]
        [InlineData("10", "-15", "-150")]
        [InlineData("11", "99", "1089")]
        [InlineData("9", "9", "81")]
        [InlineData("1.25", "4", "5")]
        public void Multiplication(string left, string right, string expected)
        {
            StringNumber one = new StringNumber(left);
            StringNumber two = new StringNumber(right);
            StringNumber result = one.Multiply(two);

            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("1", "0", "exception")]
        [InlineData("0", "-5", "0")]
        [InlineData("11", "1", "11")]
        [InlineData("11", "-2", "-5.5")]
        [InlineData("-2", "5", "-0.4")]
        [InlineData("15", "10", "1.5")]
        [InlineData("15.642", "3.3", "4.74")]
        public void Division(string left, string right, string expected)
        {
            StringNumber one = new StringNumber(left);
            StringNumber two = new StringNumber(right);
            string result;

            try
            {
                result = one.Divide(two).ToString();
            }
            catch (Exception)
            {
                result = "exception";
            }

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("9", "15", "3")]
        [InlineData("10", "10", "10")]
        [InlineData("12", "24", "12")]
        [InlineData("24", "16", "8")]
        [InlineData("67", "18", "1")]
        [InlineData("100", "88", "4")]
        [InlineData("4729181", "5335543", "2213")]
        [InlineData("2376497", "611683", "1277")]
        [InlineData("1664197", "3057407", "1531")]
        public void Gcd(string left, string right, string expected)
        {
            StringNumber one = new StringNumber(left);
            StringNumber two = new StringNumber(right);
            StringNumber result = one.Gcd(two);

            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("9", "81")]
        [InlineData("-2", "4")]
        [InlineData("1.2", "1.44")]
        [InlineData("0.5", "0.25")]
        [InlineData("111", "12321")]
        public void Square(string number, string expected)
        {
            StringNumber one = new StringNumber(number);
            StringNumber result = one.Square();

            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("81", 2, "9")]
        [InlineData("-81", 2, "-9")]
        [InlineData("112", 4, "10.583")]
        [InlineData("112", 10, "10.5830052442")]
        [InlineData("1522756", 2, "1234")]
        public void Root(string number, int precision, string expected)
        {
            StringNumber one = new StringNumber(number, precision);
            StringNumber result = one.Root();

            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("13", "1", 1)]
        [InlineData("13", "15", -1)]
        [InlineData("13", "13", 0)]
        [InlineData("-13", "-10", -1)]
        [InlineData("-23", "-45", 1)]
        [InlineData("3.456", "5.12", -1)]
        [InlineData("1.123", "1.124", -1)]
        [InlineData("1.33", "1.329", 1)]
        public void Compare(string left, string right, int expected)
        {
            StringNumber one = new StringNumber(left);
            StringNumber two = new StringNumber(right);
            int result = one.Compare(two);

            Assert.Equal(expected, result);
        }
    }
}
