using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Icon_Move : MonoBehaviour
{
    public Title_Icon_s title_Icon_S;
    public Title_Icon_e title_Icon_E;
    public Title_Icon_o title_Icon_O;
    public Title_Icon_m title_Icon_M;
    public omake om;

    bool con_L; //コントローラー入力左
    bool con_R; //コントローラー入力右
    bool con_U; //コントローラー入力上
    bool con_D; //コントローラー入力下


    public float FadeTime = 1.0f;
    float time = 0f;
    private bool UseMenu = false;
    private bool UseOption = false;
    public bool isPlaying = false;
    public Image fadeImg;
    private float start;
    private float end;

    private bool ClearFade = false;
    private Color fadecolor;

    int wait;
    bool USE_KEY_BORD;

    int Select;

    bool FINISH;



    // サウンドmiya
    public AudioSource se_move;
    public AudioSource se_select;


    // miyaUI
    public miya_test_UI menu_script;

    // Start is called before the first frame update
    void Start()
    {
        start = 0f;
        end = 1f;
        wait = 0;
        USE_KEY_BORD = false;
        Select = 2;
        FINISH = false;

        CFadeManager.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        Check_Cont();

        if (!FINISH)
        {
            if ((Input.GetKeyDown(KeyCode.J) || Input.GetButton("OK")) && Select != 1 && Select != 3 && wait == 0)
            {
                // サウンドmiya
                if (se_select) se_select.Play();

                OutStartFadeAnim();

            }

            if ((Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("OK")) && Select == 1 && wait == 0)
            {
                if (!UseMenu)
                {
                    // サウンドmiya
                    if (se_select) se_select.Play();

                    UseMenu = true;
                    om.SetOpen(true);
                }
                else
                {
                    // サウンドmiya
                    if (se_select) se_select.Play();

                    UseMenu = false;
                    om.SetOpen(false);
                }
            }

            if ((Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("OK")) && Select == 3 && wait == 0)
            {
                //ここにメニュー画面表示の処理を書いてね～
                
                if (!UseOption)
                {
                    // サウンドmiya
                    if (se_select) se_select.Play();
                    UseOption = true;
                    menu_script.Show_Window();
                    wait = 15;
                }
                else
                {
                    // サウンドmiya
                    //if (se_select) se_select.Play();

                    //UseOption = false;
                    //menu_script.Close_Window();
                }
            }

            if ((Input.GetKey(KeyCode.RightArrow) || con_R) && wait == 0 && !UseMenu && !UseOption)
            {
                title_Icon_S.SetMoveL();
                title_Icon_E.SetMoveL();
                title_Icon_O.SetMoveL();
                title_Icon_M.SetMoveL();
                wait = 50;

                Select++;


                // サウンドmiya
                if (se_move) se_move.Play();

                if (Select == 5)
                {
                    Select = 1;
                }
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || con_L) && wait == 0 && !UseMenu && !UseOption)
            {
                title_Icon_S.SetMoveR();
                title_Icon_E.SetMoveR();
                title_Icon_O.SetMoveR();
                title_Icon_M.SetMoveR();
                wait = 50;

                Select--;


                // サウンドmiya
                if (se_move) se_move.Play();


                if (Select == 0)
                {
                    Select = 4;
                }
            }

            //オプション画面が開いている場合
            if(UseOption && wait == 0)
            {
                if(Input.GetKeyDown(KeyCode.J) || Input.GetButton("OK"))
                {
                    if(menu_script.ActionKey())
                    {
                        // サウンドmiya
                        if (se_select) se_select.Play();

                        UseOption = false;
                        menu_script.Close_Window();
                    }
                }

                if((Input.GetKey(KeyCode.RightArrow) || con_R))
                {
                    menu_script.RightKey();
                    wait = 15;
                }

                if(Input.GetKey(KeyCode.LeftArrow) || con_L)
                {
                    menu_script.LeftKey();
                    wait = 15;
                }

                if ((Input.GetKey(KeyCode.UpArrow) || con_U))
                {
                    menu_script.UpKey();
                    wait = 15;
                }

                if (Input.GetKey(KeyCode.DownArrow) || con_D)
                {
                    menu_script.DownKey();
                    wait = 15;
                }

                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    USE_KEY_BORD = true;
                }
            }
        }

        //ChoiceStage();
    }

    void FixedUpdate()
    {
        if (wait > 0)
        {
            wait--;
            if(wait == 0)
            {
                USE_KEY_BORD = false;
            }
        }
    }

    private void Check_Cont()
    {
        float LR;
        float UD;
        LR = Input.GetAxis("Horizontal_p"); //右ぷら
        UD = Input.GetAxis("Vertical_p"); //上ぷら

        con_L = false;
        con_R = false;
        con_U = false;
        con_D = false;

        if (LR > 0.5f)
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

        if(!con_L && !con_R && !con_U && !con_D && UseOption && !USE_KEY_BORD)
        {
            wait = 0;
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            USE_KEY_BORD = false;
        }
    }

    public void OutStartFadeAnim()
    {
        
        if (isPlaying == true)
        {
            return;
        }

        FINISH = true;
        //StartCoroutine(fadeinplay());

        if(Select == 2)
        {
            CFadeManager.FadeOut(1);    //ステージセレクトへ
        }
        else if(Select ==4)
        {
            CFadeManager.FadeOut(999);    //ステージセレクトへ
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

    private void ChoiceStage()
    {
        if (ClearFade)
        {
            switch (Select)
            {
                case 1:
                    //オマケ

                    break;
                case 2:
                    SceneManager.LoadScene("StageSelect_Newer");

                    break;
                case 3:
                //メニュー

                case 4:
                    //UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                    break;

            }
        }
    }
}
