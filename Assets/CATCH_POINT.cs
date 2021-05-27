using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CATCH_POINT : MonoBehaviour
{
    public GameObject Block;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetPoint()
    {
        return transform.position;
    }

    public Vector3 GetBlockPoint()
    {
        return Block.transform.position;
    }
}
