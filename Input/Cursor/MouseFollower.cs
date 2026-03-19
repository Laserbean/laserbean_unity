using System;
using Laserbean.General;
using Laserbean.General.Follower;
using UnityEngine;
using UnityEngine.InputSystem;

// public class MouseFollower : MonoBehaviour
// {
//     // [SerializeField] private Camera cam;

//     // Update is called once per frame
//     void Update()
//     {
//         // Vector3 MousePos = ;
//         transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
//         transform.position = transform.position.Modify(0, CardinalAxis.Z);
//     }
// }


// public class MouseFollower : MonoBehaviour
// {
//     // [SerializeField] private Camera cam;

//     // Update is called once per frame
//     SmoothPosFollower smoothFollower;
//     FollowTarget followTarget;
//     void Start()
//     {
//         smoothFollower = this.GetComponent<SmoothPosFollower>();
//         followTarget = new(Vector3.zero);
//         smoothFollower.AddTarget(followTarget);

//     }


//     void Update()
//     {
//         // Vector3 MousePos = ;
//         var targetpos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
//         targetpos = targetpos.Modify(0, CardinalAxis.Z);
//         followTarget.Position = targetpos;
//     }
// }

public class MouseFollower : MonoBehaviour
{
    // [SerializeField] private Camera cam;

    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 50;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Calculate the direction and distance to the mouse position
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition = mousePosition.Modify(0, CardinalAxis.Z);

        Vector2 direction = mousePosition.ToVector2() - rb.position;

        // Calculate the velocity needed to move towards the mouse position
        // Using Vector2.Lerp provides a smooth easing effect
        Vector2 newPosition = Vector2.Lerp(rb.position, mousePosition, moveSpeed * Time.fixedDeltaTime);

        // Move the Rigidbody2D to the new position
        rb.MovePosition(newPosition);
    }
}


