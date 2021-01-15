using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlickingText : MonoBehaviour
{
    public float fadeStep = 0.2f;

    private Text text;
    private bool active = false;

    void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (!active)
        {
            StartCoroutine(FadeOut(FadeIn()));
        }
    }

    private IEnumerator FadeOut(IEnumerator toDo)
    {
        active = true;
        for (float alphaValue = 1f; alphaValue >= 0; alphaValue -= fadeStep)
        {
            Color c = text.color;
            c.a = alphaValue;
            text.color = c;
            yield return new WaitForSeconds(.1f);
        }
        StartCoroutine(toDo);
    }

    private IEnumerator FadeIn()
    {
        for (float alphaValue = 0f; alphaValue <= 1; alphaValue += fadeStep)
        {
            Color c = text.color;
            c.a = alphaValue;
            text.color = c;
            yield return new WaitForSeconds(.1f);
        }
        active = false;
    }
}
