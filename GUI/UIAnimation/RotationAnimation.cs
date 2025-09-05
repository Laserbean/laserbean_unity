using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [Header("Rotation Animation Settings")]
    public Vector3 fromRotation = Vector3.zero;
    public Vector3 toRotation = new Vector3(0, 0, 360);
    public float duration = 1f;
    public bool loop = false;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private float timer = 0f;
    private bool animating = false;

    void OnEnable()
    {
        StartRotation();
    }

    public void StartRotation()
    {
        timer = 0f;
        animating = true;
    }

    void Update()
    {
        if (!animating) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);
        float curvedT = curve.Evaluate(t);

        Vector3 currentRotation = Vector3.LerpUnclamped(fromRotation, toRotation, curvedT);
        transform.localEulerAngles = currentRotation;

        if (t >= 1f)
        {
            if (loop)
            {
                timer = 0f;
            }
            else
            {
                animating = false;
            }
        }
    }
}