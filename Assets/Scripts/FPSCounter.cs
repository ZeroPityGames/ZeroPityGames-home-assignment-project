using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TMP_Text TEXT;

    private float poolingTime = 1f;
    private float time;
    private float frameCount;

    private void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= poolingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            TEXT.text = frameCount.ToString() + " FPS";

            time -= poolingTime;
            frameCount = 0;
        }
    }

}
