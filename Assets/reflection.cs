using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflection : MonoBehaviour
{
    [SerializeField] int frame;
    int count =0;
    ReflectionProbe reflect;
    int mode;
    // Start is called before the first frame update
    void Start()
    {
        reflect = this.gameObject.GetComponent<ReflectionProbe>();
        //count++;

        mode = LanguageSetting.Get_MODE();

        if (mode == 2)
        {
            frame = 1;
        }
        else if(mode == 1)
        {
            frame = 3;
        }
        else
        {
            frame = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        count++;

        if(count == frame)
        {
            count = 0;
            //Debug.Log(reflect.refreshMode);

            reflect.RenderProbe();
        }
    }

    public void Changerate()
    {
        if(mode == 2)
        {
            mode--;
            frame = 1;
            count = 0;
            LanguageSetting.Set_MODE(mode);
        }
        else if(mode == 1)
        {
            mode--;
            frame = 3;
            count = 0;
            LanguageSetting.Set_MODE(mode);
        }
        else
        {
            mode = 2;
            frame = 6;
            count = 0;
            LanguageSetting.Set_MODE(mode);
        }
    }
}
