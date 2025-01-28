using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnEnable : MonoBehaviour
{
    // Start is called before the first frame update

    void OnEnable()
    {
        GameManager.Instance.PauseGame();
    }

    void OnDisable()
    {
        GameManager.Instance.UnPauseGame();
    }
}
