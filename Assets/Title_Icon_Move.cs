using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Icon_Move : MonoBehaviour
{
    public Title_Icon_s title_Icon_S;
    public Title_Icon_e title_Icon_E;
    public Title_Icon_m title_Icon_M;

    bool con_L; //コントローラー入力左
    bool con_R; //コントローラー入力右


    public float FadeTime = 1.0f;
    float time = 0f;
    private bool UseMenu = false;
    public bool isPlaying = false;
    public Image fadeImg;
    public Image MenuImg;
    private float start;
    private float end;

    private bool ClearFade = false;
    private Color fadecolor;

    int wait;

    int Select;





    // サウンドmiya
    public AudioSource se_move;
    public AudioSource se_select;






    // Start is called before the first frame update
    void Start()
    {
        start = 0f;
        end = 1f;
        wait = 0;
        Select = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Check_Cont();

        if ((Input.GetKeyDown(KeyCode.J) || Input.GetButton("OK")) && Select != 1 && wait == 0)
        {
            // サウンドmiya
            se_select.Play();

            OutStartFadeAnim();
        }

        if ((Input.GetKeyDown(KeyCode.J)|| Input.GetButton("OK")) && Select == 1 && wait == 0)
        {
            if (!UseMenu)
            {
                // サウンドmiya
                se_select.Play();

                UseMenu = true;
                MenuImg.gameObject.SetActive(true);
            }
            else
            {
                // サウンドmiya
                se_select.Play();

                UseMenu = false;
                MenuImg.gameObject.SetActive(false);
            }
        }

        if ((Input.GetKey(KeyCode.RightArrow) || con_R) && wait == 0 && !UseMenu)
        {
            title_Icon_S.SetMoveL();
            title_Icon_E.SetMoveL();
            title_Icon_M.SetMoveL();
            wait = 50;

            Select++;


            // サウンドmiya
            se_move.Play();


            if (Select==4)
            {
                Select = 1;
            }
        }

        if ((Input.GetKey(KeyCode.LeftArrow) || con_L) && wait == 0 && !UseMenu)
        {
            title_Icon_S.SetMoveR();
            title_Icon_E.SetMoveR();
            title_Icon_M.SetMoveR();
            wait = 50;

            Select--;


            // サウンドmiya
            se_move.Play();


            if (Select == 0)
            {
                Select = 3;
            }
        }

        ChoiceStage();
    }

    void FixedUpdate()
    {
        if (wait > 0)
        {
            wait--;
        }
    }

    private void Check_Cont()
    {
        float LR;
        LR = Input.GetAxis("Horizontal_p"); //右ぷら

        con_L = false;
        con_R = false;

        if (LR > 0.5f)
        {
            con_R = true;
        }

        if (LR < -0.5f)
        {
            con_L = true;
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

    private void ChoiceStage()
    {

        if (ClearFade)
        {
            switch (Select)
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
}
