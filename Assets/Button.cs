using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private GameObject FloorOne;
    private GameObject FloorTwo;
    private GameObject FloorThree;

    private float len;  //’·‚³
    private bool ON;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        FloorOne = GameObject.Find("FloorOne");
        FloorTwo = GameObject.Find("FloorTwo");
        FloorThree = GameObject.Find("FloorThree");
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(ON)
        {
            count++;

            if(count == 50)
            {
                count = -200;

                len = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
                //Debug.Log(len);
                if (len >= 12.0f)
                {
                    transform.SetParent(FloorThree.transform);
                    FloorThree.GetComponent<SPIN_FloorOne>().SetSpin(-1);
                }
                else if (len >= 8.5f)
                {
                    transform.SetParent(FloorTwo.transform);
                    FloorTwo.GetComponent<SPIN_FloorOne>().SetSpin(-1);
                }
                else
                {
                    transform.SetParent(FloorOne.transform);
                    FloorOne.GetComponent<SPIN_FloorOne>().SetSpin(-1);
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block"))
        {
            ON = true;
            count = 0;

            if(other.gameObject.CompareTag("Block"))
            {
                other.gameObject.transform.SetParent(this.gameObject.transform);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block"))
        {
            ON = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block"))
        {
            ON = false;
        }
    }
}
