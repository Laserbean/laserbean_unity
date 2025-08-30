using UnityEngine;
using UnityEngine.InputSystem;

namespace Laserbean.Input
{

    // [RequireComponent(typeof(IMouseInputable))]
    public class MouseInputHandler : MonoBehaviour
    {
        [SerializeField] private Camera cam; // assign or default to Camera.main


        private InputAction clickAction;
        private InputAction pointAction;

        IMouseInputable mouseInputable;

        void Awake()
        {
            mouseInputable = GetComponent<IMouseInputable>();
        }

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

        private void OnPointMoved(InputAction.CallbackContext ctx)
        {
            Vector2 screenPos = pointAction.ReadValue<Vector2>();
            CurDragPos = screenPos;

            mouseInputable?.OnPointMove(screenPos);
            // Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
            // RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, draggableLayer);

        }

        bool IsPointerDown { get; set; }
        Vector2 CurDragPos { get; set; }

        private void OnClickPerformed(InputAction.CallbackContext ctx)
        {
            Vector2 screenPos = pointAction.ReadValue<Vector2>();
            mouseInputable?.OnClickDown(screenPos);
            IsPointerDown = true;
        }

        private void OnClickReleased(InputAction.CallbackContext ctx)
        {
            Vector2 screenPos = pointAction.ReadValue<Vector2>();
            mouseInputable?.OnClickUp(screenPos);
            IsPointerDown = false;
        }

        [SerializeField] float dragDistanceFromCamera = 10f;


        void Update()
        {
            if (IsPointerDown)
            {
                mouseInputable?.OnDrag(CurDragPos);
            }

        }


        // void OnDrawGizmos()
        // {
        //     // Gizmos.color = Color.green;
        //     // Gizmos.DrawWireSphere(DebugMoveToPos, 0.2f);
        //     // Gizmos.color = Color.red;
        //     // Gizmos.DrawCube(DebugMoveToPos2, Vector3.one * 0.2f);

        // }
    }
}
