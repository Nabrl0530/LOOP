using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIRROR : MonoBehaviour
{
    public GameObject Laser;
    private GameObject C_Laser = null;
    private bool Use = false;
    private int Hit_Count = 0;
    Vector3 BasePos;
    Vector3 MirrorVec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Use)
        {
            Hit_Count++;

            C_Laser.transform.position = BasePos;
            C_Laser.transform.rotation = Quaternion.LookRotation(MirrorVec);
        }

        if (Hit_Count == 5)
        {
            Destroy(C_Laser);
            C_Laser = null;
            Use = false;
        }
    }

    public void HitLaser()
    {
        Hit_Count = 0;  //ÉJÉEÉìÉ^èâä˙âª

        if (!Use)
        {
            MakeLaser();
        }
    }

    void MakeLaser()
    {
        Quaternion Rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        C_Laser = Instantiate(Laser, BasePos, Rot);

        //C_Laser.transform.position = transform.position;
        C_Laser.transform.rotation = Quaternion.LookRotation(MirrorVec);

        Use = true;
    }

    public void SetBasepos(Vector3 pos, Vector3 Ditector)
    {
        BasePos = pos;
        MirrorVec = Vector3.Reflect(Ditector, transform.forward);

        //transform.rotation = Quaternion.LookRotation(diff);
    }
}
