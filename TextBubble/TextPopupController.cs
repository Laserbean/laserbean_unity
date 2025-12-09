using Laserbean.Removable;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextPopupController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI textMeshPro;


    Rigidbody2D rb;
    RemoveAfter disableAfter;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        disableAfter = GetComponent<RemoveAfter>();
    }

    public void StartPopup(string text, float time = 1f)
    {
        textMeshPro.text = text;
        disableAfter.StartRemoveAfter(time);
    }

    public void StartPopup(string text, float time, Vector2 force, ForceMode2D forceMode2D = ForceMode2D.Impulse)
    {
        textMeshPro.text = text;
        disableAfter.StartRemoveAfter(time);
        rb.AddForce(force, forceMode2D);
    }

    public void StartPopup(PopupInfo popupInfo, ForceMode2D forceMode2D = ForceMode2D.Impulse)
    {
        rb.position = popupInfo.Position; 
        textMeshPro.text = popupInfo.Text;
        disableAfter.StartRemoveAfter(popupInfo.Lifetime);
        rb.AddForce(popupInfo.Force, forceMode2D);
    }

}
