using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject[] Laser_array = new GameObject[3];
    public GameObject pair_door;
    private GameObject C_Laser = null;
    private bool Use = false;
    private int Hit_Count = 0;
    public bool WARP_OK;
    // Start is called before the first frame update
    void Start()
    {
        WARP_OK = true;
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

    public void HitLaser(int id)
    {
        Make_Pair(id);
        Hit_Count = 0;
        //Debug.Log("HIT");
        Use = true;
    }

    void Make_Pair(int id)
    {
        if(!Use)
        {
            pair_door.GetComponent<Door>().MakeLaser(id);
            Use = true;
        }      
    }

    void MakeLaser(int id)
    {
        Quaternion Rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        C_Laser = Instantiate(Laser_array[id], this.transform.position, Rot);
        C_Laser.gameObject.GetComponent<beam_pre>().Set_Color_ID(id);
    }

    void Finish_Laser()
    {
        Destroy(C_Laser);
        C_Laser = null;
    }

    public Vector3 Getpair_pos()
    {
        return pair_door.transform.position;
    }

    public Quaternion Getpair_rot()
    {
        Quaternion Rot = Quaternion.Euler(pair_door.transform.eulerAngles.x, pair_door.transform.eulerAngles.y + 225, pair_door.transform.eulerAngles.z);
        return Rot;
    }

    public void SET_WARP_OK(bool _is)
    {
        WARP_OK = _is;
    }

    public bool GET_WARP_OK()
    {
        return WARP_OK;
    }

    public bool GET_WARP_OK_pair()
    {
        return pair_door.GetComponent<Door>().GET_WARP_OK();
    }
}
