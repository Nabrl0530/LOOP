using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPop : MonoBehaviour
{
    bool con_L; //コントローラー入力左
    bool con_R; //コントローラー入力右
    bool con_LR; //コントローラー入力左右

    [SerializeField] Scroll scroll;
    [SerializeField] GameObject select;
    bool isEnter = true;
    bool isEnter_b =false;
    bool End = false;
    int key_wait = 0;

    [SerializeField] Vector3 yes = new Vector3(-37, -41, 0);
    Vector3 no = new Vector3(-375, -41, 0);

    //private void Start()
    //{
    //    yes = select.GetComponent<RectTransform>().position;
    //    no = yes;
    //    no.x -= 300;
    //}

    void Update()
    {
        if(End)
        {
            return;
        }

        Check_Cont();

        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D) || con_LR)
        {
            isEnter = !isEnter;
        }

        if(Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("OK"))
        {
            bool a = isEnter;
            isEnter = true;
            if(scroll.JudgeYesOrNo(a))
            {
                End = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("NO"))
        {
            isEnter = true;
            scroll.JudgeYesOrNo(false);          
        }

        if (isEnter != isEnter_b)
        {
            isEnter_b = isEnter;
            //はいの方だったら
            if (isEnter)
            {
                select.GetComponent<RectTransform>().localPosition = yes;
            }
            else
            {
                select.GetComponent<RectTransform>().localPosition = no;
            }
        }
    }

    void FixedUpdate()
    {
        if (key_wait > 0)
        {
            key_wait--;
        }
    }

    private void Check_Cont()
    {
        float LR;
        LR = Input.GetAxis("Horizontal_p"); //右ぷら

        con_L = false;
        con_R = false;
        con_LR = false;

        if(LR > 0.5f)
        {
            con_R = true;
        }

        if (LR < -0.5f)
        {
            con_L = true;
        }

        if ((con_R || con_L) && key_wait == 0) 
        {
            con_LR = true;
            key_wait = 15;
        }

        if(!con_L && !con_R)
        {
            key_wait = 0;
        }
    }
}

//アニメーションいれたい