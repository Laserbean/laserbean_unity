using Laserbean.General;
using UnityEngine;

public class SmoothFollowSimple : MonoBehaviour
{
    Rigidbody2D rgbd2d;

    [SerializeField] float FirstOrder = 10f; 
    [SerializeField] float SecondOrder = 50f; 
    [SerializeField] float ThirdOrder = 2f;

    [SerializeField] public Transform Target; 
    void Awake()
    {
        rgbd2d = this.GetComponent<Rigidbody2D>(); 
    }

    public void FixedUpdate()
    {
        Vector3 dir = Target.position - transform.position;

        // rgbd2d.AddForce(dir * Forcemodifier);
        // PID control
        Vector2 error = Target.position - transform.position;
        Vector2 derivative = rgbd2d.linearVelocity * -1f;
        Vector2 integral = error * Time.fixedDeltaTime; // Simple integral approximation

        float p = FirstOrder;
        float i = SecondOrder;
        float d = ThirdOrder;

        Vector2 pidForce = (p * error) + (i * integral) + (d * derivative);
        rgbd2d.AddForce(pidForce);

    }

}
