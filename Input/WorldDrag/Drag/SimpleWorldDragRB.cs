using Laserbean.General;
using UnityEngine;
using UnityEngine.Events;

namespace Laserbean.Input.WorldDrag
{
    public class SimpleWorldDragRB : MonoBehaviour, IWorldDraggable
    {
        Rigidbody2D rb2d;
        Rigidbody rb;
        bool isDragging = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb2d = GetComponent<Rigidbody2D>();
        }

        public void Drag(Vector3 mouseLocation)
        {
            // isDragging = false;
            // transform.position = mouseLocation;

            if (rb != null)
            {
                rb.MovePosition(mouseLocation);
            }
            else if (rb2d != null)
            {
                rb2d.MovePosition(mouseLocation);
            }
            OnDrag.Invoke(mouseLocation);

        }

        public void DragReleased()
        {
            isDragging = false;
            OnDragStop.Invoke();

            worldDragOver?.DragReleased(transform);
        }


        public bool DragStarted()
        {
            isDragging = true;
            OnDragStart.Invoke();
            return true;
        }

        [SerializeField] UnityEvent OnDragStart;
        [SerializeField] UnityEvent<Vector3> OnDrag;
        [SerializeField] UnityEvent OnDragStop;
        IWorldDragOver worldDragOver;


        void OnTriggerEnter2D(Collider2D collision)
        {
            if (isDragging)
            {
                worldDragOver = collision.GetComponent<IWorldDragOver>();
                worldDragOver?.DragOver(transform);
            }
        }

        void OTriggerExit2D(Collider2D collision)
        {
            if (isDragging)
            {
                worldDragOver?.DragExit(transform);
            }
        }
    }
}