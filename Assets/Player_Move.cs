using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    TOWER TOWER;
    Leba leba;
    Camera Camera;
    public GameObject g_Camera;
    float Speed_Move = 2.5f;
    Rigidbody Rigid;
    GameObject Pipe1;
    GameObject Pipe2;
    GameObject Pipe3;
    bool LOCK;  //手動操作禁止状態
    int NoComand;

    public bool HIT_TOWER;
    bool HIT_LEVER;
    bool HIT_LEVER_BACK;

    private Vector3 latestPos;  //前回のPosition
    private Vector3 lastDirection;
    public float RotateSpeed = 1f;

    //private GameObject camera;   //プレイヤー情報格納用
    public float rot;  //角度
    public float len;  //長さ
    int Layer = 1;

    // Start is called before the first frame update
    void Start()
    {
        Camera= GameObject.Find("Main Camera").GetComponent<Camera>();
        Pipe1 = GameObject.Find("FloorOne");
        Pipe2 = GameObject.Find("FloorTwo");
        Pipe3 = GameObject.Find("FloorThree");

        // Rigidbody取得
        Rigid = this.GetComponent<Rigidbody>();

        // カメラ未設定時
        if (!g_Camera) Debug.Log("【miya_player_move】there is no camera");

        rot = 0;
        len = 3;

        LOCK = false;
        NoComand = 0;
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
        //Debug.Log(len);
        if(len >= 12.0f)
        {
            transform.SetParent(Pipe3.transform);
            Layer = 3;
        }
        else if(len >= 8.5f)
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

        //向きの更新
        /*
        Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
        latestPos = transform.position;  //前回のPositionの更新

        //ベクトルの大きさが0.01以上の時に向きを変える処理をする
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
        }
        */

        //Vector3 forward = new Vector3(RotateLookX, RotateLookY, RotateLookZ);
        Quaternion rot = Quaternion.LookRotation(lastDirection);

        rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed);
        this.transform.rotation = rot;

        //ギミック操作（塔をつかむ）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (HIT_TOWER && !LOCK && NoComand == 0)
            {
                LOCK = true;
                NoComand = 0;
            }
            else if (HIT_TOWER && LOCK && NoComand == 0)
            {
                //LOCK = false;
                NoComand = 60;
            }
        }

        //塔の操作穴１の移動
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(LOCK)
            {
                TOWER.HoleMove_1();
            }

            if(HIT_LEVER)
            {
                Debug.Log("レバー操作");
                leba.SpinL();
            }
        }

        //塔の操作穴２の移動
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (LOCK)
            {
                TOWER.HoleMove_2();
            }

            if (HIT_LEVER)
            {
                Debug.Log("レバー操作");
                leba.SpinR();
            }
        }

        //塔の操作穴１の回転
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (LOCK)
            {
                TOWER.HoleSpin_1();
            }
        }

        //塔の操作穴２の回転
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (LOCK)
            {
                TOWER.HoleSpin_2();
            }
        }

        if (NoComand > 0)
        {
            NoComand--;
            if(NoComand == 0)
            {
                LOCK = false;
            }
        }

        //Camera.Update_Auto();
    }

    void FixedUpdate()
    {
        // カメラベクトル取得
        Vector3 camera_front = g_Camera.transform.forward;
        Vector3 camera_right = g_Camera.transform.right;

        Vector3 direction_move = new Vector3(0, 0, 0);

        // 移動
        if (!LOCK)
        {
            // 入力            
            if (Input.GetKey(KeyCode.W)) direction_move += camera_front;
            if (Input.GetKey(KeyCode.S)) direction_move -= camera_front;
            if (Input.GetKey(KeyCode.D)) direction_move += camera_right;
            if (Input.GetKey(KeyCode.A)) direction_move -= camera_right;

            // 正規化
            if (direction_move != new Vector3(0, 0, 0))
            {
                // Y方向を削除
                direction_move.y = 0;
                lastDirection = direction_move;
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

    public void SetHIT_TOWER()
    {
        HIT_TOWER = true;
    }

    public void ClearHIT_TOWER()
    {
        HIT_TOWER = false;
    }

    public void SetHIT_LEVER()
    {
        HIT_LEVER = true;
    }

    public void ClearHIT_LEVER()
    {
        HIT_LEVER = false;
    }

    public void SetHIT_LEVER_BACK()
    {
        HIT_LEVER_BACK = true;
    }

    public void ClearHIT_LEVER_BACK()
    {
        HIT_LEVER_BACK = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TOWER"))
        {
            TOWER = other.GetComponent<TOWER>();
        }

        if(other.gameObject.CompareTag("LEVER"))
        {
            leba = other.GetComponent<Leba>();
        }
    }
}
