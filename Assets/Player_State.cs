using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    // 参照
    public Player sc_move;
    public Player_Forword sc_forword;
    public Player_Check sc_check;

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
    }

    // 変数
    Rigidbody Rigid;
    public int m_AnimationState = (int)e_PlayerAnimationState.WAITING;
    public bool m_CanAction = true;
    //bool	m_IsClockwise		= true;
    bool m_CanClimb_forword = false;
    bool m_CanClimb_check = false;
    public bool IsBlock = false;
    public bool IsStage = false;

    //俺が追加
    public bool IsLever = false;
    public bool IsTower = false;
    public bool IsBridge = false;

    // デバッグ用
    int state_past = (int)e_PlayerAnimationState.WAITING;

    // 初期化
    void Start()
    {
        // Rigidbody取得
        Rigid = this.GetComponent<Rigidbody>();
    }

    // 更新
    void Update()
    {
        // デバッグ
        if (state_past != m_AnimationState)
        {
            state_past = m_AnimationState;
            Debug.Log("Animation State：" + m_AnimationState);
        }

        // アクション可能
        if (m_CanAction)
        {
            // 何もしていない
            m_AnimationState = (int)e_PlayerAnimationState.WAITING;

            // 歩行
            if
            (
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
            )
                m_AnimationState = (int)e_PlayerAnimationState.WALKING;

            // デバッグ
            //Debug.Log("F : " + m_CanClimb_forword);
            //Debug.Log("C : " + m_CanClimb_check);

            // よじ登る
            if (Input.GetKey(KeyCode.Space))
            {
                // 登れるものがあれば
                if (m_CanClimb_forword && !m_CanClimb_check)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.CLIMBING;
                    m_CanAction = false;

                    Rigid.useGravity = false;

                    sc_move.Set_StartPosition(this.transform.position);
                }
            }

            // 作動
            if (Input.GetKey(KeyCode.J))// Aボタン
            {
                // 対象によってステート変更
                // ブロック
                if (IsBlock)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
                    m_CanAction = false;

                    if (sc_forword.Get_Block())
                    {
                        sc_forword.Get_Block().transform.parent = this.transform;
                        sc_forword.Get_Block().GetComponent<BoxCollider>().size = new Vector3(2.2f,1.8f,2.2f);
                        //sc_forword.Get_Block().GetComponent<Rigidbody>().useGravity = false;
                        sc_forword.Get_Block().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    }

                }
                else if(IsLever)
                {
                    sc_move.UseLever();
                }
                else if(IsBridge)
                {
                    m_AnimationState = (int)e_PlayerAnimationState.BRIDGE_SET;
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
                if
                (
                Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
                )
                {
                    m_AnimationState = (int)e_PlayerAnimationState.PUSH_PUSHING;
                }
            }
            else if (m_AnimationState == (int)e_PlayerAnimationState.PUSH_PUSHING)
            {
                if
                (
                !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)
                )
                {
                    m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
                }
            }
        }

        // キャンセル
        if (Input.GetKey(KeyCode.K))// Bボタン
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
                m_CanAction = true;

                if (sc_forword.Get_Block())
                {
                    sc_forword.Get_Block().transform.parent = null;
                    sc_forword.Get_Block().GetComponent<BoxCollider>().size = new Vector3(2.2f, 2.2f, 2.2f);
                    //sc_forword.Get_Block().GetComponent<Rigidbody>().useGravity = true;
                    sc_forword.Get_Block().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                }
            }
        }
    }

    // 定期更新
    void FixedUpdate()
    {

    }

    public void Set_CanAction(bool _can)
    {
        m_CanAction = _can;
    }
    public void Set_AnimationState(e_PlayerAnimationState _state)
    {
        m_AnimationState = (int)_state;
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
}
