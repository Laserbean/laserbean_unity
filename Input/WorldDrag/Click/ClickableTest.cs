using Laserbean.General;
using UnityEngine;

public class ClickableTest : MonoBehaviour, IWorldClickable
{
    public void OnClickDown()
    {
        Debug.Log(name + " clicked!");
    }

    public void OnClickUp()
    {
        Debug.Log(name + " released!");
    }
}
