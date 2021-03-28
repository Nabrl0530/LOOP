using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;   //プレイヤー情報格納用
    public float len;

    // Use this for initialization
    void Start()
    {
        //Playerの情報を取得
        this.player = GameObject.Find("Player");

        len = 8.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_Auto()
    {
        Vector3 p2 = new Vector3(0, 0, 0);
        float dx = player.transform.position.x - p2.x;
        float dz = player.transform.position.z - p2.z;
        float rad = Mathf.Atan2(dx, dz);

        //新しいトランスフォームの値を代入する      

        Vector3 pos = new Vector3(0, 0, 0);
        pos.x = Mathf.Sin(rad) * len;
        pos.y = 6.0f;
        pos.z = Mathf.Cos(rad) * len;

        transform.position = pos;  // 座標を設定
        this.transform.LookAt(new Vector3(0, 0, 0));
    }
}
