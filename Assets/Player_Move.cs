using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public Camera Camera;
    //private GameObject camera;   //プレイヤー情報格納用
    float rot;  //角度
    float len;  //長さ

    // Start is called before the first frame update
    void Start()
    {
        Camera= GameObject.Find("Main Camera").GetComponent<Camera>();
        rot = 0;
        len = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // 左に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rot -= 0.03f;
        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rot += 0.03f;
        }
        // 奥に移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            len -= 0.05f;
            if(len <0.2f)
            {
                len = 0.2f;
            }
        }
        // 手前に移動
        if (Input.GetKey(KeyCode.DownArrow))
        {
            len += 0.05f;

            if (len > 5.8f)
            {
                len = 5.8f;
            }
        }

        this.transform.Translate(0.1f, 0.0f, 0.0f);

        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = new Vector3(0,0,0);
        pos.x = Mathf.Sin(rot) * len;
        pos.y = 0.8f;
        pos.z = -Mathf.Cos(rot) * len;

        myTransform.position = pos;  // 座標を設定

        Camera.Update_Auto();
    }
}
