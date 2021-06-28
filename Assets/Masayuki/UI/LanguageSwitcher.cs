using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class LanguageSwitcher : MonoBehaviour
{
    public Image m_JapaneseImage;
    public Image m_EnglishImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void SetON()
    {
        if(LanguageSetting.Get_Is_Japanese())
        {
            SwitchJapaneseImage();
        }
        else
        {
            SwitchEnglishImage();
        }
    }

    public void SetOFF()
    {
        m_JapaneseImage.color = new Color(1, 1, 1, 0);
        m_EnglishImage.color = new Color(1, 1, 1, 0);
        
    }

    private void SwitchJapaneseImage()
    {
        m_JapaneseImage.color = new Color(1, 1, 1, 1);
        m_EnglishImage.color = new Color(1,1,1,0);
    }

    private void SwitchEnglishImage()
    {
        m_EnglishImage.color = new Color(1, 1, 1, 1);
        m_JapaneseImage.color = new Color(1, 1, 1, 0);
    }
}
