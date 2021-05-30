using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    // 参照
    public Player sc_move;
    public Player_Forword sc_forword;
    public Player_Check sc_check;
    public Animator animator;

    // 列挙
    public enum e_PlayerAnimationState
    {
        WAITING,        // 待機
        WALKING,        // 歩き
        ABANDONED,      // 放置
        RUNNING,        // 走る
        CLIMBING,       // よじ登る
        PUSH_WAITING,   // 押す待機
        PUSH_PUSHING,   // 押す
        PULL_WAITING,   // 引く待機
        PULL_PULLING,   // 引く
        LEVER_WAITING,  // レバー待機
        LEVER_RIGHT,    // レバー右
        LEVER_LEFT,     // レバー左
        HOVERING,       // 空中
        LANDING,        // 着地
        BRIDGE_SET,     // 橋によるワープ向き変更
        BRIDGE_IN,      // 橋によるワープ吸い込み
        BRIDGE_MOVE,    // 橋によるワープ移動
        BRIDGE_POP,     // 橋によるワープ再出現
        DOOR_SET,       // 橋によるワープ向き変更
        DOOR_IN,        // 橋によるワープ吸い込み
        DOOR_POP,       // 橋によるワープ再出現
        BLOCK_MOVE,     // ブロックの規定位置への移動
        BLOCK_LOOK,     // ブロック側を向く
        PUSH_PUSHING_INV,   // 押す逆再生
    }

    // 変数
    Rigidbody Rigid;
    public int m_AnimationState = (int)e_PlayerAnimationState.WAITING;  //状態ステート
    public int m_AnimationState_Motion = (int)e_PlayerAnimationState.WAITING;   //実アニメーションステート
    public bool m_CanAction = true;
    //bool	m_IsClockwise		= true;
    public bool m_CanClimb_forword = false;
    public bool m_CanClimb_check = false;
    public bool IsBlock = false;
    public bool IsStage = false;
    bool IsRunning = false;

    public GameObject m_parent;

    //俺が追加
    public bool IsLever = false;
    public bool IsTower = false;
    public bool IsBridge = false;
    public bool IsDoor = false;
    bool Clear;

    int wait_Act;
    int wait_key;

    // デバッグ用
    int state_past = (int)e_PlayerAnimationState.WAITING;

    // 初期化
    void Start()
    {
        // Rigidbody取得
        Rigid = this.GetComponent<Rigidbody>();
        wait_Act = 0;
        wait_key = 0;
        Clear = false;
    }

    // 更新
    void Update()
    {
        if(Clear)
        {
            return;
        }

        // デバッグ
        if (state_past != m_AnimationState)
        {
            state_past = m_AnimationState;
            //Debug.Log("Animation State：" + m_AnimationState);
        }

        //行動規制中は入力禁止
        if(wait_Act > 0 || wait_key > 0)
        {
            Debug.Log(m_AnimationState);
            animator.SetInteger("state", m_AnimationState_Motion);
            return;
        }

        // アクション可能
        if (m_CanAction)
        {
            // 何もしていない
            m_AnimationState = (int)e_PlayerAnimationState.WAITING;
            m_AnimationState_Motion = (int)e_PlayerAnimationState.WAITING;

            // 歩行
            if
            (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                // 原田君用3変更
                if (!IsRunning)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.WALKING;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.WALKING;
                }
                else
                {
                    m_AnimationState = (int)e_PlayerAnimationState.RUNNING;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.RUNNING;
                }
            }
            else if (Mathf.Abs(Input.GetAxis("Vertical_p")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal_p")) > 0)
            {
                // 原田君用3変更
                if (!IsRunning)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.WALKING;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.WALKING;
                }
                else
                {
                    m_AnimationState = (int)e_PlayerAnimationState.RUNNING;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.RUNNING;
                }
            }

            // デバッグ
            //Debug.Log("F : " + m_CanClimb_forword);
            //Debug.Log("C : " + m_CanClimb_check);

            // よじ登る
            if (Input.GetKey(KeyCode.Space) || Input.GetButton("Climb"))
            {
                // 登れるものがあれば
                if (m_CanClimb_forword && !m_CanClimb_check)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.CLIMBING;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.CLIMBING;
                    m_CanAction = false;

                    //ワープなので不要
                    //Rigid.useGravity = false;

                    sc_move.Set_StartPosition(this.transform.position);
                }
            }

            // 作動
            if (Input.GetKey(KeyCode.J) || Input.GetButton("OK"))// Aボタン
            {
                // 対象によってステート変更
                // ブロック
                if (IsBlock)
                {
                    //m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
                    m_AnimationState = (int)e_PlayerAnimationState.BLOCK_MOVE;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.WALKING;
                    sc_move.Set_ActMove_Block();
                    m_CanAction = false;
                    //sc_move.Block_Catch();
                    //sc_move.Set_Catch();

                    // ブロックをプレイヤーの子に
                    //if (sc_forword.Get_Block())
                    //{
                    /*
                    sc_forword.Get_Block().transform.parent = this.transform;
                    sc_forword.Get_Block().GetComponent<BoxCollider>().size = new Vector3(2.2f, 1.8f, 2.2f);
                    //sc_forword.Get_Block().GetComponent<Rigidbody>().useGravity = false;
                    sc_forword.Get_Block().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    sc_forword.Get_Block().GetComponent<Rigidbody>().mass =1;
                    sc_move.Block_Catch();
                    */

                    sc_move.MOVE_STOP();    //速度完全キャンセル
                    /*
                    m_parent.GetComponent<Player_Axis>().LookConect(sc_move.GetForward());  //向きをプレイヤーと同期
                    m_parent.GetComponent<Player_Axis>().SetPosition(this.transform.position + sc_move.GetForward());   //位置をプレイヤーの前方に
                    m_parent.GetComponent<Player_Axis>().Set_View(sc_move.GetLastDirection());
                    m_parent.GetComponent<Player_Axis>().SetUse(true);

                    sc_forword.Get_Block().transform.parent = this.transform;
                    // プレイヤーを中心軸の子に
                    this.transform.parent = m_parent.transform;
                    */
                    //}

                }
                else if (IsLever)
                {
                    sc_move.UseLever();
                }
                else if (IsBridge)
                {
                    if (sc_move.Check_Bridge())
                    {
                        m_AnimationState = (int)e_PlayerAnimationState.BRIDGE_SET;
                        m_AnimationState_Motion = (int)e_PlayerAnimationState.WALKING;
                        m_CanAction = false;
                        sc_move.Set_Act_spin();
                    }
                }
                else if (IsDoor)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.DOOR_SET;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.WALKING;
                    m_CanAction = false;
                    sc_move.Set_Act_spin();
                }
            }

            if (Input.GetKey(KeyCode.I))// Xボタン
            {
                // 対象によってステート変更

                if (IsLever)
                {
                    sc_move.UseLever_inv();
                }
            }


        }//m_CanAction
        else
        {
            // 押す
            if (m_AnimationState == (int)e_PlayerAnimationState.PUSH_WAITING)
            {
                if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                    Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    m_AnimationState = (int)e_PlayerAnimationState.PUSH_PUSHING;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.PUSH_PUSHING;
                }
                else if (Mathf.Abs(Input.GetAxis("Vertical_p")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal_p")) > 0)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.PUSH_PUSHING;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.PUSH_PUSHING;
                }
            }
            else if (m_AnimationState == (int)e_PlayerAnimationState.PUSH_PUSHING)
            {
                if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
                    !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) &&
                    Mathf.Abs(Input.GetAxis("Vertical_p")) == 0 && 
                    Mathf.Abs(Input.GetAxis("Horizontal_p")) == 0)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
                    m_AnimationState_Motion = (int)e_PlayerAnimationState.PUSH_WAITING;
                }
            }
        }

        // キャンセル
        if (Input.GetKey(KeyCode.K) || Input.GetButton("NO"))// Bボタン
        {
            // 該当動作チェック
            if
            (
                m_AnimationState == (int)e_PlayerAnimationState.PUSH_WAITING ||
                m_AnimationState == (int)e_PlayerAnimationState.PUSH_PUSHING ||
                m_AnimationState == (int)e_PlayerAnimationState.PULL_WAITING ||
                m_AnimationState == (int)e_PlayerAnimationState.PULL_PULLING ||
                m_AnimationState == (int)e_PlayerAnimationState.LEVER_WAITING ||
                m_AnimationState == (int)e_PlayerAnimationState.LEVER_RIGHT ||
                m_AnimationState == (int)e_PlayerAnimationState.LEVER_LEFT
            )
            {
                m_AnimationState = (int)e_PlayerAnimationState.WAITING;
                m_AnimationState_Motion = (int)e_PlayerAnimationState.WAITING;
                m_CanAction = true;

                if (sc_move.Get_Catch())
                {
                    sc_forword.Get_Block().transform.parent = null;
                    sc_forword.Get_Block().GetComponent<BoxCollider>().size = new Vector3(2.2f, 2.2f, 2.2f);
                    //sc_forword.Get_Block().GetComponent<Rigidbody>().useGravity = true;
                    sc_forword.Get_Block().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    sc_forword.Get_Block().GetComponent<Rigidbody>().mass = 2000;
                    sc_move.Block_relase();
                    sc_move.Clare_Catch();
                    m_parent.GetComponent<Player_Axis>().SetUse(false);
                }
            }
        }
        Debug.Log(m_AnimationState);
        animator.SetInteger("state", m_AnimationState_Motion);
    }

    // 定期更新
    void FixedUpdate()
    {
        if(wait_Act > 0)
        {
            wait_Act--;
            if(wait_Act == 0)
            {
                m_CanAction = true;
            }
        }

        if(wait_key > 0)
        {
            wait_key--;
        }
    }

    public void Set_CanAction(bool _can)
    {
        m_CanAction = _can;
    }

    public void Set_End_Act(int C)
    {
        wait_Act = C;
    }

    public void Set_Wait_key(int C)
    {
        wait_key = C;
    }

    public void Set_AnimationState(e_PlayerAnimationState _state)
    {
        m_AnimationState = (int)_state;
    }

    public void Set_Motion(e_PlayerAnimationState _state)
    {
        m_AnimationState_Motion = (int)_state;
    }
    public void Set_CanClimb_Forword(bool _can)
    {
        m_CanClimb_forword = _can;
    }
    public void Set_CanClimb_Check(bool _can)
    {
        m_CanClimb_check = _can;
    }
    public void Set_IsBlock(bool _is)
    {
        IsBlock = _is;
    }
    public void Set_IsStage(bool _is)
    {
        IsStage = _is;
    }

    public void Set_IsLever(bool _is)
    {
        IsLever = _is;
    }

    public void Set_IsTower(bool _is)
    {
        IsTower = _is;
    }

    public void Set_IsBridge(bool _is)
    {
        IsBridge = _is;
    }

    public void Set_IsDoor(bool _is)
    {
        IsDoor = _is;
    }

    public int Get_AnimationState()
    {
        return m_AnimationState;
    }
    public bool Get_CanAction()
    {
        return m_CanAction;
    }
    public bool Get_IsBlock()
    {
        return IsBlock;
    }
    public bool Get_IsStage()
    {
        return IsStage;
    }

    public void Set_IsRunning(bool _is)
    {
        IsRunning = _is;
    }

    public void Set_Clear()
    {
        Clear = true;
    }

    public void BlockUse()
    {
        m_parent.GetComponent<Player_Axis>().LookConect(sc_move.GetForward());  //向きをプレイヤーと同期
        m_parent.GetComponent<Player_Axis>().SetPosition(this.transform.position + sc_move.GetForward());   //位置をプレイヤーの前方に
        m_parent.GetComponent<Player_Axis>().Set_View(sc_move.GetLastDirection());
        m_parent.GetComponent<Player_Axis>().SetUse(true);

        sc_forword.Get_Block().transform.parent = this.transform;
        // プレイヤーを中心軸の子に
        this.transform.parent = m_parent.transform;
    }

    private void release_block()
    {

    }
}
