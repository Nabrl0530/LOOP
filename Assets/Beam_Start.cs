using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Start : MonoBehaviour
{
    public GameObject Laser;
    GameObject laser;

    // Start is called before the first frame update
    void Start()
    {
        var parent = this.transform;
        Vector3 pos = this.transform.position;
        laser = Instantiate(Laser, pos, Quaternion.identity, parent);
        laser.gameObject.GetComponent<beam_pre>().Setrot(transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
