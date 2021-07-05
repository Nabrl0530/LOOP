using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private static Data instance;

    [SerializeField] List<int> _status = new List<int>();
    [SerializeField] bool save = false;
    const string _statusKey = "stageStatus";

    public static Data Instance
    {
        get{
            return instance;
        }
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


        //データをロード
        if(!PlayerPrefs.HasKey(_statusKey))
        {
            string s = null;

            for (int i = 0; i < _status.Count; i++)
            {
                _status[i] = (int)STAGE_STATUS.NONE;
                s += _status[i].ToString() + ",";
            }

            PlayerPrefs.SetString(_statusKey, s);
            PlayerPrefs.Save();
        }
        else
        {
            Load();
        }
    }

    private void Update()
    {
        //デバッグ用
        if(save)
        {
            Save();
            save = false;
        }
    }


    //任意のステージ番号を入れることで現在の状態が帰ってくる
    //intで帰ってくるけどSTAGE_STATUS型で用意済み
    public int GetStageStatus(int stageNum)
    {
        return _status[stageNum];
    }


    //ステージの状態を更新
    //任意のステージの番号と更新するステータスが必要
    public void SetStageStatus(int stageNum,STAGE_STATUS status)
    {
        _status[stageNum] = (int)status;

        Save();
    }

    void Load()
    {
        string s = null;

        string loadData = PlayerPrefs.GetString(_statusKey);
        string[] strArray = loadData.Split(',');

        _status.Clear();

        for(int i=0;i<strArray.Length;i++)
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
}
