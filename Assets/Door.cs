using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Laser;
    public GameObject pair_door;
    private GameObject C_Laser = null;
    private bool Use = false;
    private int Hit_Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(C_Laser != null)
        {
            C_Laser.transform.position = transform.position;
            Quaternion Rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            C_Laser.transform.rotation = Rot;         
        }

        if(Use)
        {
            Hit_Count++;
        }

        if(Hit_Count == 5)
        {
            pair_door.GetComponent<Door>().Finish_Laser();
            Use = false;
        }
    }

    public void HitLaser()
    {
        Make_Pair();
        Hit_Count = 0;
        //Debug.Log("HIT");
        Use = true;
    }

    void Make_Pair()
    {
        if(!Use)
        {
            pair_door.GetComponent<Door>().MakeLaser();
            Use = true;
        }      
    }

    void MakeLaser()
    {
        Quaternion Rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        C_Laser = Instantiate(Laser, this.transform.position, Rot);
    }

    void Finish_Laser()
    {
        Destroy(C_Laser);
        C_Laser = null;
    }
}
