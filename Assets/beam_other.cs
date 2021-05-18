using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam_other : MonoBehaviour
{
    LineRenderer LineRenderer;
    Vector3 Pos_End;
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
        Vector3 Pos_End = transform.position;
        Vector3 Ditector = transform.forward;   //new Vector3(0, 0, -1);
        /*
        RaycastHit hit;
        if (Physics.Raycast(Pos_base, Ditector, out hit, 30.0f))
        {
            //Debug.Log(hit.collider.gameObject.transform.position);
            //Pos_End = hit.collider.gameObject.transform.position;
            Pos_End = hit.point;
            //Debug.Log(hit.point);
            //Debug.Log(hit.collider.gameObject);
            if (hit.collider.CompareTag("Door"))
            {
                hit.collider.gameObject.GetComponent<Door>().HitLaser();
            }

            if (hit.collider.CompareTag("HOLE"))
            {
                //Debug.Log("HIT_HOLE?");
                hit.collider.gameObject.GetComponent<TOWER_HOLE>().HitLaser();
            }

            if (hit.collider.CompareTag("MIRROR"))
            {
                //Debug.Log("HIT_MIRROR");
                hit.collider.gameObject.GetComponent<MIRROR>().SetBasepos(hit.point, Ditector);

                hit.collider.gameObject.GetComponent<MIRROR>().HitLaser();
            }

            if (hit.collider.CompareTag("CRISTAL"))
            {
                //Debug.Log("クリスタル");
                hit.collider.gameObject.GetComponent<CRISTAL>().HitCristal();
            }

        }
        else
        {
            Pos_End = transform.position + Ditector * 30;
        }
        */

        //DoorController.CollideWithRayOpenDoor(Pos_base, Ditector);

        var positions = new Vector3[]{
        transform.position,               // 開始点
        transform.position + Ditector * 5,               // 終了点
        };

        LineRenderer.SetPositions(positions);
        LineRenderer.SetPosition(1, Pos_End);
    }

    public void Set_End(Vector3 end)
    {
        Pos_End = end;
    }
}
