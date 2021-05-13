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
