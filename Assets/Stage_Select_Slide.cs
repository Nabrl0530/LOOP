using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Select_Slide : MonoBehaviour
{
    public Stage_Select ss;
    RectTransform rt;
    Image image;

    bool IN;
    bool OUT;

    float VecM;
    int count = 0;

    Vector2 Base_Size;
    float size;
    float alpha;

    bool PICK;
    bool BACK;

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        image = this.GetComponent<Image>();

        Base_Size = rt.sizeDelta;
        size = 1.0f;
        alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(Base_Size.x * size, Base_Size.y * size); //サイズが変更できる
        image.color = new Vector4(1, 1, 1, alpha);
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

        if(PICK)
        {
            size += 0.1f;
            if(size >= 2)
            {
                alpha -= 0.1f;

                if(alpha < 0)
                {
                    alpha = 0;
                    PICK = false;
                }
            }
        }

        if(BACK)
        {
            size -= 0.1f;
            if(size <= 3)
            {
                alpha += 0.1f;
                if(alpha > 1)
                {
                    alpha = 1;
                }
            }

            if(size < 1)
            {
                size = 1;
                alpha = 1;
                BACK = false;
                ss.clear_END();
            }
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

    public void SetPick()
    {
        PICK = true;
        size = 1.0f;
        alpha = 1.0f;
    }

    public void SetBack()
    {
        BACK = true;
    }
}
