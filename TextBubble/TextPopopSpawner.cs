using Laserbean.General;
using UnityEngine;

public class TextPopopSpawner : MonoBehaviour
{
    // MultiObjectPooler popupPoolers; //TODO 
    MiniObjectPooler popupPooler;
    [SerializeField] ObjectPoolItem textPopupPoolItem;

    void Start()
    {
        popupPooler = new MiniObjectPooler(textPopupPoolItem, this.transform);
    }

    GameObject NewPopup()
    {
        var cur_popup = popupPooler.GetPooledObject();
        // cur_popup.transform.position = transform.position;
        return cur_popup;
    }

    [EasyButtons.Button]
    public void SpawnTextTest()
    {
        var cur_popup = NewPopup();
        cur_popup.SetActive(true);
        cur_popup.GetComponent<TextPopupController>().StartPopup("Test", 1, Vector2.down * 3f);
    }

    public void SpawnText(int number)
    {
        var cur_popup = NewPopup();
        cur_popup.SetActive(true);
        cur_popup.GetComponent<TextPopupController>().StartPopup(number + "");
    }

    public void StartPopup(string text, float time = 1f)
    {
        var cur_popup = NewPopup();
        cur_popup.SetActive(true);
        cur_popup.GetComponent<TextPopupController>().StartPopup(text, time);
    }

    public void StartPopup(string text, float time, Vector2 force, ForceMode2D forceMode2D = ForceMode2D.Impulse)
    {
        var cur_popup = NewPopup();
        cur_popup.SetActive(true);
        cur_popup.GetComponent<TextPopupController>().StartPopup(text, time, force, forceMode2D);
    }

    public void StartPopup(PopupInfo popupInfo)
    {
        var cur_popup = NewPopup();
        cur_popup.SetActive(true);

        cur_popup.transform.position = popupInfo.Position; 
        cur_popup.GetComponent<TextPopupController>().StartPopup(popupInfo.Text, popupInfo.Lifetime, popupInfo.Force);
    }
}
