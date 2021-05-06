using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_move : MonoBehaviour
{
	// 変数
	[SerializeField] private GameObject Camera;                                                                       // 将来的に複数のカメラの中からアクティブなもの一つを選ぶことになる
	[SerializeField] private float Speed_Move = 2.0f;
	Rigidbody Rigid;


    /// <ユンボム追加>
     
    //メニュー画面をon,offを管理する変数
    public bool UseMenu = false;

    //メニュー画面ののイメージオブジェクト
    [SerializeField]
    public GameObject Image_MenuWindow;
    //メニュー画面以外を半透明にするパンネルオブジェクト
    [SerializeField]
    public GameObject Image_PanelWindow;
    //カーソルの移動の待機時間
    public int WaitCount = 0;


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

            /// <ユンボム追加>

            WaitCount--;
            if(Input.GetKey(KeyCode.P) && !UseMenu && WaitCount < 0)
            {
                UseMenu = true;
                Image_PanelWindow.SetActive(true);
                Image_MenuWindow.SetActive(true);
                WaitCount = 30;
            }
            if (Input.GetKey(KeyCode.P) && UseMenu && WaitCount < 0)
            {
                UseMenu = false;
                Image_PanelWindow.SetActive(false);
                Image_MenuWindow.SetActive(false);
                WaitCount = 30;
            }

            /// <ユンボム追加>


            // 正規化
            if (direction_move != new Vector3(0, 0, 0))
			{
				// Y方向を削除
				direction_move.y = 0;
				direction_move = direction_move.normalized * Speed_Move;// * Time.deltaTime;
				//Debug.Log(direction_move);
			}

			// 移動
            if(!UseMenu)　//ユンボム追加
			Rigid.velocity = direction_move;
		}
	}
}
