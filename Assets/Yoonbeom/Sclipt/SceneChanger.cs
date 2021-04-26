using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public float FadeTime = 2f;

    public Image fadeImg;
    private float start;
    private float end;

    float time = 0f;
 
    bool isPlaying = false;
   private bool ClearFade = false;
    private Color fadecolor;
    void Start()
    {
        start = 0f;
        end = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            OutStartFadeAnim();

        }

        if (ClearFade)
        {
            SceneManager.LoadScene("yb_ChoiceScene");
        }
    }


 

    public void OutStartFadeAnim()

    {

        if (isPlaying == true)

        {

            return;

        }


        StartCoroutine(fadeinplay());


    }



    IEnumerator fadeinplay()

    {

        isPlaying = true;



        fadecolor = fadeImg.color;

        time = 0f;

        fadecolor.a = Mathf.Lerp(start, end, time);


        while (fadecolor.a <= 0.99f)

        {

            time += Time.deltaTime / FadeTime;

            fadecolor.a = Mathf.Lerp(start, end, time);

            fadeImg.color = fadecolor;

            yield return null;

        }
        ClearFade = true;
       isPlaying = false;

    }
}
