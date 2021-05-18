using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ArrowMover : MonoBehaviour
{
 [SerializeField]
 private int Velocity;
 private float yMove;

 private int WaitTime = 0;
 private int Step = 1;
    private bool ClearFade = false;
    bool isPlaying = false;
    public Image fadeImg;
    private float start;
    private float end;
    private Color fadecolor;
    public float FadeTime = 2f;



    float time = 0f;


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
        if (ClearFade && Step == 3)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (ClearFade && Step == 1)
        {
           SceneManager.LoadScene("yb_ChoiceScene");
        }

        CursorMove();
        ChoiceStage();
    }

   
    private void CursorMove()
    {
        yMove = 0;
        WaitTime--;
        if (Input.GetKey(KeyCode.DownArrow) && WaitTime < 0 && Step < 3)
        {
            yMove = -Velocity * Time.deltaTime;
            WaitTime = 30;
            Step++;
        }
        if (Input.GetKey(KeyCode.UpArrow) && WaitTime < 0 && Step > 1)
        {
            yMove = Velocity * Time.deltaTime;
            WaitTime = 30;
            Step--;
        }

        this.transform.Translate(new Vector3(0, yMove, 0));

    }
    private void ChoiceStage()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            switch (Step)
            {
                case 1:
                    OutStartFadeAnim();
                    break;
                case 2:
                    OutStartFadeAnim();

                    //ÉÅÉjÉÖÅ[âÊñ 
                    break;
                case 3:
                    OutStartFadeAnim();
                    break;

            }
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
        fadecolor.a = 1.0f;

        ClearFade = true;
        isPlaying = false;

    }
}
