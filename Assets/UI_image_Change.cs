using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_image_Change : MonoBehaviour
{
    int count = 0;
    public UI_image_selecter[] select = new UI_image_selecter[8];
    // Start is called before the first frame update
    void Start()
    {
        /*
        for(int i=0;i<8;i++)
        {
            select[i].Set_UI_Image(LanguageSetting.Get_Is_Japanese());
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(count>10)
        {
            return;
        }

        count++;

        if(count == 5)
        {
            for (int i = 0; i < 8; i++)
            {
                select[i].Set_UI_Image(LanguageSetting.Get_Is_Japanese());
            }
        }
    }
}
