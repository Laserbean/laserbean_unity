using Laserbean.General;
using UnityEngine;

public class ClickableTest : MonoBehaviour, IWorldClickable
{
    public void OnClickDown()
    {
        Debug.Log(name + " clicked!");
    }

    public void OnClickInterrupt()
    {
        Debug.Log(name + " interrupt!");

    }

    public void OnClickUp()
    {
        Debug.Log(name + " released!");
    }

    public void OnDoubleClick()
    {
        Debug.Log(name + " DOUBLE CLICK!");
    }
}
