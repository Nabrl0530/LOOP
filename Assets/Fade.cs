using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Image fadeImg;
    bool fade_in;
    bool fade_out;
    float alpha;
    bool END;
    int Next;

    // Start is called before the first frame update
    void Start()
    {
        fade_in = true;
        fade_out = false;
        END = false;
        alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        if (fade_in)
        {
            alpha -= 0.02f;

            Color fadecolor = fadeImg.color;

            fadecolor.a = alpha;

            if(alpha < 0)
            {
                alpha = 0;
                fade_in = false;
            }

            fadeImg.color = fadecolor;


        }

        if (fade_out)
        {
            alpha += 0.02f;

            Color fadecolor = fadeImg.color;

            fadecolor.a = alpha;

            if (alpha > 1)
            {
                alpha = 1;
                fade_out = false;
                END = true;
            }

            fadeImg.color = fadecolor;

            if(END)
            {
                switch(Next)
                {
                    case 1:
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // 現在シーンを読込しなおす
                        break;
                    case 2:
                        SceneManager.LoadScene("yb_ChoiceScene");   //ステージセレクトを読み込む
                        break;
                }
            }
        }
    }

    public void SetOut()
    {
        fade_out = true;
    }

    public void SetNext(int i)
    {
        Next = i;
    }
}
