using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPop : MonoBehaviour
{
    [SerializeField] Scroll scroll;
    [SerializeField] GameObject select;
    bool isEnter = true;

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
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D))
        {
            isEnter = !isEnter;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            bool a = isEnter;
            isEnter = true;
            scroll.JudgeYesOrNo(a);
        }

        //はいの方だったら
        if(isEnter)
        {
            select.GetComponent<RectTransform>().localPosition = yes;
        }
        else
        {
            select.GetComponent<RectTransform>().localPosition = no;
        }
    }
}

//アニメーションいれたい