using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CRISTAL : MonoBehaviour
{
    bool OK;
    bool HIT;
    bool CLEAR;
    bool ONE;
    public int count;
    public int laser_count1;
    public int laser_count2;
    public int laser_count3;
    int laser_count1_2;
    int laser_count2_2;
    int laser_count3_2;
    const int nolma = 150;
    public int no;

    public int USE_LINE_NUM;
    const int MAX_LINE = 3;
    int Line_count;

    public UI_Clear Ui_Clear;
    public Player player;
    public Camera_Move Camera_Move;
    public Fade fade;

    public bool[] Clare = new bool[MAX_LINE];

    Image UI_clear_crystal;
    Text UI_text;

    // Start is called before the first frame update
    void Start()
    {
        HIT = false;
        OK = false;
        CLEAR = false;
        ONE = true;
        count = 0;
        laser_count1 = 0;
        laser_count2 = 0;
        laser_count3 = 0;
        laser_count1_2 = 0;
        laser_count2_2 = 0;
        laser_count3_2 = 0;
        no = 0;

        UI_clear_crystal = GameObject.Find("UI_crystal").GetComponent<Image>();
        UI_text = GameObject.Find("UI_crystal_text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(ONE)
        {
            ONE = false;
            UI_text.text = USE_LINE_NUM.ToString();
        }

        if(USE_LINE_NUM ==0)
        {
            USE_LINE_NUM = 1;
        }

        Line_count = 0;
        //OK = false;

        /*
        if (Clare[USE_LINE_NUM -1] == true)
        {
            count++;
            OK = true;

            if (count == 150)
            {
                Camera_Move.Set_ClearCamera();
                player.Set_Clear();
            }

            if(count == 280)
            {
                Ui_Clear.Set_Clear();
            }

            if(count == 370)
            {
                fade.SetOut();
                fade.SetNext(2);
            }
        }
        else
        {
            no++;
            if (no == 5)
            {
                no = 0;
                count = 0;
                //HIT = false;
            }
        }
        */

        if(Clare[0] == true)
        {
            laser_count1++;
            laser_count1_2 = 0;
            if (laser_count1 > 150)
            {
                laser_count1 = 150;
            }
        }
        else
        {
            if (laser_count1 > 0 && laser_count1_2 > 1)
            {
                laser_count1--;
            }

            laser_count1_2++;
        }

        if (Clare[1] == true)
        {
            laser_count2++;
            laser_count2_2 = 0;
            if (laser_count2 > 150)
            {
                laser_count2 = 150;
            }
        }
        else
        {
            if (laser_count2 > 0 && laser_count2_2 > 1)
            {
                laser_count2--;
            }
            laser_count2_2++;
        }

        if (Clare[2] == true)
        {
            laser_count3++;
            laser_count3_2 = 0;
            if (laser_count3 > 150)
            {
                laser_count3 = 150;
            }
        }
        else
        {
            if (laser_count3 > 0 && laser_count3_2 > 1)
            {
                laser_count3--;
            }

            laser_count3_2++;
        }

        if((laser_count1 + laser_count2 + laser_count3) >= nolma * USE_LINE_NUM && !CLEAR)
        {
            CLEAR = true;
            Camera_Move.Set_ClearCamera();
            player.Set_Clear();
        }

        if(CLEAR)
        {
            count++;

            if (count == 130)
            {
                Ui_Clear.Set_Clear();
            }

            if (count == 220)
            {
                //fade.SetOut();
                //fade.SetNext(2);
                CFadeManager.FadeOut(1);    //ステージセレクトへ
            }
        }

        UI_clear_crystal.fillAmount = (float)(laser_count1 + laser_count2 + laser_count3) / (150 * USE_LINE_NUM);


        for (int i=0;i<MAX_LINE;i++)
        {
            Clare[i] = false;
        }

        /*
        if(!OK)
        {
            if(USE_LINE_NUM == 2)
            {
                if (Clare[0] == true)
                {
                    another_count++;

                    if(another_count > 75)
                    {
                        another_count = 75;
                    }
                }
                else
                {
                    another_count--;
                }

                UI_clear_crystal.fillAmount = another_count / 150;
            }
            else if(USE_LINE_NUM == 3)
            {

            }
        }
        */
    }


    public void HitCristal()
    {
        //Clare[Line_count] = true;

        /*
        if(!HIT)
        {
            HIT = true;
        }
        */

        //Line_count++;

        //no = 0;
    }

    public void SetHit()
    {
        Clare[Line_count] = true;

        /*
        if(!HIT)
        {
            HIT = true;
        }
        */

        Line_count++;
        if(Line_count > MAX_LINE)
        {
            Line_count = MAX_LINE;
        }

        no = 0;
    }
}
