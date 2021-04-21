using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_camera_move : MonoBehaviour
{
	// 定数
	const float HEIGHT_MAX = 8.0f;
	const float HEIGHT_MIN = 4.0f;
	const float Length_FromCenter = 8.0f;

	// 変数
	[SerializeField] private float Speed_Rotate = 30.0f;
	[SerializeField] private float Speed_Height = 2.0f;
	float Degree = -180;
	float Length_FromCenter_Current;
	float Height;

    // 初期化
    void Start()
    {
		// 初期値取得
		Length_FromCenter_Current = Mathf.Abs(this.transform.position.z);
		Height = this.transform.position.y;
	}

	// 更新
	void Update()
	{
		// 移動
		{
			// 入力
			if (Input.GetKey(KeyCode.LeftArrow	)) Degree += Speed_Rotate * Time.deltaTime;
			if (Input.GetKey(KeyCode.RightArrow	)) Degree -= Speed_Rotate * Time.deltaTime;
			if (Input.GetKey(KeyCode.UpArrow	) && Height < HEIGHT_MAX) Height += Speed_Height * Time.deltaTime;
			if (Height > HEIGHT_MAX - 0.1f) Height = HEIGHT_MAX - 0.1f;
			if (Input.GetKey(KeyCode.DownArrow	) && Height > HEIGHT_MIN) Height -= Speed_Height * Time.deltaTime;

			// 高さの変更に伴う中央からの距離変更
			float degree = Height * 90.0f / HEIGHT_MAX;// 0.0f~10.0f = 0°~90°
			Length_FromCenter_Current = Mathf.Cos(degree * Mathf.Deg2Rad) * Length_FromCenter;// 90°= 0.0f

			// 移動
			Vector3 result = new Vector3(0, 0, 0);
			result.x = Mathf.Sin(Degree * Mathf.Deg2Rad) * Length_FromCenter_Current;
			result.z = Mathf.Cos(Degree * Mathf.Deg2Rad) * Length_FromCenter_Current;
			result.y = Height;
			this.transform.position = result;
		}
	}

    // 定期更新
    void FixedUpdate()
    {
		
	}
}
