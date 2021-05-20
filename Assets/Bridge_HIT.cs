using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_HIT : MonoBehaviour
{
    public Bridge bridge;
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
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetHIT_Bridge(transform.position);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetHIT_Bridge(transform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("ÉåÉoÅ[î≤ÇØ");
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().ClearHIT_BRIDGE();
        }
    }

    public Bridge GetBridge()
    {
        return bridge;
    }
}
