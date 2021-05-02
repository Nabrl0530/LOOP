using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {

            this.transform.RotateAround(new Vector3(0,0,0),this.transform.up,10.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.R))
        {

            this.transform.RotateAround(new Vector3(0, 0, 0), this.transform.up, -10.0f * Time.deltaTime);
        }

    }
}
