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
    //private bool UseMenu = false;
    public bool isPlaying = false;
    public Image fadeImg;
    public Image MenuImg;
    private float start;
    private float end;

    private bool ClearFade = false;
    private Color fadecolor;

    const int MAX_OBJ = 3;

    int wait;

    int Select;

    public GameObject[] Obj = new GameObject[MAX_OBJ];

    // Start is called before the first frame update
    void Start()
    {
        start = 0f;
        end = 1f;
        wait = 0;
        Select = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Check_Cont();

        if (Input.GetKeyDown(KeyCode.Return) && wait == 0)
        {
            OutStartFadeAnim();
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
