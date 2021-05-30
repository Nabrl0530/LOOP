using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Field : MonoBehaviour
{

    int Level;
    int Last_Level;
    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Level != Last_Level)
        {
            if(Level ==1)
            {
                transform.position = new Vector3(0, 0, 0);
            }

            if (Level == 2)
            {
                transform.position = new Vector3(0, 3.51f, 0);
            }

            if (Level == 3)
            {
                transform.position = new Vector3(0, 7.0f, 0);
            }

            if (Level == 4)
            {
                transform.position = new Vector3(0, 10.5f, 0);
            }
        }

        Last_Level = Level;
    }

    public void Setlevel(int l)
    {
        Level = l;
    }
}
