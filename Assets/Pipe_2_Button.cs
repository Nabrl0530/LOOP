using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_2_Button : MonoBehaviour
{
    Player_Move Player_Move;
    private GameObject wall;   //wallèÓïÒäiî[óp
    float rot;
    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.Find("PIPE_2_BASE");
        //rot = wall.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //rot += 0.01f;

            wall.transform.Rotate(0, 1, 0);

            Player_Move = other.GetComponent<Player_Move>();
            if (Player_Move.GetLayer() == 2)
            {
                Player_Move.AddRot(-1);
            }
        }
    }
}
