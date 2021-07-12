using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_byLanguage_miya : MonoBehaviour
{
	public Sprite Sprite_Japanese;
	public Sprite Sprite_English;

	public bool Is_Japanese = true;

	// Start is called before the first frame update
	void Start()
    {
		if (Is_Japanese)	this.GetComponent<Image>().sprite = Sprite_Japanese;
		else				this.GetComponent<Image>().sprite = Sprite_English;
	}

    // Update is called once per frame
    void Update()
    {
		if (Is_Japanese != LanguageSetting.Get_Is_Japanese())
		{
			if (LanguageSetting.Get_Is_Japanese())	this.GetComponent<Image>().sprite = Sprite_Japanese;
			else									this.GetComponent<Image>().sprite = Sprite_English;

			Is_Japanese = LanguageSetting.Get_Is_Japanese();
		}
    }
}
