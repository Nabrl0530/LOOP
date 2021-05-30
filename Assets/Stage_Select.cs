using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage_Select : MonoBehaviour
{
    bool con_U; //コントローラー入力上
    bool con_D; //コントローラー入力下

    public Stage_Select_CArrow Stage_Select_CArrow;
    public Stage_Select_CArrow Stage_Select_CArrow2;

    public float FadeTime = 1.0f;
    float time = 0f;
    public bool isPlaying = false;
    public Image fadeImg;
    private float start;
    private float end;

    private bool ClearFade = false;
    private Color fadecolor;

    const int MAX_OBJ = 3;

    bool WorldEND;

    int wait;

    public int Select;

    public GameObject[] Obj = new GameObject[MAX_OBJ];
    public Canvas[] can = new Canvas[MAX_OBJ];

    // Start is called before the first frame update
    void Start()
    {
        start = 0f;
        end = 1f;
        wait = 0;
        Select = 1;
        WorldEND = false;
    }

    // Update is called once per frame
    void Update()
    {
        Check_Cont();

        if (!WorldEND)
        {
            if ((Input.GetKeyDown(KeyCode.J) || Input.GetButton("OK")) && wait == 0)
            {
                //OutStartFadeAnim();
                Obj[Select - 1].GetComponent<Stage_Select_Slide>().SetPick();
                WorldEND = true;

                if(Select == 1)
                {
                    can[0].GetComponent<Stage_Select_Easy>().SET_USE();
                }
                else if (Select == 2)
                {
                    can[1].GetComponent<Stage_Select_Normal>().SET_USE();
                }
                else if (Select == 3)
                {
                    can[2].GetComponent<Stage_Select_Hard>().SET_USE();
                }
            }

            //タイトルに戻る
            if ((Input.GetKeyDown(KeyCode.K) || Input.GetButton("NO")) && wait == 0)
            {
                OutStartFadeAnim();
                WorldEND = true;
            }

            if ((Input.GetKey(KeyCode.UpArrow) || con_U) && wait == 0 && Select < 3)
            {
                Obj[Select - 1].GetComponent<Stage_Select_Slide>().SetDown_IN(false);
                Obj[Select].GetComponent<Stage_Select_Slide>().SetDown_IN(true);
                Stage_Select_CArrow.SetBig();

                wait = 50;

                Select++;
            }

            if ((Input.GetKey(KeyCode.DownArrow) || con_D) && wait == 0 && Select > 1)
            {
                Obj[Select - 1].GetComponent<Stage_Select_Slide>().SetUp_IN(false);
                Obj[Select - 2].GetComponent<Stage_Select_Slide>().SetUp_IN(true);
                Stage_Select_CArrow2.SetBig();

                wait = 50;

                Select--;
            }
        }
        else if(WorldEND)
        {
            ChoiceStage();
        }        
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
        float UD;
        UD = Input.GetAxis("Vertical_p"); //上ぷら

        con_U = false;
        con_D = false;

        if (UD > 0.5f)
        {
            con_U = true;
        }

        if (UD < -0.5f)
        {
            con_D= true;
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
            SceneManager.LoadScene("yb_MainScene");
        }
    }

    public void Back()
    {
        Obj[Select - 1].GetComponent<Stage_Select_Slide>().SetBack();
    }

    public void clear_END()
    {
        WorldEND = false;
    }
}
