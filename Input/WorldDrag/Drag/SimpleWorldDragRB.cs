using Laserbean.General;
using UnityEngine;
using UnityEngine.Events;

namespace Laserbean.Input.WorldDrag
{
    public class SimpleWorldDragRB : MonoBehaviour, IWorldDraggable, IWorldInteractable
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
            if (!isDragable) return;

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

            if (!isDragable) return;

            OnDragStop.Invoke();

            worldDragOver?.DragReleased(transform);
        }


        public bool DragStarted()
        {
            if (!isDragable) return false;
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
        [SerializeField] bool isDragable = true;
        public void SetInteractable(bool canInteract)
        {
            isDragable = canInteract;
        }

        public bool IsInteractable()
        {
            return isDragable;
        }
    }
}