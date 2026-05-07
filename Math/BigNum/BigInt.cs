using System;
using System.Collections.Generic;
using System.Numerics;
using Laserbean.CustomUnityEvents;
using Laserbean.EventManager;
using Laserbean.General;
using Laserbean.Removable;
using UnityEditor;
using UnityEngine;

// // // [System.Serializable]
// // // public class BigNumMetric : BigNum
// // // {
// // //     public BigNumMetric(double coeff, int exp) : base(coeff, exp)
// // //     {
// // //     }
// // //     public new static BigNumMetric Parse(string value)
// // //     {
// // //         if (string.IsNullOrWhiteSpace(value))
// // //             throw new FormatException("Input string was not in a correct format.");

// // //         value = value.Trim();
// // //         var uppercase = value.ToUpperInvariant();
// // //         var suffixes = new Dictionary<string, int>
// // //         {
// // //             { "P", 15 },
// // //             { "T", 12 },
// // //             { "G", 9 },
// // //             { "M", 6 },
// // //             { "K", 3 }
// // //         };

// // //         foreach (var suffix in suffixes)
// // //         {
// // //             if (uppercase.EndsWith(suffix.Key, StringComparison.Ordinal))
// // //             {
// // //                 var numberPart = uppercase.Substring(0, uppercase.Length - suffix.Key.Length).Trim();
// // //                 if (string.IsNullOrEmpty(numberPart))
// // //                     throw new FormatException("Input string was not in a correct format.");

// // //                 double coeff = double.Parse(numberPart);
// // //                 return new BigNumMetric(coeff, suffix.Value);
// // //             }
// // //         }

// // //         // Fallback to the base BigIntMetric parser for scientific/exact values.
// // //         var baseValue = BigNum.Parse(value);
// // //         return new BigNumMetric(baseValue.coefficient, baseValue.exponent);
// // //     }

// // //     public static bool TryParse(string value, out BigNumMetric result)
// // //     {
// // //         result = null;
// // //         try
// // //         {
// // //             result = Parse(value);
// // //             return true;
// // //         }
// // //         catch
// // //         {
// // //             return false;
// // //         }
// // //     }
// // //     public override string ToString()
// // //     {
// // //         return ToString(1);
// // //     }

// // //     public string ToString(int decimalPlaces = 1)
// // //     {
// // //         double value = ToDouble();

// // //         if (value == 0) return "0";

// // //         double absValue = Math.Abs(value);
// // //         string[] metricPrefixes = { "", "K", "M", "G", "T", "P" };
// // //         int prefixIndex = 0;

// // //         while (absValue >= 1000 && prefixIndex < metricPrefixes.Length - 1)
// // //         {
// // //             absValue /= 1000;
// // //             prefixIndex++;
// // //         }

// // //         double displayValue = value / Math.Pow(1000, prefixIndex);
// // //         string format;

// // //         if (prefixIndex == 0)
// // //         {
// // //             format = "F0";
// // //         }
// // //         else
// // //         {
// // //             format = decimalPlaces == 0 ? "F0" : $"F{decimalPlaces}";
// // //         }

// // //         return displayValue.ToString(format) + metricPrefixes[prefixIndex];
// // //     }

// // // }

[System.Serializable]
public class BigNumMetric : System.IComparable<BigNumMetric>, System.IEquatable<BigNumMetric>, ISerializationCallbackReceiver
{
    [Header("Scientific")]
    public double coefficient;
    public int exponent;
    [Header("Number")]
    [ShowOnly]
    public double Display;
    public string InputText = "";
    public bool TryParseInput = false;

    public const long MaxValue = long.MaxValue;
    public const long MinValue = long.MinValue;

    public BigNumMetric(BigNumMetric num)
    {
        coefficient = num.coefficient; 
        exponent = num.exponent; 
    }
    
    public BigNumMetric(double coeff, int exp)
    {
        Normalize(coeff, exp);
    }

    public BigNumMetric(long value)
    {
        if (value == 0)
        {
            coefficient = 0;
            exponent = 0;
        }
        else
        {
            coefficient = value;
            exponent = 0;
            Normalize();
        }
    }

    public BigNumMetric(int value) : this((long)value) { }

    public BigNumMetric(double value)
    {
        if (value == 0)
        {
            coefficient = 0;
            exponent = 0;
        }
        else
        {
            coefficient = value;
            exponent = 0;
            Normalize();
        }
    }

