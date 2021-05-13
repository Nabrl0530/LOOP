using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOWER_HOLE : MonoBehaviour
{
    public GameObject Laser;
    public GameObject pair_hole;
    public GameObject Base1;
    public GameObject Base2;
    public GameObject Base3;
    private GameObject C_Laser = null;
    private bool Use = false;
    private bool Move = false;
    private int Hit_Count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (C_Laser != null)
        {
            C_Laser.transform.position = transform.position;
            Quaternion Rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            C_Laser.transform.rotation = Rot;
        }

        if (Use)
        {
            Hit_Count++;
        }

        if (Hit_Count == 5)
        {
            pair_hole.GetComponent<TOWER_HOLE>().Finish_Laser();
            Use = false;
        }

        //e‚ÌŽ²‚ðØ‚è‘Ö‚¦‚é
        if(transform.position.y > 4.9f)
        {
            transform.SetParent(Base3.transform);
        }
        else if(transform.position.y > 2.9f)
        {
            transform.SetParent(Base2.transform);
        }
        else
        {
            transform.SetParent(Base1.transform);
        }


    }

    public void HitLaser()
    {
        Make_Pair();
        Hit_Count = 0;
        Debug.Log("HIT_HOLE");
        Use = true;
    }

    void Make_Pair()
    {
        if (!Use)
        {
            pair_hole.GetComponent<TOWER_HOLE>().MakeLaser();
            Use = true;
        }
    }

    void MakeLaser()
    {

        Quaternion Rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        C_Laser = Instantiate(Laser, this.transform.position, Rot);
    }

    void Finish_Laser()
    {
        Destroy(C_Laser);
        C_Laser = null;
    }

    public void StartMove()
    {
        //Move = true;
        if(C_Laser != null)
        {
            Destroy(C_Laser);
            C_Laser = null;
        }
    }

    public void EndMove()
    {
        if (pair_hole.GetComponent<TOWER_HOLE>().GetUse())
        {
            Quaternion Rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            C_Laser = Instantiate(Laser, this.transform.position, Rot);
        }
    }

    bool GetUse()
    {
        return Use;
    }
}
