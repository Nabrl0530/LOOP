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
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetHIT_LEVER2(transform.position);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetHIT_LEVER2(transform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("ÉåÉoÅ[î≤ÇØ");
        if (other.gameObject.CompareTag("Player"))
        {
            //other.GetComponent<Player_Move>().ClearHIT_LEVER2();
            other.GetComponent<Player>().ClearHIT_LEVER2();
        }
    }
}
