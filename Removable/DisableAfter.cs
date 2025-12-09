using System.Collections;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
    [SerializeField] bool StartOnEnable = false;
    [SerializeField] float DisableAfterTime = 3f;

    private void OnEnable()
    {
        if (StartOnEnable)
        {
            StartCoroutine(DisableAfterSeconds(DisableAfterTime));
        }
    }

    public void StartDisableAfter()
    {
        StartCoroutine(DisableAfterSeconds(DisableAfterTime));
    }

    public void StartDisableAfter(float seconds)
    {
        StartCoroutine(DisableAfterSeconds(seconds));
    }

    IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
