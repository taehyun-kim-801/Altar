using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    private Color color;
    private int alphaSign = -1;
    private Color[] blinkColor;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<Text>().color;
        blinkColor = new Color[2] { color, new Color(0f, 0f, 0f, 0f) };
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        int i = 0;
        while(true)
        {
            yield return new WaitForSecondsRealtime(0.7f);
            GetComponent<Text>().color = blinkColor[i++ % 2];
        }
    }
}
