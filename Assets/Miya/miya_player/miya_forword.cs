using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_forword : MonoBehaviour
{
	// éQè∆
	public miya_player_state	sc_state;
	public miya_player_move		sc_move;

	// ïœêî
	GameObject m_Block = null;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Block")
		{
			sc_state.Set_CanClimb_Forword(true);
			sc_state.Set_IsBlock(true);
			m_Block = other.gameObject;
		}
		if (other.gameObject.tag == "Stage")
		{
			sc_state.Set_CanClimb_Forword(true);
			sc_state.Set_IsStage(true);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Block")
		{
			sc_state.Set_CanClimb_Forword(true);
			sc_state.Set_IsBlock(true);
			m_Block = other.gameObject;
		}
		if (other.gameObject.tag == "Stage")
		{
			sc_state.Set_CanClimb_Forword(true);
			sc_state.Set_IsStage(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		sc_state.Set_CanClimb_Forword(false);

		sc_state.Set_IsBlock(false);
		sc_state.Set_IsStage(false);
	}

	public GameObject Get_Block()
	{
		return m_Block;
	}
}
