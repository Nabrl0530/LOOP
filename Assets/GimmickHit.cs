using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickHit : MonoBehaviour
{
    public Player_Move Player_Move;

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
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("TOWER"))
        {
            Player_Move.SetHIT_TOWER();           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TOWER"))
        {
            Player_Move.ClearHIT_TOWER();
        }
    }
}
