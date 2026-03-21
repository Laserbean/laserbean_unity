using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Laserbean.Input.WorldDrag
{
    /// <summary>
    /// Handles world-space dragging with raycasting support for both 3D and 2D physics.
    /// Works with the MouseInputs class to receive input events and convert them to world-space operations.
    /// </summary>
    /// 
    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform> { }

    [System.Serializable]
    public class TransformEventVector2Event : UnityEvent<Transform, Vector2> { }

    public class WorldDragIInput2 : MonoBehaviour, IMouseInputable2
    {
        [SerializeField] private Camera cam;
        [SerializeField] private LayerMask draggableLayer;
        [SerializeField] private LayerMask hoverLayer;
        [SerializeField] private LayerMask floorLayer;
        [SerializeField] private float dragDistance = 40f;
        [SerializeField] private float dragBreakDistance = 5f;
        [SerializeField] private bool use2D = true;
        [SerializeField] private float floatDistance = 20f;

        private Transform currentObject;
        private Vector3 grabOffset;
        private Plane dragPlane;
        private GameObject lastHoverObject;
        private bool isCurrentlyDragging;


        [SerializeField] bool showDebug = false;


        #region Unity Events



        [Header("Left Click Events")]
        [SerializeField] private TransformEvent onLeftClickDown = new TransformEvent();
        [SerializeField] private TransformEvent onLeftClickUp = new TransformEvent();
        [SerializeField] private TransformEvent onLeftDragStart = new TransformEvent();
        [SerializeField] private TransformEvent onLeftDrag = new TransformEvent();
        [SerializeField] private TransformEvent onLeftDragEnd = new TransformEvent();
        // [SerializeField] private UnityEvent onLeftClickSuccessful = new UnityEvent();


        #endregion

        #region IMouseInputable2 Implementation

        public void OnPointMove(Vector2 screenPos)
        {
            HandleHoverDetection(screenPos);
        }

        public void OnLeftClickDown(Vector2 screenPos)
        {
            if (AttemptGetClickedObject(screenPos))
            {
                var clickable = currentObject.GetComponent<IWorldClickable>();
                if (clickable != null)
                {
                    clickable?.OnClickDown();
                    onLeftClickDown?.Invoke(currentObject.transform);
                }
            }
        }

        public void OnLeftClickUp(Vector2 screenPos)
        {

            ReleaseObject();
        }

        public void OnLeftDragStart(Vector2 screenPos)
        {
            isCurrentlyDragging = true;

            if (currentObject != null)
            {
                var draggable = currentObject.GetComponent<IWorldDraggable>();
                if (draggable == null) return;

                onLeftDragStart?.Invoke(currentObject);
                draggable.DragStarted(); // TODO
            }
        }

        public void OnLeftDrag(Vector2 screenPos)
        {

            if (currentObject != null && isCurrentlyDragging)
            {
                var dragsuccess = PerformDrag(screenPos);

                if (!dragsuccess)
                {
                    isCurrentlyDragging = false;
                    ReleaseObject();
                }
            }
        }

        public void OnLeftDragEnd(Vector2 screenPos)
        {
            isCurrentlyDragging = false;
        }
        #endregion

        #region Optional
        public void OnLeftDoubleClick(Vector2 screenPos)
        {
            // Optional: Implement double-click behavior (e.g., special grab or select)
        }

        public void OnRightClickDown(Vector2 screenPos)
        {
            // Optional: Implement right-click behavior
        }

        public void OnRightClickUp(Vector2 screenPos)
        {
            // Optional: Implement right-click release behavior
        }

        public void OnRightDrag(Vector2 screenPos)
        {
            // Optional: Implement right-click drag behavior (e.g., camera rotation)
        }

        public void OnMiddleClickDown(Vector2 screenPos)
        {
            // Optional: Implement middle-click behavior
        }

        public void OnMiddleClickUp(Vector2 screenPos)
        {
            // Optional: Implement middle-click release behavior
        }

        public void OnMiddleDrag(Vector2 screenPos)
        {
            // Optional: Implement middle-click drag behavior (e.g., pan camera)
        }

        public void OnScroll(Vector2 scrollDelta, Vector2 screenPos)
        {
            // Optional: Implement scroll behavior (e.g., zoom camera, adjust height)
        }
        public void OnRightDragStart(Vector2 ScreenPoint)
        {
        }

        public void OnRightDragEnd(Vector2 ScreenPoint)
        {
        }

        public void OnRightDoubleClick(Vector2 ScreenPoint)
        {
        }

        public void OnMiddleDragStart(Vector2 ScreenPoint)
        {
        }

        public void OnMiddleDragEnd(Vector2 ScreenPoint)
        {
        }

        public void OnMiddleDoubleClick(Vector2 ScreenPoint)
        {
        }
        #endregion

        #region Private Methods

        private void HandleHoverDetection(Vector2 screenPos)
        {
            GameObject hitObject = null;

            if (use2D)
            {
                hitObject = Raycast2D(screenPos);
            }
            else
            {
                hitObject = Raycast3D(screenPos);
            }

            // Handle hover state changes
            if (hitObject != lastHoverObject)
            {
                if (lastHoverObject != null)
                {
                    lastHoverObject.GetComponent<IHoverable>()?.OnHoverExit();
                    DebugLog($"[Hover] Exited: {lastHoverObject.name}");
                }

                lastHoverObject = hitObject;

                if (lastHoverObject != null)
                {
                    lastHoverObject.GetComponent<IHoverable>()?.OnHoverEnter();
                    DebugLog($"[Hover] Entered: {lastHoverObject.name}");
                }
            }
        }

        private bool AttemptGetClickedObject(Vector2 screenPos)
        {
            if (currentObject != null)
            {
                return false; // Already grabbing something
            }

            if (use2D)
            {
                return AttemptGetClickedObject2D(screenPos);
            }
            else
            {
                return AttemptGetClickedObject3D(screenPos);
            }
        }

        private bool AttemptGetClickedObject3D(Vector2 screenPos)
        {
            Ray ray = cam.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, draggableLayer))
            {
                currentObject = hit.transform;
                InitializeDragPlane(ray, hit.point);
                grabOffset = currentObject.position - hit.point;
                DebugLog($"[Grab] Grabbed 3D object: {currentObject.name}");
                return true;
            }
            return false;
        }

        private bool AttemptGetClickedObject2D(Vector2 screenPos)
        {
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
            RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, draggableLayer);

            if (hit2D.collider != null)
            {
                currentObject = hit2D.transform;
                dragPlane = new Plane(-cam.transform.forward, hit2D.point);
                grabOffset = currentObject.position - (Vector3)hit2D.point;
                DebugLog($"[Grab] Grabbed 2D object: {currentObject.name}");
                return true;
            }
            return false;
        }

        private void ReleaseObject()
        {
            if (currentObject != null)
            {
                currentObject.GetComponent<IWorldClickable>()?.OnClickUp();
                currentObject.GetComponent<IWorldDraggable>()?.DragReleased();

                onLeftClickUp?.Invoke(currentObject);
                onLeftDragEnd?.Invoke(currentObject);

                DebugLog($"[Release] Released: {currentObject.name}");
                currentObject = null;
            }
        }

        private bool PerformDrag(Vector2 screenPos)
        {
            if (!currentObject.TryGetComponent<IWorldDraggable>(out var draggable))
            {
                return false;
            }

            Ray ray = cam.ScreenPointToRay(screenPos);
            Vector3 movePosition = CalculateDragPosition(ray);

            if (use2D)
            {
                movePosition.z = currentObject.transform.position.z;
            }

            var curdragdistance = (movePosition - currentObject.transform.position).sqrMagnitude;
            // Debug.Log(curdragdistance);

            if (curdragdistance >= dragBreakDistance * dragBreakDistance)
            {
                return false;
            }
            draggable.Drag(movePosition);
            onLeftDrag?.Invoke(currentObject);

            return true;
        }

        private Vector3 CalculateDragPosition(Ray ray)
        {
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

            return currentObject.position; // Fallback
        }

        private void InitializeDragPlane(Ray ray, Vector3 hitPoint)
        {
            var planeOrigin = cam.transform.position - (cam.transform.position - hitPoint).normalized * dragDistance;
            dragPlane = new Plane(-cam.transform.forward, planeOrigin);
        }

        private GameObject Raycast3D(Vector2 screenPos)
        {
            Ray ray = cam.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, hoverLayer))
            {
                DebugDrawRay(hit.point, hit.normal, Color.red, 0.1f);
                return hit.transform.gameObject;
            }

            return null;
        }

        private GameObject Raycast2D(Vector2 screenPos)
        {
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
            RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, hoverLayer);

            if (hit2D.collider != null)
            {
                Debug.DrawLine(hit2D.point, hit2D.point + Vector2.up, Color.red);
                return hit2D.collider.gameObject;
            }

            return null;
        }

        #endregion

        #region Debug Gizmos

        void OnDrawGizmos()
        {
            if (currentObject != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(currentObject.position, 0.2f);
            }
        }

        #endregion


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

    }
}
