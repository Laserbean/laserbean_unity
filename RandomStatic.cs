using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General
{

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


public static class Roulette {

    public static int Spin(float[] weights)
    {
        float totalWeight = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            totalWeight += weights[i];
        }

        float randomWeight = UnityEngine.Random.Range(0f, totalWeight);
        for (int i = 0; i < weights.Length; i++)
        {
            if (randomWeight < weights[i])
            {
                return i;
            }
            randomWeight -= weights[i];
        }

        return -1;
    }

    public static int SpinEqual(int numOptions)
    {
        float[] weights = new float[numOptions];
        for (int i = 0; i < numOptions; i++)
        {
            weights[i] = 1f / numOptions;
        }

        return Spin(weights);
    }

}

}