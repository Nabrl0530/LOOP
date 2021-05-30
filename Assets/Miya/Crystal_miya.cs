using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_miya : MonoBehaviour
{
    Vector3 rotation;
    public float speed_roation_default = 10;
    public float speed_roation_clear = 100;
    bool isClear = false;

    public void Set_Clear()
	{

    }
    public void Set_Default()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rotation = new Vector3(0, 0, 0);
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( isClear )
        {
            rotation.y += speed_roation_clear * Time.deltaTime;
        }
        else
		{
            rotation.y += speed_roation_default * Time.deltaTime;
        }
        this.transform.rotation = Quaternion.Euler(rotation);
    }
}
