
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuCursor : MonoBehaviour
{
    [SerializeField]
    private int Velocity;
    private float yMove;
    private int WaitTime = 0;
    private int Step = 1;


    public float FadeTime = 2f;

    public Image fadeImg;
    public Image PanelImg;
    public Image MenuImg;
    public Image CursorImg;
    private bool Usemenu;
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
        Usemenu = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (ClearFade && Step == 2)
        {
            SceneManager.LoadScene("yb_MainScene");
        }
        if(ClearFade && Step == 1)
        {
            SceneManager.LoadScene("Stage 1");
        }


    }

    void FixedUpdate()
    {
        CursorMove();
        ChoiceStage();
    }
    private void CursorMove()
    {
        WaitTime--;

        if (Usemenu)
        {
            yMove = 0;
            if (Input.GetKey(KeyCode.UpArrow) && WaitTime < 0 && Step > 1)
            {
                yMove = Velocity * Time.deltaTime;
                WaitTime = 30;
                Step--;
            }
            if (Input.GetKey(KeyCode.DownArrow) && WaitTime < 0 && Step < 3)
            {
                yMove = -Velocity * Time.deltaTime;
                WaitTime = 30;
                Step++;
            }
            CursorImg.gameObject.transform.Translate(new Vector3(0, yMove, 0));
           // this.transform.Translate(new Vector3(0, yMove, 0));

            if(Input.GetKey(KeyCode.P) && WaitTime < 0)
            {
                Usemenu = false;
                PanelImg.gameObject.SetActive(false);
                MenuImg.gameObject.SetActive(false);
                WaitTime = 30;
                Step = 1;

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.P) && WaitTime < 0)
            {
                Debug.Log("11");
                Usemenu = true;
                PanelImg.gameObject.SetActive(true);
                MenuImg.gameObject.SetActive(true);
                WaitTime = 30;

            }
        }
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
                    break;
                case 3:
                    PanelImg.gameObject.SetActive(false);
                    MenuImg.gameObject.SetActive(false);
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
        ClearFade = true;
        isPlaying = false;

    }
}
