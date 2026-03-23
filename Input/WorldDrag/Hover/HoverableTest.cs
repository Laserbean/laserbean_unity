using Laserbean.General;
using UnityEngine;

public class HoverableTest : MonoBehaviour, IHoverable
{
    public CursorType GetCursorType()
    {
        throw new System.NotImplementedException();
    }

    public void Hover(Vector2 mouseLocation)
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverEnter()
    {
        Debug.Log("Hover Enter!".DebugColor(Color.green));
    }

    public void OnHoverExit()
    {
        Debug.Log("Hover Exit!".DebugColor(Color.red));
    }


}
