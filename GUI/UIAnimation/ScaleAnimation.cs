using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [Header("Scale Animation Settings")]
    public Vector3 minScale = Vector3.one * 0.8f;
    public Vector3 maxScale = Vector3.one * 1.2f;
    public float period = 1f;

    private float timer = 0f;

    void Update()
    {
        if (period <= 0f) return;

        timer += Time.deltaTime;
        float t = (Mathf.Sin((timer / period) * Mathf.PI * 2f) + 1f) * 0.5f; // oscillates between 0 and 1
        transform.localScale = Vector3.Lerp(minScale, maxScale, t);
    }
}