
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
    public Fade fade;
    public Player_State ps;
    RectTransform thisTransform;    // 自身のレクトトランスフォーム（座標変更用)
    int CursorPosition;             // カーソル位置（見栄えの問題）

    GameObject Parent;              // 親オブジェクト（MenuCanvas）のための入れ物
    UI_Menu ParentMenu;             // 親オブジェクトについたスクリプトが欲しい

    bool con_U; //コントローラー入力左
    bool con_D; //コントローラー入力右

    int keywait;

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

        keywait = 0;
    }


    // ------------------------------------------------------------------------------------------

    void Update()
    {
        Check_Cont();

        // カーソル移動
        if (Input.GetKeyDown(KeyCode.DownArrow) || con_D)
        {
            CursorPosition++;
            if (CursorPosition > (int)CursorPos.Return)
            {
                CursorPosition = (int)CursorPos.Retry;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || con_U)
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
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButton("OK"))
        {
            switch (CursorPosition)
            {
                case (int)CursorPos.Retry:
                    ParentMenu.Show = false;                                        // メニューを見えないようにする
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // 現在シーンを読込しなおす
                    fade.SetOut();
                    fade.SetNext(1);
                    ParentMenu.SetNot();    //開けない状態に
                    break;

                case (int)CursorPos.Stage:
                    ParentMenu.Show = false;        // メニューを見えないようにする
                    fade.SetOut();
                    fade.SetNext(2);
                    ParentMenu.SetNot();    //開けない状態に
                    // ステージ選択画面シーンへ遷移
                    break;

                case (int)CursorPos.Return:
                    ps.Menu_OFF();
                    ParentMenu.Show = false;        // メニューを見えないようにする
                    break;

                default:
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (keywait > 0)
        {
            keywait--;
        }
    }

    private void Check_Cont()
    {
        float UD;
        UD = Input.GetAxis("Vertical_p"); //上ぷら

        con_U = false;
        con_D = false;

        if (UD > 0.5f && keywait == 0)
        {
            con_U = true;
            keywait = 25;
        }

        if (UD < -0.5f && keywait == 0)
        {
            con_D = true;
            keywait = 25;
        }
    }
}