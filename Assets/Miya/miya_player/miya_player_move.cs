using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_move : MonoBehaviour
{
	// 参照
	public miya_player_state sc_state;

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

	private bool is_block = false;
	private bool is_stage = false;

	private int Frame_Climb_m = 0;
	[SerializeField] private float SECOND_FOR_CLEAR_BUG = 1.0f;

	bool IsUnder_m = false;


	// 初期化
	void Start()
	{
		// Rigidbody取得
		Rigid = this.GetComponent<Rigidbody>();
		// 過去の位置
		Position_Latest_m = this.transform.position;

		// カメラ未設定時
		if (!Camera) Debug.Log("【miya_player_move】there is no camera");

		// 初期化
		IsUnder_m = false;
	}

	// 定期更新
	void FixedUpdate()
	{
		if ( IsUnder_m ) Rigid.AddForce(new Vector3(0, 0.1f, 0));

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

				// 移動
				Rigid.velocity = direction_move * Speed_Move;

				// 落下
				if (difference.y < -0.003f)
				{
					sc_state.Set_AnimationState(miya_player_state.e_PlayerAnimationState.HOVERING);
					Rigid.velocity = new Vector3(direction_move.x, -Speed_Fall, direction_move.z);
				}
				else if (sc_state.Get_AnimationState() == (int)miya_player_state.e_PlayerAnimationState.HOVERING)
				{
					// 着地
					sc_state.Set_AnimationState(miya_player_state.e_PlayerAnimationState.WAITING);
				}

				// 回転
				if (sc_state.Get_AnimationState() == (int)miya_player_state.e_PlayerAnimationState.WALKING)
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
			if (sc_state.Get_AnimationState() == (int)miya_player_state.e_PlayerAnimationState.PUSH_PUSHING)
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

				// 移動
				Rigid.velocity = direction_move * Speed_Move * 0.5f;

				// 回転
				// 制御
				difference.y = 0;

				if (difference.magnitude > Rotate_Tolerance * 0.5f)
				{
					// 回転計算
					Quaternion rot = Quaternion.LookRotation(direction_move);
					rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed * 0.5f);
					this.transform.rotation = rot;
				}//difference.magnitude > Rotate_Tolerance
			}//ブロック押す

			// よじ登る
			if (sc_state.Get_AnimationState() == (int)miya_player_state.e_PlayerAnimationState.CLIMBING)
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
						if (length.magnitude < GoLength_AfterClimbing && Frame_Climb_m < SECOND_FOR_CLEAR_BUG * 50)
						{
							Rigid.velocity = this.transform.forward;
						}
						// 終了
						else
						{
							sc_state.Set_CanAction(true);
							Rigid.useGravity = true;

							sc_state.Set_IsBlock(false);

							Frame_Climb_m = 0;
						}
						
						Frame_Climb_m++;
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
						if (length.magnitude < GoLength_AfterClimbing && Frame_Climb_m < SECOND_FOR_CLEAR_BUG * 50)
						{
							Rigid.velocity = this.transform.forward;
						}
						// 終了
						else
						{
							sc_state.Set_CanAction(true);
							Rigid.useGravity = true;

							sc_state.Set_IsStage(false);

							Frame_Climb_m = 0;
						}

						Frame_Climb_m++;
					}
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

	////オブジェクトが触れている間
	//void OnCollisionStay(Collision collision)
	//{
	//	Debug.Log("Hiting");
	//}
}
