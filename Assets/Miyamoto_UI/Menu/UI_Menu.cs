
// //                              // //
// //   Author：宮本　早希         // //
// //   メニューのカーソル処理     // //
// //                              // //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------------------------------------------------------------------------------

public class UI_Menu : MonoBehaviour
{
    public bool Show;       // メニューが見えるか否か
    public bool NotUSE;     // 使用禁止状態
    Transform Commands;     // 子オブジェクト（メニューとカーソル全体統括するオブジェクト）のトランスフォーム

    // ------------------------------------------------------------------------------------------
    void Start()
    {
        // 全体統括子オブジェクト取得
        if (LanguageSetting.Get_Is_Japanese())
        {
            Commands = GameObject.Find("CommandsEnglishVer").transform;
            SetAllInactive();
            Commands = GameObject.Find("Commands").transform; 
        }
        else
        {
            Commands = GameObject.Find("Commands").transform;
            SetAllInactive();
            Commands = GameObject.Find("CommandsEnglishVer").transform;

        }
        
        
        Show = false;                                       // 最初は見えないようにする
        NotUSE = false;
    }

    // ------------------------------------------------------------------------------------------
    
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButton("MENU"))    // メニューキー（適宜変更してください。）
        {
            if(Show)
            {
                Show = false;   // 見えない
            }
            else
            {
                Show = true;    // 見える
            }
        }
        */

        if(Show)    // 見えるとき、全体統括オブジェクトの子オブジェクトをすべてアクティブにする
        {
            if (!NotUSE)
            {
                foreach (Transform t in Commands)
                {
                    if (!t.gameObject.activeSelf)
                    {
                        t.gameObject.SetActive(true);
                    }
                }
            }
        }
        else       // 見えないとき、全体統括オブジェクトの子オブジェクトをすべて非アクティブにする
        {
            foreach(Transform t in Commands)
            {
                if(t.gameObject.activeSelf)
                {
                    t.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetShow()
    {
        if (Show)
        {
            Show = false;   // 見えない
        }
        else
        {
            Show = true;    // 見える
        }
    }

    public void SetNot()
    {
        NotUSE = true;
    }

    //子オブジェクトをすべて非アクティブにする
    private void SetAllInactive()
    {
        foreach (Transform t in Commands)
        {
            if (t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
    //全体統括オブジェクトの子オブジェクトをすべてアクティブにする
    private void SetAllActive()
    {
        foreach (Transform t in Commands)
        {
            if (!t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(true);
            }
        }
    }
}
