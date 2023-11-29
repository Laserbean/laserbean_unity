using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Laserbean.General;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TMPro.TextMeshProUGUI>();
    }
    [SerializeField] string Prefix = "FPS: ";

    [SerializeField] float targetFps = 30;

    const int buffersize = 50;

    float[] deltaTimeBuffer = new float[buffersize];

    int currentframe = 0;
    private void Update()
    {

        deltaTimeBuffer[currentframe] = Time.unscaledDeltaTime;
        currentframe = (currentframe + 1) % buffersize;

        int curfps = CalculateFPS();

        Color curcolor;
        if (curfps >= 0.90 * targetFps) {
            curcolor = Color.green;
        }
        else if (curfps >= 0.8 * targetFps) {
            curcolor = new Color(1f, 0.5f, 0.016f, 1f);
        }
        else if (curfps >= 0.7 * targetFps) {
            curcolor = Color.yellow;
        }
        else {
            curcolor = Color.red;
        }

        textMeshPro.text = Prefix + curfps.ToString().DebugColor(curcolor);

    }

    int CalculateFPS()
    {
        float total = deltaTimeBuffer.Sum();
        return Mathf.RoundToInt(buffersize / total);
    }
}


