using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_Spin : MonoBehaviour
{
    public GameObject pair_Bridge;

    Vector3 View;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        View = pair_Bridge.transform.position;
        View.y = transform.position.y;
        this.transform.LookAt(View);
    }
}
