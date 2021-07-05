using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflection : MonoBehaviour
{
    [SerializeField] int frame;
    int count;
    ReflectionProbe reflect;
    // Start is called before the first frame update
    void Start()
    {
        reflect = this.gameObject.GetComponent<ReflectionProbe>();
        count++;
    }

    // Update is called once per frame
    void Update()
    {
        count++;

        if(count == frame)
        {
            count = 0;
            Debug.Log(reflect.refreshMode);

            reflect.RenderProbe();
        }
    }
}
