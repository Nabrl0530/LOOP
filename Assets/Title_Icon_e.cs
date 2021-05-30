using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_Icon_e : MonoBehaviour
{
    RectTransform rt;
    Image image;

    int Slide;
    bool Warp;
    int position;
    float size;
    float size_gap;
    float VecM;
    int count = 0;
    Vector2 Base_Size = new Vector2(300, 150);

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        image = this.GetComponent<Image>();

        Slide = 0;
        Warp = false;

        position = 3;
        size = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(Base_Size.x * size, Base_Size.y * size); //サイズが変更できる
    }

    void FixedUpdate()
    {
        if (count > 0 && !Warp)
        {
            Vector3 pos = rt.position;
            pos.x += VecM;
            rt.position = pos;

            size += size_gap;

            count--;
        }
        else if (count > 0 && Warp)
        {
            Vector3 pos = rt.position;
            pos.x += VecM;

            if (count > 25)
            {
                size -= 0.02f;
                pos.y += 3;
            }
            else
            {
                size += 0.02f;
                pos.y -= 3;
            }

            rt.position = pos;

            count--;
            if (count == 0)
            {
                Warp = false;
            }
        }

    }

    public void SetMoveR()
    {
        position++;
        if (position == 4)
        {
            VecM = -800 / 50;
            count = 50;
            Warp = true;
            position = 1;
        }
        else if (position == 2)
        {
            VecM = 400 / 50;
            count = 50;
            size_gap = 0.8f / 50;
        }
        else if (position == 3)
        {
            VecM = 400 / 50;
            count = 50;
            size_gap = -0.8f / 50;
        }
    }

    public void SetMoveL()
    {
        position--;
        if (position == 0)
        {
            VecM = 800 / 50;
            count = 50;
            Warp = true;
            position = 3;
        }
        else if (position == 2)
        {
            VecM = -400 / 50;
            count = 50;
            size_gap = 0.8f / 50;
        }
        else if (position == 1)
        {
            VecM = -400 / 50;
            count = 50;
            size_gap = -0.8f / 50;
        }
    }
}
