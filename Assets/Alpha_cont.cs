using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha_cont : MonoBehaviour
{
    Color color;

    public float a = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        color = this.gameObject.GetComponent<MeshRenderer>().material.color;

        color.a = a;
        //Debug.Log(color);

        this.gameObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
