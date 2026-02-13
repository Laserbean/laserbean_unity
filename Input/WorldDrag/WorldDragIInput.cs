using UnityEngine;
using UnityEngine.InputSystem;

namespace Laserbean.Input.WorldDrag
{
    public class WorldDragIInput : MonoBehaviour, IMouseInputable
    {
        [SerializeField] private Camera cam; // assign or default to Camera.main
        [SerializeField] private LayerMask draggableLayer;
        [SerializeField] private LayerMask hoverLayer; // could be same as draggableLayer or broader
        [SerializeField] private LayerMask FloorLayer; // could be same as draggableLayer or broader
        [SerializeField] private float dragDistance = 40f; // optional fixed distance from camera
        [SerializeField] private bool use2D = true;
        [SerializeField] private float floatDistance = 20f;

        private Transform GrabbedObject;
        private Vector3 grabOffset;
        private Plane dragPlane;

        private GameObject lastHover; // track hover for enter/exit

        void IMouseInputable.OnPointMove(Vector2 screenPos)
        {
            GameObject hitObj = null;

            if (use2D)
            {
                // -------- 2D hover --------
                // Convert screen to world point. Z is irrelevant for 2D physics (usually orthographic) so we take camera to get proper XY.
                Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
                RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, draggableLayer);
                if (hit2D.collider != null)
                {
                    hitObj = hit2D.collider.gameObject;
                    Debug.DrawLine(hit2D.point, hit2D.point + Vector2.up, Color.red);
                }
            }
            else
            {
                // -------- 3D hover --------
                Ray ray = cam.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, hoverLayer))
                {
                    hitObj = hit.transform.gameObject;

                    Debug.DrawRay(hit.point, hit.normal, Color.red, 1f);
                }
            }
            // Decide which to use
            var newHover = hitObj;

            // Handle enter / exit
            if (newHover != lastHover)
            {
                if (lastHover != null)
                {
                    var prev = lastHover.GetComponent<IHoverable>();
                    prev?.OnHoverExit();
                }

                lastHover = newHover;

                if (lastHover != null)
                {
                    var curr = lastHover.GetComponent<IHoverable>();
                    curr?.OnHoverEnter();
                }
            }
            else if (newHover == null && lastHover != null)
            {
                // pointer left all hoverables
                var prev = lastHover.GetComponent<IHoverable>();
                prev?.OnHoverExit();
                lastHover = null;
            }
        }

        void IMouseInputable.OnClickDown(Vector2 screenPos)
        {
            // Debug.Log("Clicked" + screenPos + "");
            Ray ray = cam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, draggableLayer))
            {
                GrabbedObject = hit.transform;
                // // define a plane orthogonal to camera through hit point
                var plane_origin = cam.transform.position - (cam.transform.position - hit.point).normalized * dragDistance;
                DebugMoveToPos2 = plane_origin;
                dragPlane = new Plane(-cam.transform.forward, plane_origin);

                // calculate offset from hit point to object pivot
                grabOffset = GrabbedObject.position - hit.point;
            }
            else
            {
                Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
                RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, draggableLayer);
                if (hit2D.collider != null)
                {
                    GrabbedObject = hit2D.transform;
                    dragPlane = new Plane(-cam.transform.forward, hit2D.point);
                    grabOffset = GrabbedObject.position - (Vector3)hit2D.point;

                    var clickable = hit2D.collider.gameObject.GetComponent<IWorldClickable>();
                    clickable?.OnClickPressed();
                }
            }
        }

        void IMouseInputable.OnClickUp(Vector2 ScreenPoint)
        {
            if (GrabbedObject != null)
            {
                // Can implement drop logic here (e.g., snapping, checking valid drop zone)
                var clickable = GrabbedObject.gameObject.GetComponent<IWorldClickable>();
                clickable?.OnClickReleased();

                var grabbable = GrabbedObject.gameObject.GetComponent<IWorldDraggable>();
                if (grabbable == null)
                {
                    GrabbedObject = null;
                    return;
                }
                grabbable.DragReleased();

                GrabbedObject = null;
            }
        }

        void IMouseInputable.OnDrag(Vector2 ScreenPoint)
        {
            curDragPos = ScreenPoint;

            if (GrabbedObject != null)
            {
                if (!GrabbedObject.TryGetComponent<IWorldDraggable>(out var grabbable)) { return; }
                Ray ray = cam.ScreenPointToRay(curDragPos);

                var surfaceFloatOffset = Vector3.zero;

                if (Physics.Raycast(ray, out RaycastHit hit, dragDistance, FloorLayer))
                {
                    // dragPlane = new Plane(-cam.transform.forward, hit.point);
                    dragPlane = new Plane(hit.normal, hit.point + hit.normal * floatDistance);
                    // var vray = (-hit.point + cam.transform.position).normalized;
                    // surfaceFloatOffset = vray * floatDistance * (1 - Vector3.Dot(vray, hit.normal));
                    Debug.DrawRay(hit.point, hit.normal, Color.yellow, 1f);
                }
                else if (Physics.Raycast(ray, out hit, dragDistance + floatDistance, FloorLayer))
                {
                    var plane_origin = ray.origin + ray.direction * dragDistance;
                    DebugMoveToPos2 = plane_origin;
                    dragPlane = new Plane(-cam.transform.forward, plane_origin);

                    var vray = (-hit.point + cam.transform.position).normalized;
                    surfaceFloatOffset = vray * floatDistance;
                    Debug.DrawRay(hit.point, hit.normal, Color.yellow, 1f);
                }
                else
                {
                    // If the ray didn't hit anything, use the furthest point along the ray at dragDistance
                    var plane_origin = ray.origin + ray.direction * dragDistance;
                    DebugMoveToPos2 = plane_origin;
                    dragPlane = new Plane(-cam.transform.forward, plane_origin);
                }

                if (dragPlane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);

                    MoveToPos = hitPoint + grabOffset + surfaceFloatOffset;
                    grabbable.Drag(MoveToPos);
                }
            }
        }

        Vector2 curDragPos;
        Vector3 MoveToPos { get; set; }
        Vector3 DebugMoveToPos2 { get; set; }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(MoveToPos, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(DebugMoveToPos2, Vector3.one * 0.2f);
        }
   
    }
}
