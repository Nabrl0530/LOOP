using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_mes : MonoBehaviour
{
    [SerializeField] Sprite[] mesTex = new Sprite[2];
    // Start is called before the first frame update
    void Start()
    {
        if(LanguageSetting.Get_Is_Japanese())
        {
            this.gameObject.GetComponent<Image>().sprite = mesTex[0];
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = mesTex[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
