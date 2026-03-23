using Laserbean.SpecialData;
using UnityEditor;
using UnityEngine;

public class CustomCursor : Singleton<CustomCursor>
{
    // Define the hotspot (the active point of the cursor, e.g., the tip of an arrow)

    // Choose between hardware or software cursor rendering
    public CursorMode cursorMode = CursorMode.Auto;

    public Texture2D defaultCursor;
    public Texture2D interactCursor;
    public Vector2 hotspot = Vector2.zero;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (EditorApplication.isPlaying)
        {
            SetCursor(defaultCursor);
        }
    }
#endif
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        SetCursor(defaultCursor);
    }

    public void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
    }

    [SerializeField] CustomDictionary<CursorType, Texture2D> texturedict = new();
    public void SetCursorByType(CursorType hoverCursorType)
    {
        if (texturedict.TryGetValue(hoverCursorType, out Texture2D texture2d))
        {
            SetCursor(texture2d);
        }
    }

    public void ResetCursor()
    {
        SetCursor(defaultCursor);
    }
}
