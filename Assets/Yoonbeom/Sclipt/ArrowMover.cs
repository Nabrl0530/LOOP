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

    bool con_L; //コントローラー入力左
    bool con_R; //コントローラー入力右
    bool con_D; //コントローラー入力下
    bool con_U; //コントローラー入力上

    void Start()
    {
        start = 0f;
        end = 1f;
        WaitTime = 0;
    }    
    
    void Update()
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
            WaitTime = 15;

        }

        Check_Cont();

        CursorMove();
        ChoiceStage();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(WaitTime > 0)
        {
            WaitTime--;
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
        xMove = 0;
        //WaitTime--;
        if ((Input.GetKey(KeyCode.LeftArrow) || con_L) && WaitTime == 0 && Step > 1 && !UseMenu)
        {
            xMove = -Velocity * Time.deltaTime;
            xMove = -370;
            WaitTime = 15;
            Step--;
        }
        if ((Input.GetKey(KeyCode.RightArrow) || con_R) && WaitTime == 0 && Step < 3 && !UseMenu)
        {
            xMove = Velocity * Time.deltaTime;
            xMove = 370;
            WaitTime = 15;
            Step++;
        }

        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !con_L && !con_R)
        {
            //WaitTime = 0;
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
                    //メニュー

                    break;
                case 2:
                    SceneManager.LoadScene("yb_ChoiceScene");

                    break;
                case 3:
                    //UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                    break;
             
            }
        }
    }

    private void Check_Cont()
    {
        float LR;
        float UD;
        UD = Input.GetAxis("Vertical_p");   //上ぷら
        LR = Input.GetAxis("Horizontal_p"); //右ぷら

        con_L = false;
        con_R = false;
        con_U = false;
        con_D = false;

        if(LR > 0.5f)
        {
            con_R = true;
        }

        if (LR < -0.5f)
        {
            con_L = true;
        }

        if (UD > 0.5f)
        {
            con_U = true;
        }

        if (UD < -0.5f)
        {
            con_D = true;
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
