using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Ladder : MonoBehaviour
{
    //[SerializeField]




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Snap(float initial,float finish,float add,ref float target)
    {
        for(float i = initial; i < finish; i += add)
        {
            if(i < target && target <= i + add)
            {
                target = i + add;
            }
        }
    }
}
