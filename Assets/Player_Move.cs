using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Camera Camera;
    public GameObject g_Camera;
    float Speed_Move = 2.5f;
    Rigidbody Rigid;
    GameObject Pipe1;
    GameObject Pipe2;
    GameObject Pipe3;

    //private GameObject camera;   //プレイヤー情報格納用
    public float rot;  //角度
    public float len;  //長さ
    int Layer = 1;

    // Start is called before the first frame update
    void Start()
    {
        Camera= GameObject.Find("Main Camera").GetComponent<Camera>();
        Pipe1 = GameObject.Find("PIPE_1_BASE");
        Pipe2 = GameObject.Find("PIPE_2_BASE");
        Pipe3 = GameObject.Find("PIPE_3_BASE");

        // Rigidbody取得
        Rigid = this.GetComponent<Rigidbody>();

        // カメラ未設定時
        if (!g_Camera) Debug.Log("【miya_player_move】there is no camera");

        rot = 0;
        len = 3;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // 左に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rot -= 1;
        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rot += 1;
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
        */

        //float len_sub = transform.position.x * transform.position.x;
        len = Mathf.Sqrt(Mathf.Pow(transform.position.x,2) + Mathf.Pow(transform.position.z, 2)); 

        if(len >= 4.5f)
        {
            transform.SetParent(Pipe3.transform);
            Layer = 3;
        }
        else if(len >= 3.0f)
        {
            transform.SetParent(Pipe2.transform);
            Layer = 2;
        }
        else
        {
            transform.SetParent(Pipe1.transform);
            Layer = 1;
        }

        /*
        this.transform.Translate(0.1f, 0.0f, 0.0f);

        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = new Vector3(0,0,0);
        pos.x = Mathf.Sin(rot * Mathf.Deg2Rad) * len;
        pos.y = 0.8f;
        pos.z = -Mathf.Cos(rot * Mathf.Deg2Rad) * len;

        myTransform.position = pos;  // 座標を設定
        */

        Camera.Update_Auto();
    }

    void FixedUpdate()
    {
        // カメラベクトル取得
        Vector3 camera_front = Camera.transform.forward;
        Vector3 camera_right = Camera.transform.right;

        // 移動
        {
            // 入力
            Vector3 direction_move = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W)) direction_move += camera_front;
            if (Input.GetKey(KeyCode.S)) direction_move -= camera_front;
            if (Input.GetKey(KeyCode.D)) direction_move += camera_right;
            if (Input.GetKey(KeyCode.A)) direction_move -= camera_right;

            // 正規化
            if (direction_move != new Vector3(0, 0, 0))
            {
                // Y方向を削除
                direction_move.y = 0;
                direction_move = direction_move.normalized * Speed_Move;// * Time.deltaTime;
                                                                        //Debug.Log(direction_move);
            }

            // 移動
            Rigid.velocity = direction_move;
        }
    }

    public int GetLayer()
    {
        return Layer;
    }

    public void AddRot(int R)
    {
        rot += R;
    }
}
