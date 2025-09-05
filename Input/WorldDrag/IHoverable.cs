using UnityEngine;

public interface IHoverable
{
    public void Hover(Vector2 mouseLocation);
    public void OnHoverExit();
    public void OnHoverEnter();

}