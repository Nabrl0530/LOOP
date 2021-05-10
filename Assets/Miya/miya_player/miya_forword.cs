using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_forword : MonoBehaviour
{
	// éQè∆
	public miya_player_state sc_state;

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
		sc_state.Set_CanClimb_Forword(true);
	}

	void OnTriggerExit(Collider other)
	{
		sc_state.Set_CanClimb_Forword(false);
	}
}
