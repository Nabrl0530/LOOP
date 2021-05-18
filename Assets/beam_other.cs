using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam_other : MonoBehaviour
{
    LineRenderer LineRenderer;
    Vector3 Pos_End;
    Vector3 Base_position;
    void Start()
    {
        LineRenderer = this.GetComponent<LineRenderer>();

        var positions = new Vector3[]{
        new Vector3(0, 0, 0),               // 開始点
        new Vector3(8, 0, 0),               // 終了点
        };

        LineRenderer.startWidth = 0.5f;                   // 開始点の太さを0.1にする
        LineRenderer.endWidth = 0.5f;                     // 終了点の太さを0.1にする

        // 線を引く場所を指定する
        LineRenderer.SetPositions(positions);
    }


    void Update()
    {
        Vector3 Pos_base = transform.position;
        Vector3 Ditector = transform.forward;
        
        var positions = new Vector3[]{
        Base_position,               // 開始点
        transform.position + Ditector * 5,               // 終了点
        };

        LineRenderer.SetPositions(positions);
        LineRenderer.SetPosition(1, Pos_End);
    }

    public void Set_End(Vector3 end)
    {
        Pos_End = end;
    }

    public void Set_Base(Vector3 Base)
    {
        Base_position = Base;
    }
}
