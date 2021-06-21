using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private GameObject FloorOne;
    private GameObject FloorTwo;
    private GameObject FloorThree;

    private float len;  //’·‚³
    public bool ON;
    bool STOP;
    int no_count;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        FloorOne = GameObject.Find("FloorOne");
        FloorTwo = GameObject.Find("FloorTwo");
        FloorThree = GameObject.Find("FloorThree");
        count = 0;
        STOP = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //‹@”\’âŽ~’†‚Íˆ—’†’f
        if(STOP)
        {
            return;
        }

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

            no_count++;
            if(no_count == 2)
            {
                no_count = 0;
                ON = false;
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
                //other.gameObject.transform.SetParent(this.gameObject.transform);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block"))
        {
            //ON = true;
            no_count = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block"))
        {
            //ON = false;
        }
    }

    public void Set_stop()
    {
        STOP = true;
    }

    public void Clare_stop()
    {
        STOP = false;
    }
}
