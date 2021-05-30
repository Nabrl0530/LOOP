using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_ef : MonoBehaviour
{
    public GameObject ef1;
    public GameObject ef2;
    public GameObject ef3;
    public GameObject ef4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setlen(float l)
    {

        ef1.transform.localScale = new Vector3(2.0f, l, 2.0f);
        ef2.transform.localScale = new Vector3(1.5f, l, 1.5f);

        ef3.transform.localScale = new Vector3(2.0f, l, 2.0f);
        ef4.transform.localScale = new Vector3(1.5f, l, 1.5f);

        Vector3 pos = transform.position + transform.forward * l / 15;
        //pos.z = l / 15;
        ef3.transform.position = pos;

        pos = transform.position + transform.forward * l / 15;
        //pos.z = l / 15;
        ef4.transform.position = pos;
    }
}
