using UnityEngine;
using System.Collections;
using TMPro;

public class Fps : MonoBehaviour
{
    public TMP_Text fpsText;
    public TMP_Text minxFpsText;
    private float count;
    private float minFps = 10000000;

    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            fpsText.text = count.ToString("0");
            minFps = Mathf.Min(count, minFps);
            minxFpsText.text = minFps.ToString("0");
            yield return new WaitForSeconds(0.1f);
        }
    }
}