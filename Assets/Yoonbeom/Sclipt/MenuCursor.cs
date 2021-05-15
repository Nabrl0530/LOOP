/*
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
    miya_player_move player;


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
        miya_player_move comp = GameObject.Find("Player").GetComponent<miya_player_move>();
        player = comp;
        start = 0f;
        end = 1f;
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
            SceneManager.LoadScene("yb_Stage1");
        }


    }

    void FixedUpdate()
    {
        CursorMove();
        ChoiceStage();
    }
    private void CursorMove()
    {
        yMove = 0;
        WaitTime--;
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
                    break;
                case 3:
                    player.WaitCount = 30;
                    player.Image_PanelWindow.SetActive(false);
                    player.Image_MenuWindow.SetActive(false);
                    player.UseMenu = false;
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
*/