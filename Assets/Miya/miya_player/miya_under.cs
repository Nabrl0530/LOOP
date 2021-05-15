using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_under : MonoBehaviour
{
	// éQè∆
	public miya_player_move sc_move;

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
		if (other.gameObject.tag == "Player") return;
		sc_move.Set_IsUnder(true);
	}

	void OnTriggerExit(Collider other)
	{
		sc_move.Set_IsUnder(false);
	}
}
