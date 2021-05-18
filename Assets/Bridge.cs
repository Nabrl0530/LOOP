using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public GameObject Laser;
    private GameObject C_Laser = null;
    public GameObject pair_door;
    private bool Use = false;
    private int Hit_Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeBridge()
    {
        Quaternion Rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        C_Laser = Instantiate(Laser, this.transform.position, Rot);
    }

    void Finish_Bridge()
    {
        Destroy(C_Laser);
        C_Laser = null;
    }
}
