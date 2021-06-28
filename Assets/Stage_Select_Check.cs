using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Select_Check : MonoBehaviour
{
    //public Image Image;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LanguageSwitcher>().SetOFF();
        //Image.color = new Color(1, 1, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetON()
    {
        GetComponent<LanguageSwitcher>().SetON();
        //Image.color = new Color(1, 1, 1, 1);
    }

    public void SetOFF()
    {
        GetComponent<LanguageSwitcher>().SetOFF();
        //Image.color = new Color(1, 1, 1, 0);
    }
}
