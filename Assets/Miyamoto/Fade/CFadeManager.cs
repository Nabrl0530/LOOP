
// //                         // //
// //   Author:宮本 早希      // //
// //   シーン遷移とフェード  // //
// //                         // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;


// // クラス // //
public class CFadeManager : MonoBehaviour
{
    // フェードイン, フェードアウトのフラグ
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;


    // フェード時間（単位：杪）
    public static float FadeTime = 1.0f;


    // 遷移先のシーン番号
    private static int NextScene;


    // フェードのインターフェース取得用
    public static InterfaceFade iFade;


    // マスク範囲
    public float CutoutRange;


    // // フェードイン開始 // //
    public static void FadeIn()
    {
        // フェードインフラグＯＮ
        isFadeIn = true;
    }


    // // フェードアウト開始 // //
    public static void FadeOut(int nextscene)
    {
        // 次のシーンを決定
        NextScene = nextscene;

        // フェードアウトフラグＯＦＦ
        isFadeOut = true;
    }


    // // 初期化 // //
    void Start()
    {
        // フェードのインターフェースを取得
        iFade = GetComponent<InterfaceFade>();

        // マスク範囲を取得
        iFade.Range = CutoutRange;
    }

    // // 更新 // //
    void Update()
    {
        // フェードイン
        if (isFadeIn)
        {
            // マスク範囲を減らす
            iFade.Range -= FadeTime * Time.deltaTime;

            // ０を越えたら止める
            if (iFade.Range < 0.0f)
            {
                iFade.Range = 0.0f;
                isFadeIn = false;
            }
        }


        // フェードアウト
        else if (isFadeOut)
        {
            // マスク範囲を増やす
            iFade.Range += FadeTime * Time.deltaTime;

            // １を越えたら止める
            if (iFade.Range > 1.0f)
            {
                iFade.Range = 1.0f;
                isFadeOut = false;
                if(NextScene != 999)
                {
                    SceneManager.LoadScene(NextScene);
                }
                else
                {
                    UnityEngine.Application.Quit();
                }
            }
        }
    }
}
