using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CursorMover : MonoBehaviour
{

    public Image Stage1Img;
    public Image Stage2Img;
    public Image Stage3Img;
    public Image UpArrowImg;
    public Image DownArrowImg;

    [SerializeField]
 private int Velocity;
 private float yMove;

 private int WaitTime = 0;
 private int Step = 1;

    public float FadeTime = 2f;

    public Image fadeImg;
    private float start;
    private float end;

    float time = 0f;

    bool isPlaying = false;
    private bool ClearFade = false;
    private Color fadecolor;

    private float UpArrowScale = 1.0f;
    private float DownArrowScale = 1.0f;

    private int ArrowDirection = 0;

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
        if (Input.GetKey(KeyCode.Return))
        {
            OutStartFadeAnim();

        }

        CursorMove();
        ChoiceStage();
        switch(Step)
        {
            case 1:
                Stage1Img.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Stage2Img.gameObject.transform.localScale = new Vector3(1,1,1);
                Stage3Img.gameObject.transform.localScale = new Vector3(1,1,1);

                break;
            case 2:
                Stage2Img.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Stage1Img.gameObject.transform.localScale = new Vector3(1, 1, 1);
                Stage3Img.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 3:
                Stage3Img.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Stage2Img.gameObject.transform.localScale = new Vector3(1, 1, 1);
                Stage1Img.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
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
    private void CursorMove()
    {
        yMove = 0;
        WaitTime--;
        if (Input.GetKey(KeyCode.UpArrow) && WaitTime < 0 && Step < 3)
        {
            yMove = Velocity * Time.deltaTime;
            WaitTime = 30;
            Step++;
            ArrowDirection = 1;
            UpArrowScale = 1.5f;
            DownArrowImg.gameObject.transform.localScale = new Vector3(1, 1, 1);

        }
        if (Input.GetKey(KeyCode.DownArrow) && WaitTime < 0 && Step > 1)
        {
            yMove = -Velocity * Time.deltaTime;
            WaitTime = 30;
            Step--;
            ArrowDirection = 2;
            DownArrowScale = 1.5f;
            UpArrowImg.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if(WaitTime > -100 && ArrowDirection == 1 && UpArrowScale > 1.0f)
        {
            UpArrowScale -= 0.01f;
        }
        if (WaitTime > -100 && ArrowDirection == 2 && DownArrowScale > 1.0f)
        {
            DownArrowScale -= 0.01f;
        }
        this.transform.Translate(new Vector3(0, yMove, 0));
        UpArrowImg.gameObject.transform.localScale = new Vector3(UpArrowScale, UpArrowScale, UpArrowScale);
        DownArrowImg.gameObject.transform.localScale = new Vector3(DownArrowScale, DownArrowScale, DownArrowScale);

    }
    private void ChoiceStage()
    {

        if (ClearFade)
        {
            switch (Step)
            {
                case 1:
                    SceneManager.LoadScene("Stage 1");
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