    public void OnBeforeSerialize()
    {
        // throw new NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        if (TryParseInput)
        {
            if (TryParse(InputText, out BigNumMetric result))
            {
                // InputText = "";
                coefficient = result.coefficient;
                exponent = result.exponent;
            }
            TryParseInput = false;
        }
        Display = ToDouble();

        // throw new NotImplementedException();
    }


    private void Normalize(double coeff, int exp)
    {
        if (coeff == 0)
        {
            coefficient = 0;
            exponent = 0;
            return;
        }

        coefficient = coeff;
        exponent = exp;
        Normalize();
    }

    private void Normalize()
    {
        if (coefficient == 0)
        {
            exponent = 0;
            return;
        }

        while (Math.Abs(coefficient) >= 10)
        {
            coefficient /= 10;
            exponent++;
        }

        while (Math.Abs(coefficient) < 1 && Math.Abs(coefficient) > 0)
        {
            coefficient *= 10;
            exponent--;
        }
    }

    public double ToDouble()
    {
        return coefficient * Math.Pow(10, exponent);
    }

    public BigNumMetric TruncateToExponent()
    {
        if (coefficient == 0)
        {
            return new BigNumMetric(0);
        }

        if (exponent >= 0)
        {
            return new BigNumMetric(Math.Truncate(coefficient), exponent);
        }

        // If exponent is negative, any value is already smaller than 1 at the current exponent scale.
        // Convert to an integer value without fractional remainder.
        return new BigNumMetric((long)Math.Truncate(ToDouble()));
    }

    public int ToInt()
    {
        var truncated = TruncateToExponent();
        var value = truncated.ToDouble();
        if (value >= int.MaxValue) return int.MaxValue;
        if (value <= int.MinValue) return int.MinValue;
        return (int)value;
    }

    // public int RoundToInt(MidpointRounding rounding = MidpointRounding.AwayFromZero)
    // {
    //     return (int)Math.Round(ToDouble(), rounding);
    // }

    // Arithmetic Operators
    public static BigNumMetric operator +(BigNumMetric a, BigNumMetric b)
    {
        if (a == null) a = new BigNumMetric(0);
        if (b == null) b = new BigNumMetric(0);

        if (a.exponent == b.exponent)
            return new BigNumMetric(a.coefficient + b.coefficient, a.exponent);

        if (a.exponent > b.exponent)
            return new BigNumMetric(a.coefficient + b.coefficient * Math.Pow(10, b.exponent - a.exponent), a.exponent);

        return new BigNumMetric(a.coefficient * Math.Pow(10, a.exponent - b.exponent) + b.coefficient, b.exponent);
    }

    public static BigNumMetric operator -(BigNumMetric a, BigNumMetric b)
    {
        if (a == null) a = new BigNumMetric(0);
        if (b == null) b = new BigNumMetric(0);

        if (a.exponent == b.exponent)
            return new BigNumMetric(a.coefficient - b.coefficient, a.exponent);

        if (a.exponent > b.exponent)
            return new BigNumMetric(a.coefficient - b.coefficient * Math.Pow(10, b.exponent - a.exponent), a.exponent);

        return new BigNumMetric(a.coefficient * Math.Pow(10, a.exponent - b.exponent) - b.coefficient, b.exponent);
    }

    public static BigNumMetric operator *(BigNumMetric a, BigNumMetric b)
    {
        if (a == null || b == null) return new BigNumMetric(0);
        return new BigNumMetric(a.coefficient * b.coefficient, a.exponent + b.exponent);
    }

    public static BigNumMetric operator /(BigNumMetric a, BigNumMetric b)
    {
        if (a == null) a = new BigNumMetric(0);
        if (b == null || b.coefficient == 0) throw new System.DivideByZeroException();
        return new BigNumMetric(a.coefficient / b.coefficient, a.exponent - b.exponent);
    }

    public static BigNumMetric operator %(BigNumMetric a, BigNumMetric b)
    {
        if (b == null || b.coefficient == 0) throw new System.DivideByZeroException();
        return new BigNumMetric(a.ToDouble() % b.ToDouble(), 0);
    }

    // Unary Operators
    public static BigNumMetric operator +(BigNumMetric a)
    {
        return a ?? new BigNumMetric(0);
    }

    public static BigNumMetric operator -(BigNumMetric a)
    {
        if (a == null) return new BigNumMetric(0);
        return new BigNumMetric(-a.coefficient, a.exponent);
    }

