using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miya_test_UI : MonoBehaviour
{
	public GameObject UI_window;

	// テストサウンド
	public GameObject TestBGM;
	public GameObject TestSE;
	AudioSource TestBGM_audio;
	AudioSource TestSE_audio;
	float FirstVolume_BGM;
	float FirstVolume_SE;

	// シリンダー
	Slider slider_bgm;
	Slider slider_se;

	// 選択時背景
	Image Back_BGM;
	Image Back_SE;
    Image Back_JAPANESE;
    Image Back_ENGLISH;
    Image Back_Exit;
	Image Back_Reset;

	// ボリューム
	static public float Magnification_BGM = 0.5f;
	static public float Magnification_SE = 0.5f;

	// デバッグ
	bool active = false;

	// グリッド調整
	public float GridValue = 0.1f;


	// 操作箇所
	enum Witch_e
	{
		BGM,
		SE,
        JAPANESE,
        ENGLISH,
		Exit,
		Reset
	}
	int Witch_Control = (int)Witch_e.BGM;   //縦の位置
	public GameObject Witch_Slider;
	public GameObject Witch_Button;//koko


    [SerializeField]
    private GameObject m_toggle_japanese = null;
    [SerializeField]
    private GameObject m_toggle_english = null;


    // Start is called before the first frame update
    void Start()
	{
		// テストサウンド
		if (TestBGM) TestBGM_audio = TestBGM.GetComponent<AudioSource>();
		if (TestSE) TestSE_audio = TestSE.GetComponent<AudioSource>();
		FirstVolume_BGM = TestBGM_audio.volume;
		FirstVolume_SE = TestSE_audio.volume;

		// シリンダー
		GameObject back = this.transform.Find("Back").gameObject;
		slider_bgm = back.transform.Find("Slider_BGM").GetComponent<Slider>();
		slider_se = back.transform.Find("Slider_SE").GetComponent<Slider>();

		// 選択時背景
		Back_BGM	= back.transform.Find("Back_BGM"	).GetComponent<Image>();
		Back_SE		= back.transform.Find("Back_SE"		).GetComponent<Image>(); Back_SE.enabled = false;
        Back_JAPANESE = back.transform.Find("Back_JAPANESE").GetComponent<Image>(); Back_JAPANESE.enabled = false;
        Back_ENGLISH = back.transform.Find("Back_ENGLISH").GetComponent<Image>(); Back_ENGLISH.enabled = false;
        Back_Exit	= back.transform.Find("Back_Exit"	).GetComponent<Image>(); Back_Exit.enabled = false;
		Back_Reset	= back.transform.Find("Back_Reset"	).GetComponent<Image>(); Back_Reset.enabled = false;
		Witch_Control = (int)Witch_e.BGM;
		

		// ボリューム
		Magnification_BGM = 0.5f;
		Magnification_SE = 0.5f;

		// デバッグ
		active = false;
	}

	// Update is called once per frame
	void Update()
	{
		// ボリューム
		Magnification_BGM = slider_bgm.value;
		Magnification_SE = slider_se.value;

		// テストプレイ
		TestBGM_audio.volume = FirstVolume_BGM * Magnification_BGM;
		TestSE_audio.volume = FirstVolume_SE * Magnification_SE;

		if (active)
		{
			// SEテストプレイ
			if (Input.GetMouseButtonUp(0)) TestSE_audio.Play();

			// キー入力対応															コントローラー要追記
			{
				// 選択位置
				{
                    // 縦
                    if (Witch_Control < (int)Witch_e.JAPANESE)
                    {
                        if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            Witch_Control++;
                        }
                        if (Input.GetKeyUp(KeyCode.UpArrow))
                        {
                            if (Witch_Control > (int)Witch_e.BGM) Witch_Control--;
                        }
                    }
                    else if (Witch_Control == (int)Witch_e.JAPANESE)
                    {
                        if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            Witch_Control += 2;
                        }
                        if (Input.GetKeyUp(KeyCode.UpArrow))
                        {
                            Witch_Control--;
                        }
                    }
                    else if (Witch_Control == (int)Witch_e.ENGLISH)
                    {
                        if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            Witch_Control++;
                        }
                        if (Input.GetKeyUp(KeyCode.UpArrow))
                        {
                            Witch_Control -= 2;
                        }
                    }
                    else if (Witch_Control == (int)Witch_e.Exit)
                    {
                        if (Input.GetKeyUp(KeyCode.UpArrow))
                        {
                            Witch_Control -= 2;
                        }
                    }
                    // 横
                    if (Witch_Control == (int)Witch_e.JAPANESE || Witch_Control == (int)Witch_e.ENGLISH)
                    {
                        if (Input.GetKeyUp(KeyCode.RightArrow))
                        {
                            if (Witch_Control == (int)Witch_e.JAPANESE) Witch_Control++;
                        }
                        if (Input.GetKeyUp(KeyCode.LeftArrow))
                        {
                            if (Witch_Control == (int)Witch_e.ENGLISH) Witch_Control--;
                        }
                    }
                    if (Witch_Control >= (int)Witch_e.Exit)
					{
						if (Input.GetKeyUp(KeyCode.RightArrow))
						{
							if (Witch_Control == (int)Witch_e.Exit) Witch_Control++;
						}
						if (Input.GetKeyUp(KeyCode.LeftArrow))
						{
							if (Witch_Control == (int)Witch_e.Reset) Witch_Control--;
						}
					}
				}
				// スライド数値
				if (Witch_Control == (int)Witch_e.BGM)
				{
					if (Input.GetKeyUp(KeyCode.RightArrow))
					{
						slider_bgm.value += GridValue;
					}
					if (Input.GetKeyUp(KeyCode.LeftArrow))
					{
						slider_bgm.value -= GridValue;
					}
					if (slider_bgm.value > 1) slider_bgm.value = 1;
					if (slider_bgm.value < 0) slider_bgm.value = 0;
				}
				else if (Witch_Control == (int)Witch_e.SE)
				{
					if (Input.GetKeyUp(KeyCode.RightArrow))
					{
						slider_se.value += GridValue;
						TestSE_audio.Play();
					}
					if (Input.GetKeyUp(KeyCode.LeftArrow))
					{
						slider_se.value -= GridValue;
						TestSE_audio.Play();
					}
					if (slider_se.value > 1) slider_se.value = 1;
					if (slider_se.value < 0) slider_se.value = 0;
				}
				// 選択時背景
				switch(Witch_Control)
				{
					case (int)Witch_e.BGM:
						Back_BGM.enabled = true;
						Back_SE.enabled = false;
                        Back_JAPANESE.enabled = false;
                        Back_ENGLISH.enabled = false;
                        Back_Exit.enabled = false;
						Back_Reset.enabled = false;
						break;
					case (int)Witch_e.SE:
						Back_BGM.enabled = false;
						Back_SE.enabled = true;
                        Back_JAPANESE.enabled = false;
                        Back_ENGLISH.enabled = false;
                        Back_Exit.enabled = false;
						Back_Reset.enabled = false;
						break;
                    case (int)Witch_e.JAPANESE:
                        Back_BGM.enabled = false;
                        Back_SE.enabled = false;
                        Back_JAPANESE.enabled = true;
                        Back_ENGLISH.enabled = false;
                        Back_Exit.enabled = false;
                        Back_Reset.enabled = false;
                        break;
                    case (int)Witch_e.ENGLISH:
                        Back_BGM.enabled = false;
                        Back_SE.enabled = false;
                        Back_JAPANESE.enabled = false;
                        Back_ENGLISH.enabled = true;
                        Back_Exit.enabled = false;
                        Back_Reset.enabled = false;
                        break;
                    case (int)Witch_e.Exit:
						Back_BGM.enabled = false;
						Back_SE.enabled = false;
                        Back_JAPANESE.enabled = false;
                        Back_ENGLISH.enabled = false;
                        Back_Exit.enabled = true;
						Back_Reset.enabled = false;
						break;
					case (int)Witch_e.Reset:
						Back_BGM.enabled = false;
						Back_SE.enabled = false;
                        Back_JAPANESE.enabled = false;
                        Back_ENGLISH.enabled = false;
                        Back_Exit.enabled = false;
						Back_Reset.enabled = true;
						break;
				}

				// 選択、LANGUAGE,Exit,Reset
				if (Input.GetKeyUp(KeyCode.Return))
				{
                    if (Witch_Control == (int)Witch_e.JAPANESE)
                    {
                        m_toggle_japanese.GetComponent<Toggle>().isOn = true;
                        LanguageSetting.Set_Is_Japanese(true);
                    }
                    if (Witch_Control == (int)Witch_e.ENGLISH)
                    {
                        m_toggle_english.GetComponent<Toggle>().isOn = true;
                        LanguageSetting.Set_Is_Japanese(false);
                    }
					if (Witch_Control == (int)Witch_e.Exit)
					{
						Close_Window();
						TestSE_audio.Play();
					}
					if (Witch_Control == (int)Witch_e.Reset)
					{
						Reset_Value();
						TestSE_audio.Play();
					}
				}
			}
		}

		// デバッグ
		{
			if (Input.GetKeyUp(KeyCode.P))
			{
				active = !active;
			}

			if (active)
			{
				Show_Window();
				//Debug.Log("Show");
			}
			else
			{
				Close_Window();
				//Debug.Log("Close");
			}
		}
	}

	public void Show_Window()
	{
		UI_window.SetActive(true);
		active = true;
	}
	public void Close_Window()
	{
		UI_window.SetActive(false);
		active = false;

		Witch_Control = (int)Witch_e.BGM;
	}

	public void Reset_Value()
	{
		slider_bgm.value = 0.5f;
		slider_se.value = 0.5f;
	}

    //上キー入力による呼び出し
    public void UpKey()
    {
        if(Witch_Control == (int)Witch_e.Reset)
        {
            return;
        }

        if (Witch_Control <= (int)Witch_e.JAPANESE)
        {
            Witch_Control--;
        }
        else if (Witch_Control == (int)Witch_e.ENGLISH)
        {
            Witch_Control -= 2;
        }
        else if (Witch_Control == (int)Witch_e.Exit)
        {
            Witch_Control -= 2;
        }

        if (Witch_Control == -1)
        {
            Witch_Control = 0;
            //Witch_Control = (int)Witch_e.Exit;
        }
    }

    //下キー入力による呼び出し
    public void DownKey()
    {
        if (Witch_Control == (int)Witch_e.Reset)
        {
            return;
        }

        if (Witch_Control < (int)Witch_e.JAPANESE)
        {
            Witch_Control++;
        }
        else if (Witch_Control == (int)Witch_e.JAPANESE)
        {
            Witch_Control += 2;
        }
        else if (Witch_Control == (int)Witch_e.ENGLISH)
        {
            Witch_Control++;
        }

        if (Witch_Control == (int)Witch_e.Reset)
        {
            //Witch_Control = (int)Witch_e.BGM;
        }
    }

    //右キー入力による呼び出し
    public void RightKey()
    {
        //最下段にいる場合
        if (Witch_Control == (int)Witch_e.Exit || Witch_Control == (int)Witch_e.Reset)
        {
            if (Witch_Control == (int)Witch_e.Exit)
            {
                Witch_Control = (int)Witch_e.Reset;
            }
            else
            {
                //Witch_Control = (int)Witch_e.Exit;
            }
        }
        else if (Witch_Control == (int)Witch_e.JAPANESE || Witch_Control == (int)Witch_e.ENGLISH)
        {
            if (Witch_Control == (int)Witch_e.JAPANESE)
            {
                Witch_Control = (int)Witch_e.ENGLISH;
            }
            else
            {
                //Witch_Control = (int)Witch_e.JAPANESE;
            }
        }
        else
        {
            //音量切り替えの場合
            if (Witch_Control == (int)Witch_e.BGM)
            {
                slider_bgm.value += GridValue;
                if (slider_bgm.value > 1) slider_bgm.value = 1;
            }

            if (Witch_Control == (int)Witch_e.SE)
            {
                
                slider_se.value += GridValue;
                if (slider_se.value > 1) slider_se.value = 1;
                TestSE_audio.Play();               
            }
        }
    }

    //左キー入力による呼び出し
    public void LeftKey()
    {
        //最下段にいる場合
        if (Witch_Control == (int)Witch_e.Exit || Witch_Control == (int)Witch_e.Reset)
        {
            if (Witch_Control == (int)Witch_e.Exit)
            {
                //Witch_Control = (int)Witch_e.Reset;
            }
            else
            {
                Witch_Control = (int)Witch_e.Exit;
            }
        }
        else if (Witch_Control == (int)Witch_e.JAPANESE || Witch_Control == (int)Witch_e.ENGLISH)
        {
            if (Witch_Control == (int)Witch_e.JAPANESE)
            {
                //Witch_Control = (int)Witch_e.ENGLISH;
            }
            else
            {
                Witch_Control = (int)Witch_e.JAPANESE;
            }
        }
        else
        {
            //音量切り替えの場合
            if (Witch_Control == (int)Witch_e.BGM)
            {
                slider_bgm.value -= GridValue;
                if (slider_bgm.value < 0) slider_bgm.value = 0;
            }

            if (Witch_Control == (int)Witch_e.SE)
            {
                slider_se.value -= GridValue;
                if (slider_se.value < 0) slider_se.value = 0;
                TestSE_audio.Play();
            }
        }
    }

    public bool ActionKey()
    {
        if (Witch_Control == (int)Witch_e.Exit)
        {
            Close_Window();
            TestSE_audio.Play();
            return true;
        }
        if (Witch_Control == (int)Witch_e.Reset)
        {
            Reset_Value();
            TestSE_audio.Play();
        }

        return false;
    }
}
