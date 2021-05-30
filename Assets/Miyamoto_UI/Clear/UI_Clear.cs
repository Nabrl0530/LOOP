
// //                              // //
// //   Author：宮本　早希         // //
// //   メニューのカーソル処理     // //
// //                              // //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------------------------------------------------------------------------------

public class UI_Clear : MonoBehaviour
{
    public bool isUiClear;      // クリアフラグ（ゲームクリア時にこれをtrueにすると発動します。）
    Transform ChildTransform;   // 子オブジェクトのトランスフォーム
    int time;
    public UI_Clear_add uI_Clear_Add;

    // ------------------------------------------------------------------------------------------

    void Start()
    {
        isUiClear = false;
        ChildTransform = GameObject.Find("ClearImage").transform;
        time = 0;
    }

    // ------------------------------------------------------------------------------------------

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)) // 試験的なクリアフラグONOFF処理です、適宜変更、削除してください。
        {
            if (isUiClear)
            {
                isUiClear = false;
            }
            else
            {
                isUiClear = true;
            }
            time = 0;
        }
        */

        if (isUiClear)
        {
            if (!ChildTransform.gameObject.activeSelf)
            {
                ChildTransform.gameObject.SetActive(true);
            }
        }
        else
        {
            if (ChildTransform.gameObject.activeSelf)
            {
                ChildTransform.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (isUiClear)
        {
            time++;
            if(time == 55)
            {
                uI_Clear_Add.Set_ON();
            }
        }
    }

    public void Set_Clear()
    {
        isUiClear = true;
        time = 0;
    }
}
