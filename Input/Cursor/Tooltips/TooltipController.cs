using System;
using Laserbean.General;
using Laserbean.Input;
using UnityEngine;

public class TooltipController : MonoBehaviour, IMouseInputable2
{
    [SerializeField] TMPro.TMP_Text textComponent;
    [SerializeField] TMPro.TMP_Text textComponentBG;

    [SerializeField] float showToolTipTimer = 0.5f;
    [SerializeField] float disableToolTipDistance = 0.5f;
    [SerializeField] Color highlightColor;

    void Start()
    {
        toolTipCountdownTimer = new(showToolTipTimer);
        toolTipCountdownTimer.OnTimerStop = ShowToolTip;
    }


    public void UpdateText(string newText)
    {
        if (textComponent != null)
        {
            textComponent.text = newText;
            textComponentBG.text = newText.RichFormat("mark", highlightColor.ColorToHexTransparency()); 
        }
    }

    public void ResetTooltipTimer()
    {
        toolTipCountdownTimer.Reset();
    }

    CountdownTimer toolTipCountdownTimer;

    Vector3 previousPosition = Vector3.zero;

    public void FixedUpdate()
    {
        toolTipCountdownTimer.Tick(Time.fixedDeltaTime);
    }

    void Update()
    {
        if (previousPosition == transform.position)
        {
            if (!toolTipCountdownTimer.IsRunning)
            {
                toolTipCountdownTimer.Reset();
                toolTipCountdownTimer.Start();
            }
        }
        else if ((previousPosition - transform.position).sqrMagnitude > disableToolTipDistance * disableToolTipDistance)
        {
            UpdateText("");
        }
        previousPosition = transform.position;
    }

    private void ShowToolTip()
    {
        var toolTipable = Raycast2D()?.GetComponent<IToolTipable>();
        if (toolTipable != null)
        {
            UpdateText(toolTipable.GetToolTip());
        }
        else
        {
            UpdateText("");
        }
    }

    private GameObject Raycast2D()
    {
        // Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));
        var worldPoint = transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity);

        if (hit2D.collider != null)
        {
            Debug.DrawLine(hit2D.point, hit2D.point + Vector2.up, Color.red);
            return hit2D.collider.gameObject;
        }

        return null;
    }

    public void OnPointMove(Vector2 ScreenPoint)
    {
    }

    public void OnLeftClickDown(Vector2 ScreenPoint)
    {
        ResetTooltipTimer();
        UpdateText("");
    }

    public void OnLeftClickUp(Vector2 ScreenPoint)
    {

    }

    public void OnLeftDragStart(Vector2 ScreenPoint)
    {

    }

    public void OnLeftDrag(Vector2 ScreenPoint)
    {

    }

    public void OnLeftDragEnd(Vector2 ScreenPoint)
    {

    }

    public void OnLeftDoubleClick(Vector2 ScreenPoint)
    {

    }

    public void OnRightClickDown(Vector2 ScreenPoint)
    {

    }

    public void OnRightClickUp(Vector2 ScreenPoint)
    {

    }

    public void OnRightDragStart(Vector2 ScreenPoint)
    {

    }

    public void OnRightDrag(Vector2 ScreenPoint)
    {

    }

    public void OnRightDragEnd(Vector2 ScreenPoint)
    {

    }

    public void OnRightDoubleClick(Vector2 ScreenPoint)
    {

    }

    public void OnMiddleClickDown(Vector2 ScreenPoint)
    {

    }

    public void OnMiddleClickUp(Vector2 ScreenPoint)
    {

    }

    public void OnMiddleDragStart(Vector2 ScreenPoint)
    {

    }

    public void OnMiddleDrag(Vector2 ScreenPoint)
    {

    }

    public void OnMiddleDragEnd(Vector2 ScreenPoint)
    {

    }

    public void OnMiddleDoubleClick(Vector2 ScreenPoint)
    {

    }

    public void OnScroll(Vector2 ScrollDelta, Vector2 ScreenPoint)
    {

    }
}
