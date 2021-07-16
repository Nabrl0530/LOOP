using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private static Data instance;

    [SerializeField] List<int> _status = new List<int>();
    [SerializeField] bool save = false;
    const string _statusKey = "stageStatus";
    const string scrollkey = "scrollkey";
    int currentStageNum;
    [SerializeField] int stageNum;

    const int EStart = 0;
    const int NStart = 5;
    const int HStart = 12;

    public static Data Instance
    {
        get
        {
            return instance;
        }
    }

    public int CurrentStageNum
    {
        get { return currentStageNum; }
    }

    public int DataNum
    {
        get { return _status.Count; }
    }

    public enum STAGE_STATUS
    {
        NONE,       //未開放
        OPEN,       //開放済み(諦めた場合も)
        CLEAR,      //クリアした
        NEW,        //開放
    }

    private void Start()
    {
        //簡易シングルトン
        if (instance)
            Destroy(this.gameObject);
        else
            instance = this;

        //全てのシーンに存在
        DontDestroyOnLoad(this.gameObject);

        //PlayerPrefs.DeleteKey(_statusKey);


        //データをロード
        if (!PlayerPrefs.HasKey(_statusKey))
        {
            string s = null;

            for (int i = 0; i < stageNum; i++)
            {
                if (i == EStart || i == NStart || i == HStart)
                {
                    _status.Add((int)STAGE_STATUS.OPEN);
                    s += _status[i].ToString() + ",";
                }
                else
                {
                    _status.Add((int)STAGE_STATUS.NONE);
                    s += _status[i].ToString() + ",";
                }
            }

            PlayerPrefs.SetString(_statusKey, s);
            PlayerPrefs.Save();
        }
        else
        {
            Load();
        }

        if (PlayerPrefs.HasKey(scrollkey))
            currentStageNum = PlayerPrefs.GetInt(scrollkey);
        else
            currentStageNum = 0;

    }

    private void Update()
    {
        //デバッグ用
        if (save)
        {
            Save();
            save = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
            Delete();
    }


    //任意のステージ番号を入れることで現在の状態が帰ってくる
    //intで帰ってくるけどSTAGE_STATUS型で用意済み
    public int GetStageStatus(int stageNum)
    {
        return _status[stageNum];
    }


    //ステージの状態を更新
    //任意のステージの番号と更新するステータスが必要
    public void SetStageStatus(int stageNum, STAGE_STATUS status)
    {
        _status[stageNum] = (int)status;

        Save();
    }


    /// <summary>
    /// 現在のステージをクリアしたときに呼ぶ
    /// </summary>
    public void StageClear()
    {
        currentStageNum = PlayerPrefs.GetInt(scrollkey);

        _status[currentStageNum] = (int)STAGE_STATUS.CLEAR;

        Save();
    }

    void Load()
    {
        string s = null;

        string loadData = PlayerPrefs.GetString(_statusKey);
        string[] strArray = loadData.Split(',');

        _status.Clear();

        for (int i = 0; i < strArray.Length - 1; i++)
        {
            _status.Add(int.Parse(strArray[i]));
            s += _status[i].ToString() + ",";
        }

        PlayerPrefs.SetString(_statusKey, s);
        PlayerPrefs.Save();
    }


    void Save()
    {
        string s = null;

        for (int i = 0; i < _status.Count; i++)
        {
            s += _status[i].ToString() + ",";
        }

        PlayerPrefs.SetString(_statusKey, s);
        PlayerPrefs.Save();
    }


    void Delete()
    {
        PlayerPrefs.DeleteKey(_statusKey);
        PlayerPrefs.DeleteKey(scrollkey);

        _status.Clear();

        //データをロード
        string s = null;

        for (int i = 0; i < stageNum; i++)
        {
            if (i == EStart || i == NStart || i == HStart)
            {
                _status.Add((int)STAGE_STATUS.OPEN);
                s += _status[i].ToString() + ",";
            }
            else
            {
                _status.Add((int)STAGE_STATUS.NONE);
                s += _status[i].ToString() + ",";
            }
        }

        PlayerPrefs.SetString(_statusKey, s);
        PlayerPrefs.Save();

        CFadeManager.FadeOut(1);
    }
}