using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text text;
    long timer;
    float duration = 1f;
    float time = 0f;

    void Awake()
    {
        timer = (1000 * 60) * 30;
        text = GetComponent<Text>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= duration)
        {
            time = 0f;
            timer -= 1000;
            text.text = "00:" + ((timer / 1000) / 60).ToString("00") + ":" + ((timer / 1000) % 60).ToString("00");
        }
    }
}