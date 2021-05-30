using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Select_Slide : MonoBehaviour
{
    RectTransform rt;
    Image image;

    bool IN;
    bool OUT;

    float VecM;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (IN && count > 0)
        {
            if (count < 11)
            {
                Vector3 pos = rt.position;
                pos.y += VecM;
                rt.position = pos;               
            }

            count--;
        }
        else if(!IN && count > 0)
        {
            if (count > 40)
            {
                Vector3 pos = rt.position;
                pos.y += VecM;
                rt.position = pos;
            }
            count--;
        }
    }

    public void SetUp_IN(bool _is)
    {
        if (_is)
        {
            IN = true;
            VecM = 100;
            count = 50;
        }
        else
        {
            IN = false;
            VecM = 100;
            count = 50;
        }
    }

    public void SetDown_IN(bool _is)
    {
        if (_is)
        {
            IN = true;
            VecM = -100;
            count = 50;
        }
        else
        {
            IN = false;
            VecM = -100;
            count = 50;
        }
    }
}
