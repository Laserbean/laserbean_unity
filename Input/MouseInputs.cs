using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Laserbean.Input
{
    /// <summary>
    /// Custom UnityEvent types for mouse input
    /// </summary>
    [System.Serializable]
    public class Vector2Event : UnityEvent<Vector2> { }

    [System.Serializable]
    public class Vector2Vector2Event : UnityEvent<Vector2, Vector2> { }

    /// <summary>
    /// Handles all mouse input events and delegates them to IMouseInputable2 components.
    /// Supports left, right, and middle mouse buttons, scroll wheel, hover detection, and drag operations.
    /// </summary>
    public class MouseInputs : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        
        [Header("Drag Settings")]
        [SerializeField] private float dragThreshold = 5f; // Minimum drag distance to trigger drag
        
        [Header("Double Click Settings")]
        [SerializeField] private float doubleClickTimeWindow = 0.3f; // Time window for double click detection

        private InputAction pointAction;
        private InputAction leftClickAction;
        private InputAction rightClickAction;
        private InputAction middleClickAction;
        private InputAction scrollAction;

        private IMouseInputable2 mouseInputable;

        // Input state tracking
        private Vector2 currentScreenPos;
        private Vector2 dragStartPos;

        private bool isLeftClickPressed;
        private bool isRightClickPressed;
        private bool isMiddleClickPressed;

        private bool isLeftDragging;
        private bool isRightDragging;
        private bool isMiddleDragging;

        private float lastLeftClickTime;
        private float lastRightClickTime;
        private float lastMiddleClickTime;

        #region Unity Events

        [Header("Point/Hover Events")]
        [SerializeField] private Vector2Event onPointMove = new Vector2Event();

        [Header("Left Click Events")]
        [SerializeField] private Vector2Event onLeftClickDown = new Vector2Event();
        [SerializeField] private Vector2Event onLeftClickUp = new Vector2Event();
        [SerializeField] private Vector2Event onLeftDoubleClick = new Vector2Event();
        [SerializeField] private Vector2Event onLeftDragStart = new Vector2Event();
        [SerializeField] private Vector2Event onLeftDrag = new Vector2Event();
        [SerializeField] private Vector2Event onLeftDragEnd = new Vector2Event();
        [SerializeField] private UnityEvent onLeftClickSuccessful = new UnityEvent();

        [Header("Right Click Events")]
        [SerializeField] private Vector2Event onRightClickDown = new Vector2Event();
        [SerializeField] private Vector2Event onRightClickUp = new Vector2Event();
        [SerializeField] private Vector2Event onRightDoubleClick = new Vector2Event();
        [SerializeField] private Vector2Event onRightDragStart = new Vector2Event();
        [SerializeField] private Vector2Event onRightDrag = new Vector2Event();
        [SerializeField] private Vector2Event onRightDragEnd = new Vector2Event();
        [SerializeField] private UnityEvent onRightClickSuccessful = new UnityEvent();

        [Header("Middle Click Events")]
        [SerializeField] private Vector2Event onMiddleClickDown = new Vector2Event();
        [SerializeField] private Vector2Event onMiddleClickUp = new Vector2Event();
        [SerializeField] private Vector2Event onMiddleDoubleClick = new Vector2Event();
        [SerializeField] private Vector2Event onMiddleDragStart = new Vector2Event();
        [SerializeField] private Vector2Event onMiddleDrag = new Vector2Event();
        [SerializeField] private Vector2Event onMiddleDragEnd = new Vector2Event();
        [SerializeField] private UnityEvent onMiddleClickSuccessful = new UnityEvent();

        [Header("Scroll Events")]
        [SerializeField] private Vector2Vector2Event onScroll = new Vector2Vector2Event();

        #endregion

        void Awake()
        {
            mouseInputable = GetComponent<IMouseInputable2>();
            if (mouseInputable == null)
            {
                Debug.LogWarning("MouseInputs: No IMouseInputable2 component found on this GameObject.", gameObject);
            }
        }

        void OnEnable()
        {
            InitializeInputActions();
            SubscribeToInputEvents();
        }

        void OnDisable()
        {
            UnsubscribeFromInputEvents();
            DisableInputActions();
        }

        private void InitializeInputActions()
        {
            cam = cam != null ? cam : Camera.main;

            // Point/Position tracking
            pointAction = new InputAction("point", binding: "<Pointer>/position");
            pointAction.Enable();

            // Mouse buttons
            leftClickAction = new InputAction("leftClick", binding: "<Mouse>/leftButton");
            leftClickAction.AddBinding("<Touchscreen>/touch*/press");
            leftClickAction.Enable();

            rightClickAction = new InputAction("rightClick", binding: "<Mouse>/rightButton");
            rightClickAction.Enable();

            middleClickAction = new InputAction("middleClick", binding: "<Mouse>/middleButton");
            middleClickAction.Enable();

            // Scroll wheel
            scrollAction = new InputAction("scroll", binding: "<Mouse>/scroll");
            scrollAction.Enable();
        }

        private void SubscribeToInputEvents()
        {
            // Point/Hover tracking
            pointAction.performed += OnPointMoved;

            // Left click
            leftClickAction.performed += OnLeftClickPerformed;
            leftClickAction.canceled += OnLeftClickReleased;

            // Right click
            rightClickAction.performed += OnRightClickPerformed;
            rightClickAction.canceled += OnRightClickReleased;

            // Middle click
            middleClickAction.performed += OnMiddleClickPerformed;
            middleClickAction.canceled += OnMiddleClickReleased;

            // Scroll wheel
            scrollAction.performed += OnScroll;
        }

        private void UnsubscribeFromInputEvents()
        {
            pointAction.performed -= OnPointMoved;

            leftClickAction.performed -= OnLeftClickPerformed;
            leftClickAction.canceled -= OnLeftClickReleased;

            rightClickAction.performed -= OnRightClickPerformed;
            rightClickAction.canceled -= OnRightClickReleased;

            middleClickAction.performed -= OnMiddleClickPerformed;
            middleClickAction.canceled -= OnMiddleClickReleased;

            scrollAction.performed -= OnScroll;
        }

        private void DisableInputActions()
        {
            pointAction?.Disable();
            leftClickAction?.Disable();
            rightClickAction?.Disable();
            middleClickAction?.Disable();
            scrollAction?.Disable();
        }

        private void OnPointMoved(InputAction.CallbackContext ctx)
        {
            currentScreenPos = pointAction.ReadValue<Vector2>();
            onPointMove.Invoke(currentScreenPos);
            mouseInputable?.OnPointMove(currentScreenPos);
        }

        #region Left Click

        private void OnLeftClickPerformed(InputAction.CallbackContext ctx)
        {
            currentScreenPos = pointAction.ReadValue<Vector2>();
            dragStartPos = currentScreenPos;
            isLeftClickPressed = true;
            isLeftDragging = false;

            // Check for double click
            float timeSinceLastClick = Time.time - lastLeftClickTime;
            if (timeSinceLastClick <= doubleClickTimeWindow)
            {
                bool doubleClickHandled = mouseInputable?.OnLeftDoubleClick(currentScreenPos) ?? false;
                if (doubleClickHandled)
                {
                    onLeftDoubleClick.Invoke(currentScreenPos);
                    lastLeftClickTime = 0; // Reset to prevent triple clicks
                    return;
                }
            }

            lastLeftClickTime = Time.time;
            bool clickHandled = mouseInputable?.OnLeftClickDown(currentScreenPos) ?? false;
            
            if (clickHandled)
            {
                onLeftClickDown.Invoke(currentScreenPos);
                onLeftClickSuccessful.Invoke();
            }
            else
            {
                isLeftClickPressed = false;
            }
        }

        private void OnLeftClickReleased(InputAction.CallbackContext ctx)
        {
            currentScreenPos = pointAction.ReadValue<Vector2>();
            isLeftClickPressed = false;

            if (isLeftDragging)
            {
                mouseInputable?.OnLeftDragEnd(currentScreenPos);
                onLeftDragEnd.Invoke(currentScreenPos);
                isLeftDragging = false;
            }

            mouseInputable?.OnLeftClickUp(currentScreenPos);
            onLeftClickUp.Invoke(currentScreenPos);
        }

        #endregion

        #region Right Click

        private void OnRightClickPerformed(InputAction.CallbackContext ctx)
        {
            currentScreenPos = pointAction.ReadValue<Vector2>();
            dragStartPos = currentScreenPos;
            isRightClickPressed = true;
            isRightDragging = false;

            // Check for double click
            float timeSinceLastClick = Time.time - lastRightClickTime;
            if (timeSinceLastClick <= doubleClickTimeWindow)
            {
                bool doubleClickHandled = mouseInputable?.OnRightDoubleClick(currentScreenPos) ?? false;
                if (doubleClickHandled)
                {
                    onRightDoubleClick.Invoke(currentScreenPos);
                    lastRightClickTime = 0; // Reset to prevent triple clicks
                    return;
                }
            }

            lastRightClickTime = Time.time;
            bool clickHandled = mouseInputable?.OnRightClickDown(currentScreenPos) ?? false;
            
            if (clickHandled)
            {
                onRightClickDown.Invoke(currentScreenPos);
                onRightClickSuccessful.Invoke();
            }
            else
            {
                isRightClickPressed = false;
            }
        }

        private void OnRightClickReleased(InputAction.CallbackContext ctx)
        {
            currentScreenPos = pointAction.ReadValue<Vector2>();
            isRightClickPressed = false;

            if (isRightDragging)
            {
                mouseInputable?.OnRightDragEnd(currentScreenPos);
                onRightDragEnd.Invoke(currentScreenPos);
                isRightDragging = false;
            }

            mouseInputable?.OnRightClickUp(currentScreenPos);
            onRightClickUp.Invoke(currentScreenPos);
        }

        #endregion

        #region Middle Click

        private void OnMiddleClickPerformed(InputAction.CallbackContext ctx)
        {
            currentScreenPos = pointAction.ReadValue<Vector2>();
            dragStartPos = currentScreenPos;
            isMiddleClickPressed = true;
            isMiddleDragging = false;

            // Check for double click
            float timeSinceLastClick = Time.time - lastMiddleClickTime;
            if (timeSinceLastClick <= doubleClickTimeWindow)
            {
                bool doubleClickHandled = mouseInputable?.OnMiddleDoubleClick(currentScreenPos) ?? false;
                if (doubleClickHandled)
                {
                    onMiddleDoubleClick.Invoke(currentScreenPos);
                    lastMiddleClickTime = 0; // Reset to prevent triple clicks
                    return;
                }
            }

            lastMiddleClickTime = Time.time;
            bool clickHandled = mouseInputable?.OnMiddleClickDown(currentScreenPos) ?? false;
            
            if (clickHandled)
            {
                onMiddleClickDown.Invoke(currentScreenPos);
                onMiddleClickSuccessful.Invoke();
            }
            else
            {
                isMiddleClickPressed = false;
            }
        }

        private void OnMiddleClickReleased(InputAction.CallbackContext ctx)
        {
            currentScreenPos = pointAction.ReadValue<Vector2>();
            isMiddleClickPressed = false;

            if (isMiddleDragging)
            {
                mouseInputable?.OnMiddleDragEnd(currentScreenPos);
                onMiddleDragEnd.Invoke(currentScreenPos);
                isMiddleDragging = false;
            }

            mouseInputable?.OnMiddleClickUp(currentScreenPos);
            onMiddleClickUp.Invoke(currentScreenPos);
        }

        #endregion

        #region Scroll

        private void OnScroll(InputAction.CallbackContext ctx)
        {
            Vector2 scrollDelta = scrollAction.ReadValue<Vector2>();
            currentScreenPos = pointAction.ReadValue<Vector2>();
            onScroll.Invoke(scrollDelta, currentScreenPos);
            mouseInputable?.OnScroll(scrollDelta, currentScreenPos);
        }

        #endregion

        void Update()
        {
            if (isLeftClickPressed)
            {
                HandleLeftDrag();
            }

            if (isRightClickPressed)
            {
                HandleRightDrag();
            }

            if (isMiddleClickPressed)
            {
                HandleMiddleDrag();
            }
        }

        #region Drag Handling

        private void HandleLeftDrag()
        {
            float dragDistance = Vector2.Distance(currentScreenPos, dragStartPos);

            if (!isLeftDragging && dragDistance >= dragThreshold)
            {
                isLeftDragging = true;
                bool dragStartHandled = mouseInputable?.OnLeftDragStart(dragStartPos) ?? false;
                if (dragStartHandled)
                {
                    onLeftDragStart.Invoke(dragStartPos);
                }
                else
                {
                    isLeftDragging = false;
                    return;
                }
            }

            if (isLeftDragging)
            {
                mouseInputable?.OnLeftDrag(currentScreenPos);
                onLeftDrag.Invoke(currentScreenPos);
            }
        }

        private void HandleRightDrag()
        {
            float dragDistance = Vector2.Distance(currentScreenPos, dragStartPos);

            if (!isRightDragging && dragDistance >= dragThreshold)
            {
                isRightDragging = true;
                bool dragStartHandled = mouseInputable?.OnRightDragStart(dragStartPos) ?? false;
                if (dragStartHandled)
                {
                    onRightDragStart.Invoke(dragStartPos);
                }
                else
                {
                    isRightDragging = false;
                    return;
                }
            }

            if (isRightDragging)
            {
                mouseInputable?.OnRightDrag(currentScreenPos);
                onRightDrag.Invoke(currentScreenPos);
            }
        }

        private void HandleMiddleDrag()
        {
            float dragDistance = Vector2.Distance(currentScreenPos, dragStartPos);

            if (!isMiddleDragging && dragDistance >= dragThreshold)
            {
                isMiddleDragging = true;
                bool dragStartHandled = mouseInputable?.OnMiddleDragStart(dragStartPos) ?? false;
                if (dragStartHandled)
                {
                    onMiddleDragStart.Invoke(dragStartPos);
                }
                else
                {
                    isMiddleDragging = false;
                    return;
                }
            }

            if (isMiddleDragging)
            {
                mouseInputable?.OnMiddleDrag(currentScreenPos);
                onMiddleDrag.Invoke(currentScreenPos);
            }
        }

        #endregion
    }
}
