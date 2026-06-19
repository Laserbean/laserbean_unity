using UnityEngine;

public interface IWorldClickable
{
    public void OnClickDown();
    public void OnClickUp();
    public void OnClickInterrupt(); 

    public void OnDoubleClick();

    public void SetClickable(bool Clickable); 

}