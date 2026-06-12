using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using Laserbean.General;

namespace Laserbean.NewInput
{

    public class MouseInputs2 : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        [Header("Drag Settings")]
        [SerializeField] private float dragThreshold = 5f; // Minimum drag distance to trigger drag


        [Header("Input Actions")]
        [SerializeField] private InputActionAsset inputActionsAsset;

        [SerializeField] private InputActionReference PointAction;
        [SerializeField] private InputActionReference ClickAction;
        [SerializeField] private InputActionReference DoubleClickAction;
        [SerializeField] private InputActionReference HoldAction;


        private IMouseInputs mouseInputable;

        // Input state tracking
        private Vector2 currentScreenPos;
        private Vector2 dragStartPos;

        private bool isDragging;
        private bool isClickDown;


        #region Unity Events

        [Header("Point/Hover Events")]
        [SerializeField] private UnityEvent<Vector2> onPointMove = new();

        [Header("Left Click Events")]
        [SerializeField] private UnityEvent<Vector2> onClickDown = new();
        [SerializeField] private UnityEvent<Vector2> onClickUp = new();
        [SerializeField] private UnityEvent<Vector2> onDoubleClick = new();
        [SerializeField] private UnityEvent<Vector2> onDragStart = new();
        [SerializeField] private UnityEvent<Vector2> onDrag = new();
        [SerializeField] private UnityEvent<Vector2> onDragEnd = new();


        #endregion

        void Awake()
        {
            mouseInputable = GetComponent<IMouseInputs>();
            if (mouseInputable == null)
            {
                Debug.LogWarning("MouseInputs: No IMouseInputable2 component found on this GameObject.", gameObject);
            }

        }

        void OnEnable()
        {
            inputActionsAsset.Enable();
            InitializeInputActions();
            SubscribeToInputEvents();
        }

        void OnDisable()
        {
            inputActionsAsset.Disable();
            UnsubscribeFromInputEvents();
        }

        private void InitializeInputActions()
        {
            cam = cam != null ? cam : Camera.main;
        }

        private void SubscribeToInputEvents()
        {
            PointAction.action.performed += OnPointMoved;

            ClickAction.action.performed += OnClickPerformed;
            ClickAction.action.canceled += OnClickCanceled;
            DoubleClickAction.action.performed += OnDoubleClickPerformed;
            HoldAction.action.performed += OnHoldPerformed;
            HoldAction.action.canceled += OnHoldCanceled;

            // ClickAction.action.performed += _ => Debug.Log("OnLeftClickPerformed");

        }

        private void UnsubscribeFromInputEvents()
        {
            PointAction.action.performed -= OnPointMoved;

            ClickAction.action.performed -= OnClickPerformed;
            ClickAction.action.canceled -= OnClickCanceled;
            DoubleClickAction.action.performed -= OnDoubleClickPerformed;
            HoldAction.action.performed -= OnHoldPerformed;
            HoldAction.action.canceled -= OnHoldCanceled;
        }

        private void OnPointMoved(InputAction.CallbackContext ctx)
        {
            currentScreenPos = PointAction.action.ReadValue<Vector2>();
            onPointMove.Invoke(currentScreenPos);
            mouseInputable?.OnPointMove(currentScreenPos);
        }


        private void OnClickPerformed(InputAction.CallbackContext ctx)
        {
            currentScreenPos = PointAction.action.ReadValue<Vector2>();
            onClickDown.Invoke(currentScreenPos);
            mouseInputable?.OnClickDown(currentScreenPos);
            dragStartPos = currentScreenPos;
            isClickDown = true;
        }

        private void OnClickCanceled(InputAction.CallbackContext ctx)
        {
            currentScreenPos = PointAction.action.ReadValue<Vector2>();
            mouseInputable?.OnClickUp(currentScreenPos);
            onClickUp.Invoke(currentScreenPos);


            isDragging = false;
            isClickDown = false;

            mouseInputable?.OnDragEnd(currentScreenPos);
            onDragEnd.Invoke(currentScreenPos);
        }
        
        private void OnHoldPerformed(InputAction.CallbackContext context)
        {
            currentScreenPos = PointAction.action.ReadValue<Vector2>();
            mouseInputable?.OnHoldDown(currentScreenPos);
        }

        private void OnHoldCanceled(InputAction.CallbackContext context)
        {
            currentScreenPos = PointAction.action.ReadValue<Vector2>();
            mouseInputable?.OnHoldUp(currentScreenPos);
        }

        private void OnDoubleClickPerformed(InputAction.CallbackContext context)
        {
            onDoubleClick.Invoke(currentScreenPos);
            mouseInputable?.OnDoubleClick(currentScreenPos);
        }


        void Update()
        {
            // if (isDragging)
            // {
            HandleLeftDrag();
            // }
        }

        #region Drag Handling
        private void HandleLeftDrag()
        {
            float dragDistance = Vector2.Distance(currentScreenPos, dragStartPos);

            if (isClickDown && dragDistance >= dragThreshold)
            {
                // Debug.Log("Drag Start".DebugColor(Color.yellow));
                isDragging = true;
                onDragStart.Invoke(dragStartPos);
                mouseInputable?.OnDragStart(dragStartPos);
            }

            if (isDragging && isClickDown)
            {
                // Debug.Log("Is Dragging".DebugColor(Color.yellow));
                mouseInputable?.OnDrag(currentScreenPos);
                onDrag.Invoke(currentScreenPos);
            }
        }

        // private void HandleLeftDrag()
        // {
        //     mouseInputable?.OnLeftDrag(currentScreenPos);
        //     onDrag.Invoke(currentScreenPos);
        // }


        #endregion
    }
}
