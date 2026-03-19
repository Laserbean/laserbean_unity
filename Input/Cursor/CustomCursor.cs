using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    // Assign your custom cursor texture in the Unity Inspector
    public Texture2D cursorTexture;
    
    // Define the hotspot (the active point of the cursor, e.g., the tip of an arrow)
    public Vector2 hotSpot = Vector2.zero; 
    
    // Choose between hardware or software cursor rendering
    public CursorMode cursorMode = CursorMode.Auto; 

    void Start()
    {
        // Call SetCursor when the game starts
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
