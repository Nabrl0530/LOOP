using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ArrowMover : MonoBehaviour
{
 [SerializeField]
 private int Velocity;
 private float xMove;

 private int WaitTime = 0;
 private int Step = 2;

    public float FadeTime = 2f;

    public Image fadeImg;
    public Image MenuImg;
    private float start;
    private float end;

    float time = 0f;
    private bool UseMenu = false;
    bool isPlaying = false;
    private bool ClearFade = false;
    private Color fadecolor;

    void Start()
    {
            start = 0f;
            end = 1f;
        
    }
    void Update()
    {
        

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Return) && Step != 1)
        {
            OutStartFadeAnim();

        }
        if (Input.GetKey(KeyCode.Return) && Step == 1 && WaitTime < 0)
        {
            if (!UseMenu)
            {
                UseMenu = true;
                MenuImg.gameObject.SetActive(true);
            }
            else
            {
                UseMenu = false;
                MenuImg.gameObject.SetActive(false);
            }
            WaitTime = 30;

        }

        CursorMove();
        ChoiceStage();
    }

    public void OutStartFadeAnim()

    {

        if (isPlaying == true)

        {

            return;

        }


        StartCoroutine(fadeinplay());


    }
    private void CursorMove()
    {
        xMove = 0;
        WaitTime--;
        if (Input.GetKey(KeyCode.LeftArrow) && WaitTime < 0 && Step > 1 && !UseMenu)
        {
            xMove = -Velocity * Time.deltaTime;
            WaitTime = 30;
            Step--;
        }
        if (Input.GetKey(KeyCode.RightArrow) && WaitTime < 0 && Step < 3 && !UseMenu)
        {
            xMove = Velocity * Time.deltaTime;
            WaitTime = 30;
            Step++;
        }

        this.transform.Translate(new Vector3(xMove, 0, 0));

    }
    private void ChoiceStage()
    {

        if (ClearFade)
        {
            switch (Step)
            {
                case 1:
                    //ƒƒjƒ…[

                    break;
                case 2:
                    SceneManager.LoadScene("yb_ChoiceScene");

                    break;
                case 3:
                    UnityEditor.EditorApplication.isPlaying = false;
                    break;
             
            }
        }
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
