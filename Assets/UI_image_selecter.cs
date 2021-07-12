using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_image_selecter : MonoBehaviour
{
    public Sprite[] img = new Sprite[2];
    Image UI_Action_mes;
    // Start is called before the first frame update
    void Start()
    {
        UI_Action_mes = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Set_UI_Image(bool JAPANEESE)
    {
        if(JAPANEESE)
        {
            UI_Action_mes.sprite = img[0];
        }
        else
        {
            UI_Action_mes.sprite = img[1];
        }
        
    }
}
