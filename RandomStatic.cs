using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomStatic
{
    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;
        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

    const string glyphs= "abcdefghijklmnopqrstuvwxyz0123456789";

    public static string GenerateRandomString(int charAmmount) {
        string mystring = ""; 
        for(int i=0; i < charAmmount; i++)
        {
            mystring += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
        }
        return mystring; 
    }
}
