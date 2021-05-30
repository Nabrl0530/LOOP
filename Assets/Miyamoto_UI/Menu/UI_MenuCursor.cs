
// //                              // //
// //   Author：宮本　早希         // //
// //   メニューの処理             // //
// //                              // //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ------------------------------------------------------------------------------------------

public class UI_MenuCursor : MonoBehaviour
{
    RectTransform thisTransform;    // 自身のレクトトランスフォーム（座標変更用)
    int CursorPosition;             // カーソル位置（見栄えの問題）

    GameObject Parent;              // 親オブジェクト（MenuCanvas）のための入れ物
    UI_Menu ParentMenu;             // 親オブジェクトについたスクリプトが欲しい


    // カーソルの場所一覧
    enum CursorPos
    {
        Retry,
        Stage,
        Return,
        end
    };

    // ------------------------------------------------------------------------------------------

    void Start()
    {
        thisTransform = GetComponent<RectTransform>();        // レクトトランスフォーム取得
        CursorPosition = (int)CursorPos.Retry;                // カーソル初期位置決定

        Parent = GameObject.Find("MenuCanvas");               // 親オブジェクトを取得
        ParentMenu = Parent.GetComponent<UI_Menu>();          // 親オブジェクトについたスクリプトを取得
    }


    // ------------------------------------------------------------------------------------------

    void Update()
    {
        // カーソル移動
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CursorPosition++;
            if (CursorPosition > (int)CursorPos.Return)
            {
                CursorPosition = (int)CursorPos.Retry;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CursorPosition--;
            if (CursorPosition < (int)CursorPos.Retry)
            {
                CursorPosition = (int)CursorPos.Return;
            }
        }
        
        // カーソル位置に合わせて座標を変更
        switch (CursorPosition)
        {
            case (int)CursorPos.Retry:
                thisTransform.anchoredPosition = new Vector2(70.0f, 280.0f);
                break;

            case (int)CursorPos.Stage:
                thisTransform.anchoredPosition = new Vector2(70.0f, 100.0f);
                break;

            case (int)CursorPos.Return:
                thisTransform.anchoredPosition = new Vector2(70.0f, -60.0f);
                break;

            default:
                break;
        }


        // 決定が押されたら
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (CursorPosition)
            {
                case (int)CursorPos.Retry:
                    ParentMenu.Show = false;                                        // メニューを見えないようにする
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // 現在シーンを読込しなおす
                    break;

                case (int)CursorPos.Stage:
                    ParentMenu.Show = false;        // メニューを見えないようにする
                    // ステージ選択画面シーンへ遷移
                    break;

                case (int)CursorPos.Return:
                    ParentMenu.Show = false;        // メニューを見えないようにする
                    break;

                default:
                    break;
            }
        }
    }
}