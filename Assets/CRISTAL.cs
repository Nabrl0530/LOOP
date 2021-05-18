using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRISTAL : MonoBehaviour
{
    bool OK;
    bool HIT;
    public int count;
    public int no;
    // Start is called before the first frame update
    void Start()
    {
        HIT = false;
        OK = false;
        count = 0;
        no = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (HIT)
        {
            count++;

            if (count == 180)
            {
                Debug.Log("clear");
                GoToClear.Go();
            }

            no++;
            if (no == 5)
            {
                no = 0;
                count = 0;
                HIT = false;
            }
        }        
    }


    public void HitCristal()
    {
        if(!HIT)
        {
            HIT = true;
        }

        no = 0;
    }
}
