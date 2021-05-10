using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_state : MonoBehaviour
{
	// 参照
	public miya_player_move sc_move;

	// 列挙
	public enum e_PlayerAnimationState
	{
		WAITING,		// 待機
		WALKING,		// 歩き
		ABANDONED,		// 放置
		RUNNING,		// 走る
		CLIMBING,		// よじ登る
		PUSH_WAITING,	// 押す待機
		PUSH_PUSHING,	// 押す
		PULL_WAITING,	// 引く待機
		PULL_PULLING,	// 引く
		LEVER_WAITING,	// レバー待機
		LEVER_RIGHT,	// レバー右
		LEVER_LEFT,		// レバー左
		HOVERING,		// 空中
		LANDING,		// 着地
	}
	// 変数
	Rigidbody Rigid;
	int		m_AnimationState	= (int)e_PlayerAnimationState.WAITING;
	bool	m_CanAction			= true;
	//bool	m_IsClockwise		= true;
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
		if ( m_CanAction )
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

			// よじ登る
			if (Input.GetKey(KeyCode.Space))
			{
				// 登れるものがあれば
				if (true)
				{
					m_AnimationState = (int)e_PlayerAnimationState.CLIMBING;
					m_CanAction = false;

					Rigid.useGravity = false;

					sc_move.Set_StartPosition(this.transform.position);
				}
			}

			// 作動
			if (Input.GetKey(KeyCode.J))// Aボダン
			{
				// 対象によってステート変更

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

	public int Get_AnimationState()
	{
		return m_AnimationState;
	}
	public bool Get_CanAction()
	{
		return m_CanAction;
	}
}
