using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class beam : MonoBehaviour
{
    LineRenderer LineRenderer;
    void Start()
    {
        // LineRendererコンポーネントをゲームオブジェクトにアタッチする
        ///var lineRenderer = gameObject.AddComponent<LineRenderer>();
        LineRenderer = this.GetComponent<LineRenderer>();

        var positions = new Vector3[]{
        new Vector3(0, 0, 0),               // 開始点
        new Vector3(8, 0, 0),               // 終了点
        };

        LineRenderer.startWidth = 0.1f;                   // 開始点の太さを0.1にする
        LineRenderer.endWidth = 0.1f;                     // 終了点の太さを0.1にする

        // 線を引く場所を指定する
        LineRenderer.SetPositions(positions);
    }


    void Update()
    {
        var positions = new Vector3[]{
        transform.position,               // 開始点
        transform.position + transform.forward * 5,               // 終了点
        };

        LineRenderer.SetPositions(positions);
    }
}
