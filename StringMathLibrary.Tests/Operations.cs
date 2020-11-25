using System;
using Xunit;

namespace StringMathLibrary.Tests
{
    public class Operations
    {
        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("1", "0", "1")]
        [InlineData("-1", "0", "-1")]
        [InlineData("10", "-15", "-5")]
        [InlineData("11", "99", "110")]
        [InlineData("99", "99", "198")]
        [InlineData("1.25", "27.7", "28.95")]
        [InlineData("1.25", "27.75", "29")]
        [InlineData("1.25", "-27.75", "-26.5")]
        [InlineData("-1.25", "27.75", "26.5")]
        public void Addition(string left, string right, string expected)
        {
            string result = StringMath.Add(left, right);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("1", "0", "1")]
        [InlineData("-1", "0", "-1")]
        [InlineData("0", "1", "-1")]
        [InlineData("10", "15", "-5")]
        [InlineData("10", "-15", "25")]
        [InlineData("11", "99", "-88")]
        [InlineData("99", "99", "0")]
        [InlineData("1.25", "27.7", "-26.45")]
        [InlineData("1.25", "27.75", "-26.5")]
        [InlineData("1.25", "-27.75", "29")]
        [InlineData("-1.25", "27.75", "-29")]
        public void Subtraction(string left, string right, string expected)
        {
            string result = StringMath.Subtract(left, right);

            Assert.Equal(expected, result);
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
            string result = StringMath.Multiply(left, right);

            Assert.Equal(expected, result);
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
            string result;
            try
            {
                result = StringMath.Divide(left, right);
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
            string result = StringMath.Gcd(left, right);

            Assert.Equal(expected, result);
        }
    }
}
