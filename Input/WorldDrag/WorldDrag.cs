using UnityEngine;
using UnityEngine.InputSystem;

namespace Laserbean.Input.WorldDrag
{
    public class WorldDrag : MonoBehaviour
    {
        [SerializeField] private Camera cam; // assign or default to Camera.main
        [SerializeField] private LayerMask draggableLayer;
        [SerializeField] private LayerMask hoverLayer; // could be same as draggableLayer or broader

        [SerializeField] private float dragDistance = 0f; // optional fixed distance from camera

        [SerializeField] private bool prefer2DHover = true;

        private InputAction clickAction;
        private InputAction pointAction;

        private Transform GrabbedObject;
        private Vector3 grabOffset;
        private Plane dragPlane;

        void OnEnable()
        {
            cam = cam != null ? cam : Camera.main;
            var inputActionAsset = ScriptableObject.CreateInstance<InputActionAsset>();

            pointAction = new InputAction("point", binding: "<Pointer>/position");
            clickAction = new InputAction("click", binding: "<Mouse>/leftButton");

            clickAction.AddBinding("<Touchscreen>/touch*/press");

            pointAction.Enable();
            clickAction.Enable();

            clickAction.performed += OnClickPerformed;
            clickAction.canceled += OnClickReleased;
            pointAction.performed += OnPointMoved; // for hover
        }

        void OnDisable()
        {
            clickAction.performed -= OnClickPerformed;
            clickAction.canceled -= OnClickReleased;
            pointAction.performed -= OnPointMoved;
            pointAction.Disable();
            clickAction.Disable();
        }

        private GameObject lastHover; // track hover for enter/exit

        private void OnPointMoved(InputAction.CallbackContext ctx)
        {
            Vector2 screenPos = pointAction.ReadValue<Vector2>();
            GameObject hitObj2D = null;
            GameObject hitObj3D = null;


            // -------- 2D hover --------
            // Convert screen to world point. Z is irrelevant for 2D physics (usually orthographic) so we take camera to get proper XY.
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
            RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, draggableLayer);

            if (hit2D.collider != null)
            {
                hitObj2D = hit2D.collider.gameObject;
            }

            // -------- 3D hover --------
            Ray ray = cam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, hoverLayer))
            {
                hitObj3D = hit.transform.gameObject;
            }

            // Decide which to use
            GameObject newHover = null;
            if (hitObj2D != null && hitObj3D != null)
            {
                newHover = prefer2DHover ? hitObj2D : hitObj3D;
            }
            else if (hitObj2D != null)
            {
                newHover = hitObj2D;
            }
            else if (hitObj3D != null)
            {
                newHover = hitObj3D;
            }

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

        private void OnClickPerformed(InputAction.CallbackContext ctx)
        {
            Vector2 screenPos = pointAction.ReadValue<Vector2>();
            // Debug.Log("Clicked" + screenPos + "");
            Ray ray = cam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, draggableLayer))
            {

                GrabbedObject = hit.transform;
                // define a plane orthogonal to camera through hit point
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

        private void OnClickReleased(InputAction.CallbackContext ctx)
        {
            if (GrabbedObject != null)
            {
                // You can implement drop logic here (e.g., snapping, checking valid drop zone)
                var clickable = GrabbedObject.gameObject.GetComponent<IWorldClickable>();
                clickable?.OnClickReleased();

                var grabbable = GrabbedObject.gameObject.GetComponent<IWorldDraggable>();
                if (grabbable == null) { return; }
                grabbable.DragReleased();

                GrabbedObject = null;

            }
        }

        [SerializeField] float dragDistanceFromCamera = 10f;

        void Update()
        {
            if (GrabbedObject != null)
            {
                if (!GrabbedObject.TryGetComponent<IWorldDraggable>(out var grabbable)) { return; }


                Vector2 screenPos = pointAction.ReadValue<Vector2>();
                Ray ray = cam.ScreenPointToRay(screenPos);

                if (Physics.Raycast(ray, out RaycastHit hit, dragDistanceFromCamera))
                {
                    dragPlane = new Plane(-cam.transform.forward, hit.point);
                }




                if (dragPlane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);

                    DebugMoveToPos = hitPoint + grabOffset;
                    grabbable.Drag(hitPoint + grabOffset);
                }
            }
        }
        Vector3 DebugMoveToPos { get; set; }
        Vector3 DebugMoveToPos2 { get; set; }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(DebugMoveToPos, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(DebugMoveToPos2, Vector3.one * 0.2f);

        }
    }
}