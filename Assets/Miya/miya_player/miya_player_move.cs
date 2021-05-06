using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_move : MonoBehaviour
{
	// 変数
	[SerializeField] private GameObject Camera;                                                                       // 将来的に複数のカメラの中からアクティブなもの一つを選ぶことになる
	[SerializeField] private float Speed_Move = 2.0f;
	[SerializeField] private float Speed_Fall = 4.0f;
	Rigidbody Rigid;
	private Vector3 Position_Latest_m;
	public float RotateSpeed = 5.0f;

	// 初期化
	void Start()
	{
		// Rigidbody取得
		Rigid = this.GetComponent<Rigidbody>();
		// 過去の位置
		Position_Latest_m = this.transform.position;

		// カメラ未設定時
		if ( !Camera ) Debug.Log("【miya_player_move】there is no camera");
	}

	// 定期更新
	void FixedUpdate()
	{
		// カメラベクトル取得
		Vector3 camera_front = Camera.transform.forward;
		Vector3 camera_right = Camera.transform.right;

		// 移動//カメラの外側の時に不自然だから直す
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

			// 回転
			Vector3 difference = this.transform.position - Position_Latest_m;
			Position_Latest_m = this.transform.position;
			if (difference.magnitude > 0.001f)
			{
				// 落下判定
				//miya_player_stateのm_AnimationStateをHOVERINGに設定

				// 落下
				Rigid.velocity = new Vector3(direction_move.x, -Speed_Fall, direction_move.z);

				// 制御
				difference.y = 0;

				// 回転計算
				Quaternion rot = Quaternion.LookRotation(difference);
				rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed);
				this.transform.rotation = rot;
			}
		}
	}
}
