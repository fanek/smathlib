﻿# StringMathLibrary

A C# library for math operations on strings.

For now it includes only Addition, Subtraction, Multiplication, Division, Gcd, Square, Root, Abs, Compare.

## Usage
- by strings 
```cs
	string result = StringMath.Add(left, right);

	string result = StringMath.Divide(left, right, precision);
```
- or by new StringNumber type
```cs
	StringNumber one = new StringNumber(left);
	StringNumber two = new StringNumber(right);
	StringNumber result = one.Add(two);

	string value = result.ToString();
```
