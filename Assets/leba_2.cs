using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leba_2 : MonoBehaviour
{
    public GameObject FloorTwo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpinL()
    {
        FloorTwo.GetComponent<SPIN_FloorOne>().SetSpin(-1);
    }

    public void SpinR()
    {
        FloorTwo.GetComponent<SPIN_FloorOne>().SetSpin(1);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("レバー");
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player_Move>().SetHIT_LEVER2();
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("レバー抜け");
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player_Move>().ClearHIT_LEVER2();
        }
    }
}
