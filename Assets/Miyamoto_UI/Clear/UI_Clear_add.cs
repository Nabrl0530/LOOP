using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Clear_add : MonoBehaviour
{
    public Sprite[] img = new Sprite[2];
    RectTransform rt;
    Image image;
    float x, y, a;
    bool ON;
    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        image = this.GetComponent<Image>();
        x = y = 1000;
        a = 1.0f;
        ON = false;
        image.color = new Vector4(1, 1, 1, 0);

        if(LanguageSetting.Get_Is_Japanese())
        {
            image.sprite = img[0];
        }
        else
        {
            image.sprite = img[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (ON)
        {
            if (x < 1400)
            {
                x += 10;
                y += 10;
            }
            else
            {
                x += 5;
                y += 5;
            }

            if (a > 0)
            {
                a -= 0.05f;
            }

            image.color = new Vector4(1, 1, 1, a);

            rt.sizeDelta = new Vector2(x, y); //サイズが変更できる
        }
    }

    public void Set_ON()
    {
        ON = true;
        image.color = new Vector4(1, 1, 1, 1);
        a = 1.0f;
        x = y = 1000;
        rt.sizeDelta = new Vector2(x, y); //サイズが変更できる
    }
}
