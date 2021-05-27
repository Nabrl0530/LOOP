using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    GameObject camera_ob;
    public GameObject[] obj = new GameObject[7];
    float len;  //’·‚³
    Vector3 Ditection;

    // Start is called before the first frame update
    void Start()
    {
        camera_ob = GameObject.Find("miya_camera_default");
    }

    // Update is called once per frame
    void Update()
    {
        Ditection = camera_ob.transform.position - transform.position;

        len = Mathf.Sqrt(Mathf.Pow(Ditection.x, 2) + Mathf.Pow(Ditection.z, 2));

        if (len < 3)
        {
            for(int i=0; i<7;i++)
            {
                obj[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                obj[i].SetActive(true);
            }
        }
    }
}
