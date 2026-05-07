#if UNITY_EDITOR
using UnityEngine;
// // idk how this works
public class RichTextGUI : MonoBehaviour
{
    [EasyButtons.Button]
    public void SetGUIStyle()
    {
        GUIStyle style = new GUIStyle();
        style.richText = true;
        GUILayout.Label("<size=30>Some <color=yellow>RICH</color> text</size>", style);
    }
}
#endif