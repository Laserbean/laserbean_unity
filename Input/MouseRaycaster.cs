using Laserbean.Input.WorldDrag;
using UnityEngine;

namespace Laserbean.NewInput
{
    public class MouseRaycaster : MonoBehaviour, IMouseInputs
    {
        [SerializeField] private Camera cam;

        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private LayerMask floorLayer;

        [SerializeField] private float dragDistance = 40f;
        [SerializeField] bool showDebug = false;
        [SerializeField] private bool use2D = true;
        [SerializeField] private float floatDistance = 20f;

        void Start()
        {
            if (cam == null)
            {
                cam = Camera.main;
            }
        }

        Transform CurrentTransform = null;
        IWorldClickable worldclickable;
        IWorldDraggable worlddraggable;
        public void OnClickDown(Vector2 ScreenPoint)
        {
            CurrentTransform = GetClickedTransform(ScreenPoint);

            if (CurrentTransform != null)
            {
                worldclickable = CurrentTransform.GetComponent<IWorldClickable>();
                worldclickable?.OnClickDown();

            }

        }

        public void OnClickUp(Vector2 ScreenPoint)
        {
            worldclickable?.OnClickUp();
            worldclickable = null;
            CurrentTransform = null;
        }

        public void OnDoubleClick(Vector2 ScreenPoint)
        {

            worldclickable?.OnDoubleClick();

        }

        public void OnDrag(Vector2 ScreenPoint)
        {
            worlddraggable?.Drag(GetWorldPosition(ScreenPoint));
            worlddraggable = null;
        }

        public void OnDragEnd(Vector2 ScreenPoint)
        {

            worlddraggable?.DragReleased();
        }

        public void OnDragStart(Vector2 ScreenPoint)
        {

            if (CurrentTransform != null)
            {
                worlddraggable = CurrentTransform.GetComponent<IWorldDraggable>();
                worlddraggable?.DragStarted();
            }
        }

        public void OnHold(Vector2 ScreenPoint)
        {

        }

        public void OnPointMove(Vector2 ScreenPoint)
        {

        }


        // private Transform currentObject;
        private Vector3 grabOffset;
        private Plane dragPlane;

        private void InitializeDragPlane(Ray ray, Vector3 hitPoint)
        {
            var planeOrigin = cam.transform.position - (cam.transform.position - hitPoint).normalized * dragDistance;
            dragPlane = new Plane(-cam.transform.forward, planeOrigin);
        }

        private Transform GetClickedTransform(Vector2 screenPos)
        {
            if (use2D)
            {
                return AttemptGetClickedObject2D(screenPos);
            }
            else
            {
                return AttemptGetClickedObject3D(screenPos);
            }
        }


        private Vector3 GetWorldPosition(Vector3 screenPos)
        {
            Ray ray = cam.ScreenPointToRay(screenPos);

            Vector3 surfaceFloatOffset = Vector3.zero;

            // Try to hit the floor layer for floating
            if (Physics.Raycast(ray, out RaycastHit hit, dragDistance, floorLayer))
            {
                dragPlane = new Plane(hit.normal, hit.point + hit.normal * floatDistance);
                DebugDrawRay(hit.point, hit.normal, Color.yellow, 0.1f);
            }
            else if (Physics.Raycast(ray, out hit, dragDistance + floatDistance, floorLayer))
            {
                var planeOrigin = ray.origin + ray.direction * dragDistance;
                dragPlane = new Plane(-cam.transform.forward, planeOrigin);

                var rayDirection = (-hit.point + cam.transform.position).normalized;
                surfaceFloatOffset = rayDirection * floatDistance;
                DebugDrawRay(hit.point, hit.normal, Color.yellow, 0.1f);
            }
            else
            {
                // No floor hit, use fixed drag distance plane
                var planeOrigin = ray.origin + ray.direction * dragDistance;
                dragPlane = new Plane(-cam.transform.forward, planeOrigin);
            }

            // Calculate intersection with drag plane
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                return hitPoint + grabOffset + surfaceFloatOffset;
            }

            Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);

            return worldPos; // Fallback
        }



        private Transform AttemptGetClickedObject3D(Vector2 screenPos)
        {
            Transform currentObject = null;
            Ray ray = cam.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactableLayer))
            {
                currentObject = hit.transform;
                InitializeDragPlane(ray, hit.point);
                grabOffset = currentObject.position - hit.point;
                DebugLog($"[Grab] Grabbed 3D object: {currentObject.name}");
            }
            return currentObject;
        }

        private Transform AttemptGetClickedObject2D(Vector2 screenPos)
        {
            Transform currentObject = null;

            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
            RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, interactableLayer);

            if (hit2D.collider != null)
            {
                currentObject = hit2D.transform;
                dragPlane = new Plane(-cam.transform.forward, hit2D.point);
                grabOffset = currentObject.position - (Vector3)hit2D.point;
                DebugLog($"[Grab] Grabbed 2D object: {currentObject.name}");
            }
            return currentObject;
        }




        void DebugLog(string text)
        {
            if (showDebug)
                Debug.Log(text);
        }

        void DebugDrawRay(Vector3 start, Vector3 dir, Color color, float duration)
        {
            if (showDebug)
                Debug.DrawRay(start, dir, color, duration);
        }

        public void OnHoldDown(Vector2 ScreenPoint)
        {
            throw new System.NotImplementedException();
        }

        public void OnHoldUp(Vector2 ScreenPoint)
        {
            throw new System.NotImplementedException();
        }
    }
}
