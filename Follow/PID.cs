using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PID_Vector3
{
    [Header("PID Constants")]
    [SerializeField] public float p_const = 15f;
    [SerializeField] public float i_const = 10f;
    [SerializeField] public float d_const = 5f;

    [Header("Integral Buffer Size"), Range(0, 10000)]
    [SerializeField] float buffer_size = 64;
    readonly private Queue<Vector3> total_error_buffer = new();
    private Vector3 Derivative
    {
        get
        {
            return (current_error - previous_error) / Time.fixedDeltaTime;
        }
    }

    Vector3 previous_error = Vector3.zero;
    Vector3 current_error = Vector3.zero;
    Vector3 total_error = Vector3.zero;

    public Vector3 Force = Vector3.zero;

    public PID_Vector3() { }
    public PID_Vector3(float p, float i, float d)
    {
        p_const = p;
        i_const = i;
        d_const = d;
    }

    public void FixedUpdate(Vector3 position, Vector3 TargetPosition, float deltaTime)
    {
        previous_error = current_error;
        current_error = TargetPosition - position;
        var integral = current_error * deltaTime; // Simple integral approximation

        if (buffer_size > 0)
        {
            if (total_error_buffer.Count >= buffer_size)
            {
                total_error -= total_error_buffer.Dequeue();
            }
            total_error_buffer.Enqueue(integral);
        }
        total_error += integral;

        Force = (p_const * current_error) + (i_const * total_error) + (d_const * Derivative);

    }

}
public class PID_Float
{
    [Header("PID Constants")]
    [SerializeField] public float p_const = 15f;
    [SerializeField] public float i_const = 10f;
    [SerializeField] public float d_const = 5f;

    [Header("Integral Buffer Size"), Range(0, 10000)]
    [SerializeField] float buffer_size = 64;
    readonly private Queue<float> total_error_buffer = new();
    private float Derivative
    {
        get
        {
            return (current_error - previous_error) / Time.fixedDeltaTime;
        }
    }

    float previous_error = 0f;
    float current_error = 0f;
    float total_error = 0f;

    public float Force = 0f;

    public PID_Float() { }
    public PID_Float(float p, float i, float d)
    {
        p_const = p;
        i_const = i;
        d_const = d;
    }

    public void FixedUpdate(float position, float TargetPosition, float deltaTime)
    {
        previous_error = current_error;
        current_error = TargetPosition - position;
        var integral = current_error * deltaTime; // Simple integral approximation

        if (buffer_size > 0)
        {
            if (total_error_buffer.Count >= buffer_size)
            {
                total_error -= total_error_buffer.Dequeue();
            }
            total_error_buffer.Enqueue(integral);
        }
        total_error += integral;

        Force = (p_const * current_error) + (i_const * total_error) + (d_const * Derivative);
    }
}