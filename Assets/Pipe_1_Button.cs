using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_1_Button : MonoBehaviour
{
    Player_Move Player_Move;
    private GameObject Pipe;   //wallèÓïÒäiî[óp
    public GameObject FloorOne;
    float rot;
    // Start is called before the first frame update
    void Start()
    {
        Pipe = GameObject.Find("PIPE_1_BASE");
        //rot = Pipe.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block"))
        {
            Pipe.transform.Rotate(0, 1, 0);
            Player_Move = other.GetComponent<Player_Move>();
            if(Player_Move.GetLayer() == 1)
            {

            }
        }
    }
}
