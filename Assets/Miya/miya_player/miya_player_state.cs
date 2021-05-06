using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_state : MonoBehaviour
{
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
	int		m_AnimationState	= (int)e_PlayerAnimationState.WAITING;
	//bool	m_IsClockwise		= true;
	// デバッグ用
	int state_past = (int)e_PlayerAnimationState.WAITING;

	// 初期化
	void Start()
	{

	}

	// 更新
	void Update()
	{
		if (state_past != m_AnimationState)
		{
			state_past = m_AnimationState;
			Debug.Log("Animation State：" + m_AnimationState);
		}

		// 切替
		if
		(
		Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
		Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
		) 
		m_AnimationState = (int)e_PlayerAnimationState.WALKING;

		if (Input.GetKey(KeyCode.J))// Aボダン
		{
			// 対象によってステート変更

		}
		if (Input.GetKey(KeyCode.K))// Bボタン
		{
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

	// 定期更新
	void FixedUpdate()
	{

	}
}
