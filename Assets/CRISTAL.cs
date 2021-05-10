using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRISTAL : MonoBehaviour
{
    bool HIT;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        HIT = false;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(HIT)
        {
            count++;

            if(count == 120)
            {
                Debug.Log("clear");
            }
        }
    }

    public void HitCristal()
    {
        if(!HIT)
        {
            HIT = true;
        }
    }
}
