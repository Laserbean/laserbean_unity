using Laserbean.Input;
using Laserbean.Input.WorldDrag;
using UnityEngine;

public class HandTool2D : MonoBehaviour, IMouseInputable2
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask draggableLayer;
    [SerializeField] private LayerMask hoverLayer;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private float dragDistance = 40f;
    [SerializeField] private bool use2D = true;
    [SerializeField] private float floatDistance = 20f;

    private Transform grabbedObject;

    [SerializeField] private Transform clickedObject;
    private Vector3 grabOffset;
    // private Plane dragPlane;
    // private GameObject lastHoverObject;

    [ShowOnly] private Vector2 currentScreenPos;
    // private bool isCurrentlyDragging;

    [SerializeField] bool showDebug = false;

    [SerializeField] private TransformEvent onLeftClickDown = new TransformEvent();
    [SerializeField] private TransformEvent onLeftClickUp = new TransformEvent();
    [SerializeField] private TransformEvent onLeftDragStart = new TransformEvent();
    [SerializeField] private TransformEvent onLeftDrag = new TransformEvent();
    [SerializeField] private TransformEvent onLeftDragEnd = new TransformEvent();
    // [SerializeField] private UnityEvent onLeftClickSuccessful = new UnityEvent();


    [SerializeField] GameObject HandTool;
    AnchoredJoint2D _HandToolJoint;
    AnchoredJoint2D HandToolJoint
    {
        get
        {
            if (_HandToolJoint == null)
            {
                _HandToolJoint = HandTool.GetComponent<AnchoredJoint2D>();
            }
            return _HandToolJoint;
        }
    }

    private bool AttemptGrab(Vector2 screenPos)
    {
        if (grabbedObject != null)
        {
            return false; // Already grabbing something
        }


        return AttemptGrab2D(screenPos);

    }

    private bool AttemptGrab2D(Vector2 screenPos)
    {
        Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
        RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, draggableLayer);

        if (hit2D.collider != null)
        {
            grabbedObject = hit2D.transform;
            // dragPlane = new Plane(-cam.transform.forward, hit2D.point);
            grabOffset = grabbedObject.position - (Vector3)hit2D.point;

            var draggable = hit2D.collider.GetComponent<HandDraggable>();
            if (draggable != null)
            {
                // draggable?.OnClickPressed();
                HandToolJoint.connectedBody = hit2D.transform.GetComponent<Rigidbody2D>();
                // HandToolJoint.connectedAnchor = hit2D.point;
                HandToolJoint.enabled = true;

                onLeftClickDown?.Invoke(hit2D.transform);
                clickedObject = hit2D.transform;
            }

            DebugLog($"[Grab] Grabbed 2D object: {grabbedObject.name}");

            return true;
        }
        return false;
    }
    private void ReleaseGrab()
    {
        if (grabbedObject != null)
        {
            // var clickable = grabbedObject.GetComponent<IWorldClickable>();
            // clickable?.OnClickReleased();

            HandToolJoint.connectedBody = null;
            HandToolJoint.connectedAnchor = Vector3.zero;
            HandToolJoint.enabled = false;

            onLeftDragEnd?.Invoke(grabbedObject);

            DebugLog($"[Release] Released: {grabbedObject.name}");
            grabbedObject = null;
        }
    }


    void OnDrawGizmos()
    {
        if (grabbedObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(grabbedObject.position, 0.2f);
        }
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

    #region fish
    public bool OnLeftClickDown(Vector2 ScreenPoint)
    {
        currentScreenPos = ScreenPoint;
        return AttemptGrab(ScreenPoint);
    }

    #endregion

    public void OnLeftClickUp(Vector2 ScreenPoint)
    {
        currentScreenPos = ScreenPoint;
        ReleaseGrab();
        onLeftClickUp?.Invoke(clickedObject);
        clickedObject = null;
    }

    public bool OnLeftDoubleClick(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
        return false;
    }

    public void OnLeftDrag(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }

    public void OnLeftDragEnd(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }

    public bool OnLeftDragStart(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
        return false;

    }

    public bool OnMiddleClickDown(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
        return false;
    }

    public void OnMiddleClickUp(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }

    public bool OnMiddleDoubleClick(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
        return false;
    }

    public void OnMiddleDrag(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }

    public void OnMiddleDragEnd(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }

    public bool OnMiddleDragStart(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
        return false;
    }

    public void OnPointMove(Vector2 ScreenPoint)
    {
        // // throw new System.NotImplementedException();
    }

    public bool OnRightClickDown(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
        return false;
    }

    public void OnRightClickUp(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }

    public bool OnRightDoubleClick(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
        return false;
    }

    public void OnRightDrag(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }

    public void OnRightDragEnd(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }

    public bool OnRightDragStart(Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
        return false;
    }

    public void OnScroll(Vector2 ScrollDelta, Vector2 ScreenPoint)
    {
        // throw new System.NotImplementedException();
    }
}
