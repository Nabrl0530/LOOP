using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALL2_button : MonoBehaviour
{
    private GameObject wall;   //wall情報格納用
    float rot;
    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.Find("WALL_2_BASE");
        rot = wall.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rot += 0.01f;

            wall.transform.Rotate(0, 1, 0);
        }
    }
}
