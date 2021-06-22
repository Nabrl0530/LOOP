using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_sound_control : MonoBehaviour
{
	// どっち
	public bool BGMtrueSEfalse = true;
	
	// ソース
	AudioSource Sound;

	// ボリューム
	float Volume;

	// Start is called before the first frame update
	void Start()
    {
		// どっち
		BGMtrueSEfalse = true;

		// ソース
		Sound = this.GetComponent<AudioSource>();

		// ボリューム
		Volume = Sound.volume;
		//Debug.Log(Volume);

		// どっち
		if ( BGMtrueSEfalse )	Sound.volume = Volume * miya_test_UI.Magnification_BGM;
		else					Sound.volume = Volume * miya_test_UI.Magnification_SE;
		//Debug.Log(miya_test_UI.Magnification_BGM);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
