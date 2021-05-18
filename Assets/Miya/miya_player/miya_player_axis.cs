using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_axis : MonoBehaviour
{
	public GameObject m_Player = null;

    // Start is called before the first frame update
    void Start()
    {
		this.transform.position = m_Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
