using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Under : MonoBehaviour
{
    public bool HIT = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Get_HIT()
    {
        return HIT;
    }

    void OnTriggerEnter(Collider other)
    {
        HIT = true;
    }

    void OnTriggerStay(Collider other)
    {
        HIT = true;
    }

    void OnTriggerExit(Collider other)
    {
        HIT = false;
    }
}
