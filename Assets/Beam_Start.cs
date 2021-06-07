using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Start : MonoBehaviour
{
    public GameObject[] Laser = new GameObject[3];
    GameObject laser;
    public int Use_ID;

    // Start is called before the first frame update
    void Start()
    {
        var parent = this.transform;
        Vector3 pos = this.transform.position;
        laser = Instantiate(Laser[Use_ID], pos, Quaternion.identity, parent);
        laser.gameObject.GetComponent<beam_pre>().Setrot(transform.rotation);
        laser.gameObject.GetComponent<beam_pre>().Set_Color_ID(Use_ID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
