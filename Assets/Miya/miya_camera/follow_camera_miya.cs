using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// フォローカメラ
using Cinemachine;

public class follow_camera_miya : MonoBehaviour
{
	// フォローカメラ
	CinemachineVirtualCamera follow_camera;
	private CinemachineOrbitalTransposer _transposer;

	bool isMenu = false;
	public void Set_isMenu(bool _is) { isMenu = _is; }
	
	// Start is called before the first frame update
	void Start()
    {
		follow_camera = this.GetComponent<CinemachineVirtualCamera>();
		_transposer = follow_camera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
	}

    // Update is called once per frame
    void Update()
    {
        if ( isMenu )
		{
			_transposer.m_XAxis.m_InputAxisName = null;
		}
		else
		{
			_transposer.m_XAxis.m_InputAxisName = "Holizontal_c";
		}
    }
}
