using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_HIT : MonoBehaviour
{
    public Door door;
    public GameObject pair_Gate;
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
            if (other.GetComponent<Player>().SetHIT_Door(transform.position))
            {
                other.GetComponent<Player>().SetGate(transform.position);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().SetHIT_Door(transform.position))
            {
                other.GetComponent<Player>().SetGate(transform.position);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().ClearHIT_DOOR();
        }
    }

    public Door GetDoor()
    {
        return door;
    }

}
