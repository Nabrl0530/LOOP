using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_camera_move : MonoBehaviour
{
	// •Ï”
	float Degree = -180;
	public float Speed_Rotate = 30.0f;
	float Length_FromCenter;
	float Height;

    // ‰Šú‰»
    void Start()
    {
		// ‰Šú’læ“¾
		Length_FromCenter = Mathf.Abs(this.transform.position.z);
		Height = this.transform.position.y;
	}

    // ’èŠúXV
    void FixedUpdate()
    {
		// ˆÚ“®
		{
			// “ü—Í
			if (Input.GetKey(KeyCode.LeftArrow)	) Degree += Speed_Rotate * Time.deltaTime;
			if (Input.GetKey(KeyCode.RightArrow)) Degree -= Speed_Rotate * Time.deltaTime;

			// ˆÚ“®
			Vector3 result = new Vector3(0, 0, 0);
			result.x = Mathf.Sin(Degree * Mathf.Deg2Rad) * Length_FromCenter;
			result.z = Mathf.Cos(Degree * Mathf.Deg2Rad) * Length_FromCenter;
			result.y = Height;
			this.transform.position = result;
		}
	}
}
