using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_camera_move : MonoBehaviour
{
	// 定数
	const float HEIGHT_MAX = 17.5f;
	const float HEIGHT_MIN = 4.0f;

	// 変数
	bool	Looking_FromUp_m	= false;
	float	Length_FromCenter	= 0;
	float	Length_FromCenter_Current = 0;
	[SerializeField] private float Speed_Rotate = 60.0f;
	//[SerializeField] private float Speed_Height = 2.0f;
	float	Degree			= -180;
	float	Height_Default	= 0;
	float	Height			= 0;

    // 初期化
    void Start()
    {
		// 初期値取得
		Length_FromCenter = Mathf.Abs(this.transform.position.z);
		Length_FromCenter_Current = Length_FromCenter;
		Height_Default = this.transform.position.y;
		Height = Height_Default;
	}

	// 更新
	void Update()
	{
		// 入力
		if (Input.GetKey(KeyCode.LeftArrow	)) Degree += Speed_Rotate * Time.deltaTime;
		if (Input.GetKey(KeyCode.RightArrow	)) Degree -= Speed_Rotate * Time.deltaTime;

		// 移動
		if ( !Looking_FromUp_m )
		{
			// 見下ろし視点へ切替
			if (Input.GetKey(KeyCode.UpArrow	)) Looking_FromUp_m = true;
			// 過去可変
			//if (Input.GetKey(KeyCode.UpArrow	)) Height += Speed_Height * Time.deltaTime;
			//if (Height > HEIGHT_MAX - 0.1f) Height = HEIGHT_MAX - 0.1f;

			// 移動
			Vector3 result = new Vector3(0, 0, 0);
			result.x = Mathf.Sin(Degree * Mathf.Deg2Rad) * Length_FromCenter;
			result.z = Mathf.Cos(Degree * Mathf.Deg2Rad) * Length_FromCenter;
			result.y = Height_Default;
			this.transform.position = result;
		}
		else
		{
			// 高さ
			Height = HEIGHT_MAX - 0.1f;

			// 高さの変更に伴う中央からの距離変更
			float degree = Height * 90.0f / HEIGHT_MAX;// 0.0f~10.0f = 0°~90°
			Length_FromCenter_Current = Mathf.Cos(degree * Mathf.Deg2Rad) * Length_FromCenter;// 90°= 0.0f

			// 移動
			Vector3 result = new Vector3(0, 0, 0);
			result.x = Mathf.Sin(Degree * Mathf.Deg2Rad) * Length_FromCenter_Current;
			result.z = Mathf.Cos(Degree * Mathf.Deg2Rad) * Length_FromCenter_Current;
			result.y = Height;
			this.transform.position = result;

			// 通常視点へ切替
			if (Input.GetKey(KeyCode.DownArrow)) Looking_FromUp_m = false;
			// 過去可変
			//if (Input.GetKey(KeyCode.DownArrow)) Height -= Speed_Height * Time.deltaTime;
		}
	}

    // 定期更新
    void FixedUpdate()
    {
		
	}
}
