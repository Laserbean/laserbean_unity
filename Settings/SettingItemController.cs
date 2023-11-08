using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingItemController : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI label; 
    [SerializeField] TMPro.TextMeshProUGUI description; 
    [SerializeField] GameObject toggle; 
    [SerializeField] GameObject textinput; 
    [SerializeField] GameObject slider; 

    string Name;
    TypeType type;

    bool init = false; 

    public void Init(string _name, TypeType _type, string dispname = "", string desc = "") {
        Name = _name; 
        type = _type; 
        label.text = dispname; 
        description.text = desc;

        switch(type) {
            case TypeType._int:
                textinput.GetComponent<TMPro.TMP_InputField>().contentType = TMPro.TMP_InputField.ContentType.IntegerNumber; 
                textinput.SetActive(true); 
                textinput.GetComponent<TMPro.TMP_InputField>().text = "" + (SettingsManager.Instance.GetInt(_name)); 
                textinput.GetComponent<TMPro.TMP_InputField>().interactable = false;  

            break;
            case TypeType._bool:
                toggle.SetActive(true); 
                toggle.GetComponent<Toggle>().isOn = SettingsManager.Instance.GetBool(_name);
            break;
            case TypeType._float:
                // slider.SetActive(true); 
                // slider.
                textinput.GetComponent<TMPro.TMP_InputField>().contentType = TMPro.TMP_InputField.ContentType.DecimalNumber; 
                textinput.GetComponent<TMPro.TMP_InputField>().text = "" + (SettingsManager.Instance.GetFloat(_name)); 
            break;

            case TypeType._string:
                textinput.SetActive(true); 
                textinput.GetComponent<TMPro.TMP_InputField>().text = "" + (SettingsManager.Instance.GetString(_name));
                textinput.GetComponent<TMPro.TMP_InputField>().interactable = false;  

            break;
        }
        init = true; 
    }

    public void OnBoolChanged(bool fish) {
        if (!init) {
            return;
        }
        SettingsManager.Instance.UpdateSetting(Name, fish); 
        Debug.Log(fish); 
    }

    public void OnStringDeselect(string fish) {
        if (!init) {
            return;
        }
        if (type == TypeType._string) {
            SettingsManager.Instance.UpdateSetting(Name, fish); 
        } else if (type == TypeType._int){
            SettingsManager.Instance.UpdateSetting(Name, int.Parse(fish)); 
        } else if (type == TypeType._float){
            SettingsManager.Instance.UpdateSetting(Name, float.Parse(fish)); 
        }
        Debug.Log(fish);       
    }

    public void OnSliderChange(float fish) {
        if (!init) {
            return;
        }
        SettingsManager.Instance.UpdateSetting(Name, fish);        
        Debug.Log(fish);       
    }
}
