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
	string name;

	bool isMenu = false;
	public void Set_isMenu(bool _is) { isMenu = _is; }
	
	// Start is called before the first frame update
	void Start()
    {
		follow_camera = this.GetComponent<CinemachineVirtualCamera>();
		_transposer = follow_camera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
		name = _transposer.m_XAxis.m_InputAxisName;
	}

    // Update is called once per frame
    void Update()
    {
        if ( isMenu )
		{
			_transposer.m_XAxis.m_InputAxisName = null;
			_transposer.m_XAxis.m_InputAxisValue = 0;
		}
		else
		{
			_transposer.m_XAxis.m_InputAxisName = name;
		
			if (Mathf.Abs(_transposer.m_XAxis.m_InputAxisValue) < 0.05)
			{
				_transposer.m_XAxis.m_InputAxisValue = 0;
			}
		}
    }
}
