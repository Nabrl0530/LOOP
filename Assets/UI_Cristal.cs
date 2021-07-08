using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cristal : MonoBehaviour
{
    public Sprite[] img = new Sprite[3];
    Image UI_clear_num;
    // Start is called before the first frame update
    void Start()
    {
        UI_clear_num = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_UI_Num(int i)
    {
        UI_clear_num.sprite = img[i - 1];
    }
}
