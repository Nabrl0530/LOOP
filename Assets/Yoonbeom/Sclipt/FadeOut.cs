using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour

{

    public float FadeTime = 2f; 

    public Image fadeImg;

    float start;

    float end;

    float time = 0f;

    bool isPlaying = false;



    void Awake()

    {
        OutStartFadeAnim();
    }

    public void OutStartFadeAnim()

    {

        if (isPlaying == true)

        {

            return;

        }

        start = 0f;

        end = 1f;

        StartCoroutine(fadeinplay());

    }



    IEnumerator fadeinplay()

    {

        isPlaying = true;



        Color fadecolor = fadeImg.color;

        time = 0f;

        fadecolor.a = Mathf.Lerp(start, end, time);



        while (fadecolor.a < 1f)

        {

            time += Time.deltaTime / FadeTime;

            fadecolor.a = Mathf.Lerp(start, end, time);

            fadeImg.color = fadecolor;

            yield return null;

        }

        isPlaying = false;

    }
}