    public static BigNumMetric operator ++(BigNumMetric a)
    {
        if (a == null) a = new BigNumMetric(0);
        return a + new BigNumMetric(1);
    }

    public static BigNumMetric operator --(BigNumMetric a)
    {
        if (a == null) a = new BigNumMetric(0);
        return a - new BigNumMetric(1);
    }

    // Comparison Operators
    public static bool operator ==(BigNumMetric a, BigNumMetric b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return Math.Abs(a.ToDouble() - b.ToDouble()) < double.Epsilon;
    }

    public static bool operator !=(BigNumMetric a, BigNumMetric b) => !(a == b);

    public static bool operator <(BigNumMetric a, BigNumMetric b)
    {
        if (a == null) a = new BigNumMetric(0);
        if (b == null) b = new BigNumMetric(0);
        return a.ToDouble() < b.ToDouble();
    }

    public static bool operator >(BigNumMetric a, BigNumMetric b)
    {
        if (a == null) a = new BigNumMetric(0);
        if (b == null) b = new BigNumMetric(0);
        return a.ToDouble() > b.ToDouble();
    }

    public static bool operator <=(BigNumMetric a, BigNumMetric b) => a < b || a == b;

    public static bool operator >=(BigNumMetric a, BigNumMetric b) => a > b || a == b;

    // Conversion Operators
    public static implicit operator BigNumMetric(int value) => new BigNumMetric(value);
    public static implicit operator BigNumMetric(long value) => new BigNumMetric(value);
    public static implicit operator BigNumMetric(double value) => new BigNumMetric(value);

    public static explicit operator int(BigNumMetric value)
    {
        if (value == null) return 0;
        return (int)value.ToDouble();
    }

    public static explicit operator long(BigNumMetric value)
    {
        if (value == null) return 0;
        return (long)value.ToDouble();
    }

    public static explicit operator double(BigNumMetric value)
    {
        if (value == null) return 0;
        return value.ToDouble();
    }

    // Comparison Interface
    public int CompareTo(BigNumMetric other)
    {
        if (other == null) return 1;
        var diff = this.ToDouble() - other.ToDouble();
        if (diff < 0) return -1;
        if (diff > 0) return 1;
        return 0;
    }

    public bool Equals(BigNumMetric other) => this == other;

    public override bool Equals(object obj) => obj is BigNumMetric bc && this == bc;

    public override int GetHashCode() => ToDouble().GetHashCode();

    // Formatting



    public override string ToString()
    {
        return ToString(1);
    }

    public string ToString(int decimalPlaces = 1)
    {
        double value = ToDouble();

        if (value == 0) return "0";

        double absValue = Math.Abs(value);
        string[] metricPrefixes = { "", "K", "M", "G", "T", "P" };
        int prefixIndex = 0;

        while (absValue >= 1000 && prefixIndex < metricPrefixes.Length - 1)
        {
            absValue /= 1000;
            prefixIndex++;
        }

        double displayValue = value / Math.Pow(1000, prefixIndex);
        string format;

        if (prefixIndex == 0)
        {
            format = "F0";
        }
        else
        {
            format = decimalPlaces == 0 ? "F0" : $"F{decimalPlaces}";
        }

        return displayValue.ToString(format) + metricPrefixes[prefixIndex];
    }

    public static BigNumMetric Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new FormatException("Input string was not in a correct format.");

        value = value.Trim();
        var uppercase = value.ToUpperInvariant();
        var suffixes = new Dictionary<string, int>
        {
            { "P", 15 },
            { "T", 12 },
            { "G", 9 },
            { "M", 6 },
            { "K", 3 }
        };

        foreach (var suffix in suffixes)
        {
            if (uppercase.EndsWith(suffix.Key, StringComparison.Ordinal))
            {
                var numberPart = uppercase.Substring(0, uppercase.Length - suffix.Key.Length).Trim();
                if (string.IsNullOrEmpty(numberPart))
                    throw new FormatException("Input string was not in a correct format.");

                double coeff = double.Parse(numberPart);
                return new BigNumMetric(coeff, suffix.Value);
            }
        }

        // Fallback to the base BigIntMetric parser for scientific/exact values.
        var baseValue = BigNum.Parse(value);
        return new BigNumMetric(baseValue.coefficient, baseValue.exponent);
    }

    public static bool TryParse(string value, out BigNumMetric result)
    {
        result = null;
        try
        {
            result = Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
