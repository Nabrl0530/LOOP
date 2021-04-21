using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_move : MonoBehaviour
{
	// 変数
	[SerializeField] private GameObject Camera;                                                                       // 将来的に複数のカメラの中からアクティブなもの一つを選ぶことになる
	[SerializeField] private float Speed_Move = 2.0f;
	Rigidbody Rigid;

	// 初期化
	void Start()
	{
		// Rigidbody取得
		Rigid = this.GetComponent<Rigidbody>();

		// カメラ未設定時
		if ( !Camera ) Debug.Log("【miya_player_move】there is no camera");
	}

	// 定期更新
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
}
