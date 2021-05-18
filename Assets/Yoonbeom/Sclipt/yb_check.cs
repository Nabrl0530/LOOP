using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yb_check : MonoBehaviour
{
	// éQè∆
	public yb_player_state sc_state;

	// Start is called before the first frame update
	void Start()
    {
        ////
        ///
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		sc_state.Set_CanClimb_Check(true);
	}

	void OnTriggerExit(Collider other)
	{
		sc_state.Set_CanClimb_Check(false);
	}
}
