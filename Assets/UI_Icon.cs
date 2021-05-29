using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Icon : MonoBehaviour
{
    GameObject Came;

    // Start is called before the first frame update
    void Start()
    {
        Came = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Came.transform);
    }
}
