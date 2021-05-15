using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Check : MonoBehaviour
{
    // éQè∆
    public Player_State sc_state;

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
        sc_state.Set_CanClimb_Check(true);
    }

    void OnTriggerExit(Collider other)
    {
        sc_state.Set_CanClimb_Check(false);
    }
}
