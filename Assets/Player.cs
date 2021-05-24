using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 参照
    //public GameObject Center;
    public Player_State sc_state;

    TOWER TOWER;
    Leba leba;
    //leba_2 leba_2;
    Bridge bridge;
    Door door;
    GameObject Pipe1;
    GameObject Pipe2;
    GameObject Pipe3;

    public Player_Forword Player_Forword;
    public Player_Check Player_Check;
    public Player_Under Player_Under;

    //int NoComand;

    public int Actcount;    //各アクションの処理時間
    public float Act_spin;  //ギミック操作時の向き補正値
    public Vector3 Act_move;  //アクションによる移動量
    public Vector3 Gimmikpoint; //操作対象の位置
    public Vector3 Gatepoint;   //吸い込まれる場所
    public float pop_y;         //放出力
    public float len;  //長さ
    private float searchAngle = 80f;    //視野角
    private float Size = 1.0f;
    private float rot_z;   //回転速度


    bool HIT_TOWER = false;
    bool HIT_LEVER = false;
    //public bool HIT_LEVER2;
    bool HIT_BRIDGE = false;
    bool HIT_LEVER_BACK = false;
    bool HIT_DOOR = false;

    bool IsUnder_m = false;

    // 変数
    Rigidbody Rigid;
    [SerializeField] private GameObject Camera;                                                                       // 将来的に複数のカメラの中からアクティブなもの一つを選ぶことになる
    [SerializeField] private float Speed_Move = 8.0f;
    [SerializeField] private float RotateSpeed = 20.0f;
    [SerializeField] private float Speed_Fall = 4.0f;
    [SerializeField] private float Speed_Climb = 4.0f;
    [SerializeField] private float Height_Climb_Block = 2.3f;
    [SerializeField] private float Height_Climb_Stage = 0.75f;//1.8f;
    [SerializeField] private float GoLength_AfterClimbing = 0.5f;
    [SerializeField] private float Rotate_Tolerance = 0.1f;
    [SerializeField] private float Camera_DistanceTolerance = 100;
    private Vector3 Position_Latest_m;
    private Vector3 StartPosition = new Vector3(0, 0, 0);

    public bool is_block = false;
    public bool is_stage = false;

    // 初期化
    void Start()
    {
        // Rigidbody取得
        Rigid = this.GetComponent<Rigidbody>();
        // 過去の位置
        Position_Latest_m = this.transform.position;

        // カメラ未設定時
        if (!Camera) Debug.Log("【miya_player_move】there is no camera");

        Pipe1 = GameObject.Find("FloorOne");
        Pipe2 = GameObject.Find("FloorTwo");
        Pipe3 = GameObject.Find("FloorThree");
    }

    void Update()
    {
        len = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
        if (len >= 12.0f)
        {
            transform.SetParent(Pipe3.transform);
        }
        else if (len >= 8.5f)
        {
            transform.SetParent(Pipe2.transform);
        }
        else
        {
            transform.SetParent(Pipe1.transform);
        }
    }

    // 定期更新
    void FixedUpdate()
    {
        if (IsUnder_m)
        {
            Rigid.AddForce(new Vector3(0, 0.15f, 0));
        }

        // 情報
        Vector3 difference = this.transform.position - Position_Latest_m;
        Position_Latest_m = this.transform.position;

        // どっち
        if (sc_state.Get_IsBlock())
        {
            is_block = true;
            is_stage = false;
        }
        if (sc_state.Get_IsStage())
        {
            is_block = false;
            is_stage = true;
        }

        // カメラベクトル取得
        Vector3 distance = this.transform.position - Camera.transform.position; distance.y = 0;
        Vector3 camera_front;
        Vector3 camera_right;
        if (distance.magnitude < Camera_DistanceTolerance)
        {
            camera_front = Camera.transform.forward;
            camera_right = Camera.transform.right;
        }
        else
        {
            camera_front = distance;
            camera_right = Quaternion.Euler(0, 90, 0) * camera_front;
        }

        // アクション可能
        if (sc_state.Get_CanAction())
        {
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
                    direction_move = direction_move.normalized;// * Time.deltaTime;
                }

                // 移動//進行方向にオブジェクトがあったら法線方向へ回転
                Rigid.velocity = direction_move * Speed_Move;

                // 落下
                if (difference.y < -0.003f)
                {
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.HOVERING);
                    Rigid.velocity = new Vector3(direction_move.x, -Speed_Fall, direction_move.z);
                }
                else if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.HOVERING)
                {
                    // 着地
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.WAITING);
                }

                // 回転
                if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.WALKING)
                {
                    // 制御
                    difference.y = 0;

                    if (difference.magnitude > Rotate_Tolerance)
                    {
                        // 回転計算
                        Quaternion rot = Quaternion.LookRotation(direction_move);
                        rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed);
                        this.transform.rotation = rot;
                    }//difference.magnitude > Rotate_Tolerance
                }//sc_state.Get_AnimationState() == (int)miya_player_state.e_PlayerAnimationState.WALKING
            }//移動
        }//sc_state.Get_CanAction()
        else
        {
            // ブロック押す
            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.PUSH_PUSHING)
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
                    direction_move = direction_move.normalized;// * Time.deltaTime;
                }

                // 移動//進行方向にオブジェクトがあったら法線方向へ回転
                Rigid.velocity = direction_move * Speed_Move * 1.0f;

                // 回転
                // 制御
                difference.y = 0;

                if (difference.magnitude > Rotate_Tolerance * 0.0f) //*0.5f
                {
                    // 回転計算
                    Quaternion rot = Quaternion.LookRotation(direction_move);
                    rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed * 0.5f);
                    this.transform.rotation = rot;
                }//difference.magnitude > Rotate_Tolerance
            }//ブロック押す

            // よじ登る
            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.CLIMBING)
            {
                // ブロック
                if (is_block)
                {
                    if (this.transform.position.y < StartPosition.y + Height_Climb_Block)
                    {
                        Rigid.velocity = new Vector3(0, Speed_Climb, 0);
                    }
                    else
                    {
                        Vector3 length = this.transform.position - StartPosition; length.y = 0;
                        if (length.magnitude < GoLength_AfterClimbing && true)// 秒数を指定してバグを回避
                        {
                            Rigid.velocity = this.transform.forward;
                        }
                        // 終了
                        else
                        {
                            sc_state.Set_CanAction(true);
                            Rigid.useGravity = true;

                            sc_state.Set_IsBlock(false);
                        }
                    }
                }
                // ステージ
                if (is_stage)
                {

                    if (this.transform.position.y < StartPosition.y + Height_Climb_Stage)
                    {
                        Rigid.velocity = new Vector3(0, Speed_Climb, 0);
                    }
                    else
                    {
                        Vector3 length = this.transform.position - StartPosition; length.y = 0;
                        if (length.magnitude < GoLength_AfterClimbing && true)// 秒数を指定してバグを回避
                        {
                            Rigid.velocity = this.transform.forward;
                        }
                        // 終了
                        else
                        {
                            sc_state.Set_CanAction(true);
                            Rigid.useGravity = true;

                            sc_state.Set_IsStage(false);
                        }
                    }
                }
            }

            //橋によるワープ移動（向き変更）
            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.BRIDGE_SET)
            {
                Actcount--;

                transform.Rotate(0, Act_spin, 0);

                if (Actcount == 0)
                {
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.BRIDGE_IN);
                    Actcount = 100;
                    //物理挙動による移動の無効化
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                    Size = 1.0f;
                    rot_z = 1.0f;

                    Act_move = (Gatepoint - transform.position) / 50;
                }
            }

            //橋によるワープ移動（吸い込み）
            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.BRIDGE_IN)
            {
                Actcount--;

                transform.Rotate(0, 0, rot_z);

                rot_z += 1.5f;

                if (Actcount <= 50)
                {
                    if (Actcount > 25)
                    {
                        Size -= 0.04f;
                    }

                    transform.localScale = new Vector3(Size, Size, Size);
                    transform.position += Act_move;
                }

                if (Actcount == 0)
                {
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.BRIDGE_MOVE);
                    transform.position = Gatepoint;
                    Actcount = 70;

                    Act_move = (bridge.Getpair_pos() - transform.position) / 50;

                    Quaternion angle = Quaternion.identity;

                    angle.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
                    transform.rotation = angle;

                    //出現時の向きを合わせる
                    Vector3 View = bridge.Getpair_pos();
                    View.y = transform.position.y;
                    this.transform.LookAt(View);
                    Player_Forword.PosReset();
                    Player_Check.PosReset();
                    Player_Under.PosReset();
                }
            }

            //橋によるワープ移動（移動）
            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.BRIDGE_MOVE)
            {
                Actcount--;


                if (Actcount <= 50)
                {
                    transform.position += Act_move;
                }


                if (Actcount == 0)
                {
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.BRIDGE_POP);
                    Act_move = Act_move.normalized;

                    transform.position += Act_move * 0.5f;

                    Actcount = 100;
                    pop_y = 0.2f;
                }
            }

            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.BRIDGE_POP)
            {
                Actcount--;

                if(Actcount == 50)
                {
                    Size = 0;
                }

                if (Actcount <= 50)
                {
                    transform.position += (Act_move * 0.03f);
                    Size += 0.02f;
                    transform.localScale = new Vector3(Size, Size, Size);

                    Vector3 pos = transform.position;
                    pos.y += pop_y;

                    transform.position = pos;

                    pop_y -= 0.008f;

                }


                if (Actcount == 0)
                {
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.WAITING);

                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    this.gameObject.GetComponent<BoxCollider>().enabled = true;

                    Size = 1.0f;
                    transform.localScale = new Vector3(Size, Size, Size);
                    sc_state.Set_CanAction(true);

                    Player_Forword.PosReset();
                    Player_Check.PosReset();
                    Player_Under.PosReset();
                }
            }

            /////////////////////
            ///
            //額縁によるワープ移動（向き変更）
            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.DOOR_SET)
            {
                Actcount--;

                transform.Rotate(0, Act_spin, 0);

                if (Actcount == 0)
                {
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.DOOR_IN);
                    Actcount = 100;
                    //物理挙動による移動の無効化
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                    Size = 1.0f;
                    rot_z = 1.0f;

                    Act_move = (Gatepoint - transform.position) / 50;
                }
            }

            //額縁によるワープ移動（吸い込み）
            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.DOOR_IN)
            {
                Actcount--;

                transform.Rotate(0, 0, rot_z);

                rot_z += 1.5f;

                if (Actcount <= 50)
                {
                    if (Actcount > 25)
                    {
                        Size -= 0.04f;
                    }

                    transform.localScale = new Vector3(Size, Size, Size);
                    transform.position += Act_move;
                }

                if (Actcount == 0)
                {
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.DOOR_POP);
                    transform.position = Gatepoint;
                    Actcount = 70;

                    Act_move = (bridge.Getpair_pos() - transform.position) / 50;

                    Quaternion angle = Quaternion.identity;

                    angle.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
                    transform.rotation = angle;

                    //出現時の向きを合わせる
                    Vector3 View = bridge.Getpair_pos();
                    View.y = transform.position.y;
                    this.transform.LookAt(View);
                    Player_Forword.PosReset();
                    Player_Check.PosReset();
                    Player_Under.PosReset();
                }
            }

            if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.DOOR_POP)
            {
                Actcount--;

                if (Actcount == 50)
                {
                    Size = 0;
                }

                if (Actcount <= 50)
                {
                    transform.position += (Act_move * 0.03f);
                    Size += 0.02f;
                    transform.localScale = new Vector3(Size, Size, Size);

                    Vector3 pos = transform.position;
                    pos.y += pop_y;

                    transform.position = pos;

                    pop_y -= 0.008f;

                }

                if (Actcount == 0)
                {
                    sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.WAITING);

                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    this.gameObject.GetComponent<BoxCollider>().enabled = true;

                    Size = 1.0f;
                    transform.localScale = new Vector3(Size, Size, Size);
                    sc_state.Set_CanAction(true);

                    Player_Forword.PosReset();
                    Player_Check.PosReset();
                    Player_Under.PosReset();
                }
            }

        }
    }//FixedUpdate

    public void Set_StartPosition(Vector3 _start)
    {
        StartPosition = _start;
    }

    public void Set_IsUnder(bool _is)
    {
        IsUnder_m = _is;
    }

    public void Set_Act_spin()
    {
        //　 対象の方向
        Vector3 Direction = Gimmikpoint - transform.position;
        float sub_y = Direction.y;

        Direction.y = 0;

        Vector3 forward = transform.forward;

        forward.y = 0;

        var axis = Vector3.Cross(forward, Direction);   //どっち向き？
        var angle = Vector3.Angle(forward, Direction);  //角度（大きさだけ）

        if(axis.y > 0)
        {
            Act_spin = angle / 50;
        }
        else
        {
            Act_spin = -angle / 50; 
        }

        Actcount = 50;

    }


    //オブジェクトからの当たり判定操作

    public void SetHIT_TOWER(Vector3 pos)
    {
        if (CheckView(pos))
        {
            HIT_TOWER = true;
            sc_state.Set_IsTower(HIT_TOWER);
            Gimmikpoint = pos;
        }
        else
        {
            ClearHIT_TOWER();
        }
    }

    public void ClearHIT_TOWER()
    {
        HIT_TOWER = false;
        sc_state.Set_IsTower(HIT_TOWER);
    }

    public void SetHIT_LEVER(Vector3 pos)
    {
        if(CheckView(pos))
        {
            HIT_LEVER = true;
            sc_state.Set_IsLever(HIT_LEVER);
            Gimmikpoint = pos;
        }
        else
        {
            ClearHIT_LEVER();
        }
    }

    public void SetHIT_LEVER2(Vector3 pos)
    {
        /*
        if (CheckView(pos))
        {
            HIT_LEVER2 = true;
            sc_state.Set_IsLever(HIT_LEVER2);
            Gimmikpoint = pos;
        }
        else
        {
            ClearHIT_LEVER2();
        }
        */
    }

    public bool SetHIT_Bridge(Vector3 pos)
    {
        if (CheckView(pos))
        {
            HIT_BRIDGE = true;
            sc_state.Set_IsBridge(HIT_BRIDGE);
            Gimmikpoint = pos;
            return true;
        }
        else
        {
            ClearHIT_BRIDGE();
        }
        return false;
    }

    public bool SetHIT_Door(Vector3 pos)
    {
        if (CheckView(pos))
        {
            HIT_DOOR = true;
            sc_state.Set_IsDoor(HIT_DOOR);
            Gimmikpoint = pos;
            return true;
        }
        else
        {
            ClearHIT_DOOR();
        }
        return false;
    }

    public void ClearHIT_LEVER()
    {
        HIT_LEVER = false;
        sc_state.Set_IsLever(HIT_LEVER);
    }

    public void ClearHIT_LEVER2()
    {
        //HIT_LEVER2 = false;
        //sc_state.Set_IsLever(HIT_LEVER2);
    }

    public void ClearHIT_BRIDGE()
    {
        HIT_BRIDGE = false;
        sc_state.Set_IsBridge(HIT_BRIDGE);
    }

    public void ClearHIT_DOOR()
    {
        HIT_DOOR = false;
        sc_state.Set_IsDoor(HIT_DOOR);
    }

    public void SetHIT_LEVER_BACK()
    {
        HIT_LEVER_BACK = true;
    }

    public void ClearHIT_LEVER_BACK()
    {
        HIT_LEVER_BACK = false;
    }

    public bool Check_Bridge()
    {
        return bridge.GetUse();
    }



    bool CheckView(Vector3 pos)
    {
        //　 対象の方向
        Vector3 Direction = pos - transform.position;
        float sub_y = Direction.y;

        Direction.y = 0;

        Vector3 forward = transform.forward;

        forward.y = 0;

        var angle = Vector3.Angle(forward, Direction);

        //　サーチする角度内だったら発見している
        if (angle <= searchAngle)
        {
            return true;
        }

        return false;
    }

    public void SetGate(Vector3 pos)
    {
        Gatepoint = pos;
    }

    public void UseLever()
    {
        if (HIT_LEVER)
        {
            leba.SpinL();
        }
        /*
        if (HIT_LEVER2)
        {
            leba_2.SpinL();
        }
        */
    }

    public void UseLever_inv()
    {
        if (HIT_LEVER)
        {
            leba.SpinR();
        }

        /*
        if (HIT_LEVER2)
        {
            leba_2.SpinR();
        }
        */
    }
    

    //オブジェクトを発見した際にスクリプトを獲得する
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TOWER"))
        {
            TOWER = other.GetComponent<TOWER>();
        }

        if (other.gameObject.CompareTag("LEVER"))
        {
            leba = other.GetComponent<Leba>();
        }

        if (other.gameObject.CompareTag("LEVER_BACK"))
        {
            //leba_2 = other.GetComponent<leba_2>();
        }

        if (other.gameObject.CompareTag("Bridge_HIT"))
        {
            bridge = other.GetComponent<Bridge_HIT>().GetBridge();
        }

        if (other.gameObject.CompareTag("Door_HIT"))
        {
            door = other.GetComponent<Door_HIT>().GetDoor();
        }
    } 
}
