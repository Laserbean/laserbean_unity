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

    const int buffersize = 50;

    float[] deltaTimeBuffer = new float[buffersize];

    int currentframe = 0;
    private void Update()
    {

        deltaTimeBuffer[currentframe] = Time.unscaledDeltaTime;
        currentframe = (currentframe + 1) % buffersize;

        int curfps = CalculateFPS();

        Color curcolor;
        if (curfps >= 55) {
            curcolor = Color.green;
        }
        else if (curfps >= 25) {
            curcolor = Color.yellow;
        }
        else {
            curcolor = Color.red;
        }

        textMeshPro.text = "FPS : " + curfps.ToString().DebugColor(curcolor);

    }

    int CalculateFPS()
    {
        float total = deltaTimeBuffer.Sum();
        return Mathf.RoundToInt(buffersize / total);
    }
}


