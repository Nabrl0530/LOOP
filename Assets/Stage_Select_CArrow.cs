using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Select_CArrow : MonoBehaviour
{
    RectTransform rt;
    Vector2 Base_Size;
    float size;

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        Base_Size = rt.sizeDelta;
        size = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(Base_Size.x * size, Base_Size.y * size); //サイズが変更できる
    }

    void FixedUpdate()
    {
        if(size > 1)
        {
            size -= 0.009f;

            if(size < 1)
            {
                size = 1;
            }
        }
    }
    public void SetBig()
    {
        size = 1.3f;
    }
}

