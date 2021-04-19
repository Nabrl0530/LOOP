using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Start : MonoBehaviour
{
    public GameObject Laser;

    // Start is called before the first frame update
    void Start()
    {
        var parent = this.transform;
        Instantiate(Laser, this.transform.position, Quaternion.identity, parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
