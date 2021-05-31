using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class omake : MonoBehaviour
{
    bool OPEN;
    RectTransform rt;
    Vector2 Base_Size;
    float size = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        OPEN = false;
        Base_Size = rt.sizeDelta;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(OPEN && size != 1)
        {
            size += 0.03f;
            if(size>1)
            {
                size = 1;
            }
        }
        else if(!OPEN && size != 0)
        {
            size -= 0.03f;
            if (size < 0)
            {
                size = 0;
            }
        }

        //rt.sizeDelta = new Vector2(Base_Size.x * size, Base_Size.y * 1); //サイズが変更できる
        rt.localScale = new Vector2(size, 1);
    }

    public void SetOpen(bool _is)
    {
        OPEN = _is;
    }
}
