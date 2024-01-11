using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General
{

    public class SecondOrderDynamics
    {
        Vector3 xp;
        Vector3 y, yd; //state variables
        float k1, k2, k3;
        float _w, _z, _d;
        const float PI = 3.141592654f;


        public SecondOrderDynamics(float f, float z, float r, Vector3 x0)
        {
            // f = frequency, 
            // z = damping, 0 to 1 underdamped >1 overdamped
            // r = response; 
            k1 = z / (PI * f);
            k2 = 1 / ((2 * PI) * (2 * PI * f));
            k3 = r * z / (2 * PI * f);

            _w = 2 * PI * f;
            _z = z;
            _d = _w * Mathf.Sqrt(Mathf.Abs(z * z - 1));

            xp = x0;
            y = x0;
            yd = Vector3.zero;
        }

        public Vector3 Step(float T, Vector3 x, Vector3 xd = default)
        {
            //T is the time between frames.
            if (xd == default) {
                xd = (x - xp) / T;
                xp = x;
            }
            // float k2_stable = Mathf.Max(k2, 1.1f * (T* T/4 + T*k1/2)); 
            float k2_stable = Mathf.Max(k2, T * T / 2 + T * k1 / 2, T * k1);

            y += T * yd;
            // yd = yd + T * (x + k3 * xd -y - k1 * yd) /k2; 
            yd += T * (x + k3 * xd - y - k1 * yd) / k2_stable;
            return y;
        }

        public Vector3 StepPoleMatch(float T, Vector3 x, Vector3 xd = default)
        {
            if (xd == default) {
                xd = (x - xp) / T;
                xp = x;
            }
            // float k2_stable = Mathf.Max(k2, 1.1f * (T* T/4 + T*k1/2)); 
            float k1_stable, k2_stable;

            if (_w * T < _z) {
                k1_stable = k1;
                k2_stable = Mathf.Max(k2, T * T / 2 + T * k1 / 2, T * k1);
            } else {
                float t1 = Mathf.Exp(-_z * _w * T);
                float alpha = 2 * t1 * (_z <= 1 ? Mathf.Cos(T * _d) : (float)System.Math.Cosh(T * _d));
                float beta = t1 * t1;
                float t2 = T / (1 + beta - alpha);
                k1_stable = (1 - beta) * t2;
                k2_stable = T * t2;
            }


            y += T * yd;
            // yd = yd + T * (x + k3 * xd -y - k1 * yd) /k2; 
            yd += T * (x + k3 * xd - y - k1 * yd) / k2_stable;
            return y;
        }

    }



    public class SecondOrderDynamicsFloat
    {
        float xp;
        float y, yd; //state variables


        float k1, k2, k3;
        float _w, _z, _d;
        const float PI = 3.141592654f;


        public SecondOrderDynamicsFloat(float f, float z, float r, float x0)
        {
            // f = frequency, 
            // z = damping, 0 to 1 underdamped >1 overdamped
            // r = response; 
            k1 = z / (PI * f);
            k2 = 1 / ((2 * PI) * (2 * PI * f));
            k3 = r * z / (2 * PI * f);

            _w = 2 * PI * f;
            _z = z;
            _d = _w * Mathf.Sqrt(Mathf.Abs(z * z - 1));

            xp = x0;
            y = x0;
            yd = 0;
        }

        public float Step(float T, float x, float xd = 0)
        {
            //T is the time between frames.
            if (xd == 0) {
                xd = (x - xp) / T;
                xp = x;
            }
            // float k2_stable = Mathf.Max(k2, 1.1f * (T* T/4 + T*k1/2)); 
            float k2_stable = Mathf.Max(k2, T * T / 2 + T * k1 / 2, T * k1);

            y += T * yd;
            // yd = yd + T * (x + k3 * xd -y - k1 * yd) /k2; 
            yd += T * (x + k3 * xd - y - k1 * yd) / k2_stable;
            return y;
        }

        public float StepPoleMatch(float T, float x, float xd = 0)
        {
            if (xd == 0) {
                xd = (x - xp) / T;
                xp = x;
            }
            // float k2_stable = Mathf.Max(k2, 1.1f * (T* T/4 + T*k1/2)); 
            float k1_stable, k2_stable;

            if (_w * T < _z) {
                k1_stable = k1;
                k2_stable = Mathf.Max(k2, T * T / 2 + T * k1 / 2, T * k1);
            } else {
                float t1 = Mathf.Exp(-_z * _w * T);
                float alpha = 2 * t1 * (_z <= 1 ? Mathf.Cos(T * _d) : (float)System.Math.Cosh(T * _d));
                float beta = t1 * t1;
                float t2 = T / (1 + beta - alpha);
                k1_stable = (1 - beta) * t2;
                k2_stable = T * t2;
            }


            y += T * yd;
            // yd = yd + T * (x + k3 * xd -y - k1 * yd) /k2; 
            yd += T * (x + k3 * xd - y - k1 * yd) / k2_stable;
            return y;
        }

    }


}