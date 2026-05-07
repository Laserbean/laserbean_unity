using System;
using System.Collections.Generic;
using System.Numerics;
using Laserbean.CustomUnityEvents;
using Laserbean.EventManager;
using Laserbean.General;
using Laserbean.Removable;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BigNum : System.IComparable<BigNum>, System.IEquatable<BigNum>, ISerializationCallbackReceiver
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

    public BigNum(double coeff, int exp)
    {
        Normalize(coeff, exp);
    }

    public BigNum(long value)
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

    public BigNum(int value) : this((long)value) { }

    public BigNum(double value)
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
            if (TryParse(InputText, out BigNum result))
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

    public BigNum TruncateToExponent()
    {
        if (coefficient == 0)
        {
            return new BigNum(0);
        }

        if (exponent >= 0)
        {
            return new BigNum(Math.Truncate(coefficient), exponent);
        }

        // If exponent is negative, any value is already smaller than 1 at the current exponent scale.
        // Convert to an integer value without fractional remainder.
        return new BigNum((long)Math.Truncate(ToDouble()));
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
    public static BigNum operator +(BigNum a, BigNum b)
    {
        if (a == null) a = new BigNum(0);
        if (b == null) b = new BigNum(0);

        if (a.exponent == b.exponent)
            return new BigNum(a.coefficient + b.coefficient, a.exponent);

        if (a.exponent > b.exponent)
            return new BigNum(a.coefficient + b.coefficient * Math.Pow(10, b.exponent - a.exponent), a.exponent);

        return new BigNum(a.coefficient * Math.Pow(10, a.exponent - b.exponent) + b.coefficient, b.exponent);
    }

    public static BigNum operator -(BigNum a, BigNum b)
    {
        if (a == null) a = new BigNum(0);
        if (b == null) b = new BigNum(0);

        if (a.exponent == b.exponent)
            return new BigNum(a.coefficient - b.coefficient, a.exponent);

        if (a.exponent > b.exponent)
            return new BigNum(a.coefficient - b.coefficient * Math.Pow(10, b.exponent - a.exponent), a.exponent);

        return new BigNum(a.coefficient * Math.Pow(10, a.exponent - b.exponent) - b.coefficient, b.exponent);
    }

    public static BigNum operator *(BigNum a, BigNum b)
    {
        if (a == null || b == null) return new BigNum(0);
        return new BigNum(a.coefficient * b.coefficient, a.exponent + b.exponent);
    }

    public static BigNum operator /(BigNum a, BigNum b)
    {
        if (a == null) a = new BigNum(0);
        if (b == null || b.coefficient == 0) throw new System.DivideByZeroException();
        return new BigNum(a.coefficient / b.coefficient, a.exponent - b.exponent);
    }

    public static BigNum operator %(BigNum a, BigNum b)
    {
        if (b == null || b.coefficient == 0) throw new System.DivideByZeroException();
        return new BigNum(a.ToDouble() % b.ToDouble(), 0);
    }

    // Unary Operators
    public static BigNum operator +(BigNum a)
    {
        return a ?? new BigNum(0);
    }

    public static BigNum operator -(BigNum a)
    {
        if (a == null) return new BigNum(0);
        return new BigNum(-a.coefficient, a.exponent);
    }

    public static BigNum operator ++(BigNum a)
    {
        if (a == null) a = new BigNum(0);
        return a + new BigNum(1);
    }

    public static BigNum operator --(BigNum a)
    {
        if (a == null) a = new BigNum(0);
        return a - new BigNum(1);
    }

    // Comparison Operators
    public static bool operator ==(BigNum a, BigNum b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return Math.Abs(a.ToDouble() - b.ToDouble()) < double.Epsilon;
    }

    public static bool operator !=(BigNum a, BigNum b) => !(a == b);

    public static bool operator <(BigNum a, BigNum b)
    {
        if (a == null) a = new BigNum(0);
        if (b == null) b = new BigNum(0);
        return a.ToDouble() < b.ToDouble();
    }

    public static bool operator >(BigNum a, BigNum b)
    {
        if (a == null) a = new BigNum(0);
        if (b == null) b = new BigNum(0);
        return a.ToDouble() > b.ToDouble();
    }

    public static bool operator <=(BigNum a, BigNum b) => a < b || a == b;

    public static bool operator >=(BigNum a, BigNum b) => a > b || a == b;

    // Conversion Operators
    public static implicit operator BigNum(int value) => new BigNum(value);
    public static implicit operator BigNum(long value) => new BigNum(value);
    public static implicit operator BigNum(double value) => new BigNum(value);

    public static explicit operator int(BigNum value)
    {
        if (value == null) return 0;
        return (int)value.ToDouble();
    }

    public static explicit operator long(BigNum value)
    {
        if (value == null) return 0;
        return (long)value.ToDouble();
    }

    public static explicit operator double(BigNum value)
    {
        if (value == null) return 0;
        return value.ToDouble();
    }

    // Comparison Interface
    public int CompareTo(BigNum other)
    {
        if (other == null) return 1;
        var diff = this.ToDouble() - other.ToDouble();
        if (diff < 0) return -1;
        if (diff > 0) return 1;
        return 0;
    }

    public bool Equals(BigNum other) => this == other;

    public override bool Equals(object obj) => obj is BigNum bc && this == bc;

    public override int GetHashCode() => ToDouble().GetHashCode();

    // Formatting
    public override string ToString()
    {
        if (coefficient == 0) return "0";
        return exponent == 0 ? coefficient.ToString("F0") : $"{coefficient}e{exponent}";
    }

    public virtual string ToString(string format)
    {
        if (coefficient == 0) return "0";
        return exponent == 0 ? coefficient.ToString(format) : $"{coefficient.ToString(format)}e{exponent}";
    }

    public static BigNum Parse(string value)
    {
        if (value.Contains("e", System.StringComparison.OrdinalIgnoreCase))
        {
            var parts = value.Split(new[] { 'e', 'E' });
            double coeff = double.Parse(parts[0]);
            int exp = int.Parse(parts[1]);
            return new BigNum(coeff, exp);
        }
        return new BigNum(double.Parse(value));
    }

    public static bool TryParse(string value, out BigNum result)
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

    // public void OnBeforeSerialize()
    // {
    //     // throw new NotImplementedException();
    // }

    // public void OnAfterDeserialize()
    // {
    //     Display = ToDouble();
    //     // throw new NotImplementedException();
    // }
}