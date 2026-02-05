using Laserbean.General;
using UnityEngine;

public class ClickableTest : MonoBehaviour, IWorldClickable
{
    public void OnClickPressed()
    {
        Debug.Log(name + " clicked!");
    }

    public void OnClickReleased()
    {
        Debug.Log(name + " released!");
    }
}
