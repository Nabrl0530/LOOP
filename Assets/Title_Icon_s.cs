using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_Icon_s : MonoBehaviour
{
    RectTransform rt;
    Image image;

    int Slide;
    int position;
    float size;
    float size_gap;
    float VecM; //横移動量
    float VecY; //縦移動量
    int count = 0;
    Vector2 Base_Size = new Vector2(300, 150);

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        image = this.GetComponent<Image>();

        Slide = 0;

        position = 2;
        size = 1.8f;
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(Base_Size.x * size, Base_Size.y * size); //サイズが変更できる
    }

    void FixedUpdate()
    {
        if(count > 0)
        {
            Vector3 pos = rt.position;
            pos.x += VecM;
            pos.y += VecY;
            rt.position = pos;

            size += size_gap;

            count--;
        }
    }

    public void SetMoveR()
    {
        position++;
        if(position == 2)
        {
            VecM = 450 / 50;
            VecY = 0;
            count = 50;
            size_gap = 0.8f / 50;
        }
        else if (position == 3)
        {
            VecM = 450 / 50;
            VecY = 0;
            count = 50;
            size_gap = -0.8f / 50;
        }
        else if (position == 4)
        {
            VecM = -450 / 50;
            VecY = 1.5f;
            count = 50;
            size_gap = -1.0f / 50;
        }
        else if (position == 5)
        {
            VecM = -450 / 50;
            VecY = -1.5f;
            count = 50;
            size_gap = 1.0f / 50;
            position = 1;
        }
        
    }

    public void SetMoveL()
    {
        position--;
        if (position == 0)
        {
            VecM = 450 / 50;
            VecY = 1.5f;
            count = 50;
            size_gap = -1.0f / 50;
            position = 4;
        }
        else if (position == 1)
        {
            VecM = -450 / 50;
            VecY = 0;
            count = 50;
            size_gap = -0.8f / 50;
        }
        else if (position == 2)
        {
            VecM = -450 / 50;
            VecY = 0;
            count = 50;
            size_gap = 0.8f / 50;
        }
        else if (position == 3)
        {
            VecM = 450 / 50;
            VecY = -1.5f;
            count = 50;
            size_gap = 1.0f / 50;
        }
    }
}
