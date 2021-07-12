using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    enum STATE
    {
        STAY,
        UP,
        DOWN,
    }

    enum STAGE_LEVEL
    {
        FIRST,
        EASY,
        NORMAL,
        HARD,
        EXTRA,
        END,
    }

    enum MOVE_MODE
    {
        NORMAL,
        SKIP,
    }

    enum SOUND
    {
        MOVE,
        OK,
        OPEN,
        CANCEL,
    }

    bool con_L; //コントローラー入力左
    bool con_R; //コントローラー入力右
    bool con_U; //コントローラー入力上
    bool con_D; //コントローラー入力下


    [SerializeField] int num;
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool a;    //調整↑
    [SerializeField] bool d;    //調整↓
    [SerializeField] float correction = 0.0f;   //調整値
    [SerializeField] int currentStagenum = 0;
    [SerializeField] int hard;
    [SerializeField] int normal;
    [SerializeField] int easy;

    [SerializeField] GameObject pop;

    [SerializeField] Image level;
    [SerializeField] Image lvNum;

    [SerializeField] Animator batU;
    [SerializeField] Animator batD;

    [SerializeField] Sprite[] numTex = new Sprite[10];

    [SerializeField] List<Sprite> lvTex = new List<Sprite>();
    [SerializeField] List<AudioSource> sound = new List<AudioSource>();
    [SerializeField] List<int> moveStageNum = new List<int>();
    [SerializeField] List<int> stageList = new List<int>();

    Color color = new Color(1, 1, 1, 1);
    Scrollbar sb;
    Data data;

    STAGE_LEVEL stageLevel = STAGE_LEVEL.EASY;
    STATE status = STATE.STAY;
    float nextPos = 0;
    float addValue = 0.1f;
    bool isPop = false;

    const float correctionAdd = 0.0001f; //調整移動量
    const float scrollSpeed = 1.0f;
    const string scrollkey = "scrollkey";

    public int CurrentStagenum
    {
        get{return currentStagenum;}
    }

    void CreateStageList()
    {
        //ステージをリストにセット
        for (int i = 1; i <= easy; i++)
            stageList.Add(10 + i);

        for (int i = 1; i <= normal; i++)
            stageList.Add(20 + i);

        for (int i = 1; i <= hard; i++)
            stageList.Add(30 + i);
    }

    void UpdateCurrentStageNum()
    {
        //選んでいるステージ更新
        currentStagenum = Mathf.FloorToInt(
               (nextPos - correction * (float)num) / (1.0f / (float)num - correction)
               + 0.001f);

        //テキストの番号も更新
        int w = stageList[currentStagenum] % 10;
        lvNum.sprite = numTex[w];
    }

    private void Start()
    {
        CFadeManager.FadeIn();

        sound[(int)SOUND.MOVE].Stop();
        sound[(int)SOUND.OK].Stop();
        sound[(int)SOUND.OPEN].Stop();
        sound[(int)SOUND.CANCEL].Stop();

        //スクロールの初期ポジションをセット
        //ステージに入った記憶がある場合そのステージから選択できる
        if (PlayerPrefs.HasKey(scrollkey))
            nextPos = correction * num + PlayerPrefs.GetInt(scrollkey) * (1.0f / (float)num - correction);
        else
            nextPos = correction * num;

        //スクロールバーのデータ読み込み
        sb = GetComponent<Scrollbar>();
        sb.value = nextPos;
 
        //ステージのステータスを読み込み
        data = Data.Instance;

        //ステージリストにステージの難易度(10の位)、番号(1の位)を入れる
        CreateStageList();

        //現在のステージ番号を更新
        UpdateCurrentStageNum();

        //ステージレベルをセット
        ChangeStageLevel();

        isPop = false;
    }

    void MoveSetting()
    {
        sound[(int)SOUND.MOVE].Play();

        //移動の値を変更
        addValue = (nextPos - sb.value) / scrollSpeed;

        //正の値だったら上
        //負の値だったら下
        if (addValue > 0)
        {
            batU.SetTrigger("fly");
            status = STATE.UP;
        }
        else
        {
            batD.SetTrigger("fly");
            status = STATE.DOWN;
        }

      

        //番号をいったん見えなくする
        color.a = 0;
        lvNum.color = color;
        level.color = color;

        UpdateCurrentStageNum();

        //ステージの難易度を更新
        ChangeStageLevel();
    }


    /////////////////////////////////////////////////////////////////////////////


    void Update()
    {
        if (isPop)
            return;

        Check_Cont();

        //上下
        if (up || Input.GetKeyDown(KeyCode.W) ||con_U)
        {
            if (status == STATE.STAY)
            {
                //最上階未満なら通常道理、一個上に進む
                //最上階なら最下層に移動
                if (currentStagenum < stageList.Count - 1)
                {
                    nextPos = correction * num + (currentStagenum + 1) * (1.0f / (float)num - correction);
                }
                else
                {
                    nextPos = correction * num + 0 * (1.0f / (float)num - correction);
                }

                MoveSetting();
            }

            up = false;
        }

        if (down || Input.GetKeyDown(KeyCode.S) || con_D)
        {
            if (status == STATE.STAY)
            {
                //上：通常移動
                //下：一番上に跳ぶ    
                if (currentStagenum > 0)
                {

                    nextPos = correction * num + (currentStagenum - 1) * (1.0f / (float)num - correction);
                }
                else
                {
                    nextPos = correction * num + (stageList.Count - 1) * (1.0f / (float)num - correction);

                }

                MoveSetting();
            }


            down = false;
        }

        //難易度移動
        if (Input.GetKeyDown(KeyCode.D) || con_R)
        {
            if (status == STATE.STAY)
            {
                //ステージの難易度スキップ
                MoveStageLevel();

                MoveSetting();

            }
        }

        if (Input.GetKeyDown(KeyCode.A) || con_L)
        {
            if (status == STATE.STAY)
            {
                MoveStageLevel(false);

                MoveSetting();
            }

        }

        //微調整用
        if (a)
        {
            sb.value += correctionAdd;
            a = false;
            correction += correctionAdd;
        }
        if (d)
        {
            sb.value -= correctionAdd;
            d = false;
            correction -= correctionAdd;
        }


        switch (status)
        {
            case STATE.UP:
                MoveUp();
                break;

            case STATE.DOWN:
                MoveDown();
                break;

            default:
                //番号を表示
                if (color.a < 1)
                {
                    color.a += 1.5f * Time.deltaTime;
                    lvNum.color = color;
                    level.color = color;
                }

                break;
        }

        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("OK"))
        {
            int yn = data.GetStageStatus(currentStagenum);

            Debug.Log(moveStageNum[currentStagenum]);

            sound[(int)SOUND.OK].Play();

            if (yn == (int)Data.STAGE_STATUS.NONE)
                Debug.Log("未開放");
            else if (yn == (int)Data.STAGE_STATUS.OPEN)
            {
               // CFadeManager.FadeOut(moveStageNum[currentStagenum]);

                pop.SetActive(true);
                isPop = true;
            }
                
            else if (yn == (int)Data.STAGE_STATUS.CLEAR)
            {
                // CFadeManager.FadeOut(moveStageNum[currentStagenum]);

                pop.SetActive(true);
                isPop = true;
            }

            PlayerPrefs.SetInt(scrollkey, currentStagenum);
            PlayerPrefs.Save();
        }

        if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("NO"))
        {
            sound[(int)SOUND.CANCEL].Play();
            CFadeManager.FadeOut(0);
            isPop = true;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void MoveUp()
    {
        if (sb.value >= nextPos)
        {
            sb.value = nextPos;
            status = STATE.STAY;
            return;
        }

        sb.value += addValue * Time.deltaTime;
    }

    void MoveDown()
    {
        if (sb.value <= nextPos||Mathf.Abs(addValue)<0.01f)
        {
            sb.value = nextPos;
            status = STATE.STAY;
            return;
        }

        sb.value += addValue * Time.deltaTime;
    }


    ///////////////////////////////////////////////////////////////////////////////////////

    public void Open(int stagenum)
    {
        if (stagenum > stageList.Count - 1)
        {
            Debug.Log("最大");
            return;
        }


        if (currentStagenum < stagenum)
        {
            batU.SetTrigger("fly");
            status = STATE.UP;
            nextPos = correction * num + stagenum * (1.0f / (float)num - correction);

            addValue = (nextPos - sb.value) / scrollSpeed;

            //選んでいるステージ更新
            currentStagenum = Mathf.FloorToInt(
                   (nextPos - correction * (float)num) / (1.0f / (float)num - correction)
                   + 0.001f);

            //番号テキストの変更
            int w;
            w = stageList[currentStagenum] % 10;
            lvNum.sprite = numTex[w];

            ChangeStageLevel();

            data.SetStageStatus(stagenum, Data.STAGE_STATUS.OPEN);

            sound[(int)SOUND.OPEN].Play();

        }
    }

    public void ChangeStageLevel()
    {
        int lv0, lv1, lv2, lv3;
        lv0 = 0;
        lv1 = easy;
        lv2 = lv1 + normal;
        lv3 = lv2 + hard - 1;


        if (currentStagenum == lv0)
        {
            stageLevel = STAGE_LEVEL.FIRST;
        }
        else if (currentStagenum < lv1)
        {
            stageLevel = STAGE_LEVEL.EASY;
        }
        else if (currentStagenum < lv2)
        {
            stageLevel = STAGE_LEVEL.NORMAL;
        }
        else if (currentStagenum < lv3)
        {
            stageLevel = STAGE_LEVEL.HARD;
        }
        else if(currentStagenum == lv3)
        {
            stageLevel = STAGE_LEVEL.END;
        }

        int sl = 0;

        switch (stageLevel)
        {
            case STAGE_LEVEL.NORMAL:
                sl = 1;
                break;
            case STAGE_LEVEL.HARD:
                sl = 2;
                break;
            case STAGE_LEVEL.END:
                sl = 2;
                break;
        }

        level.sprite = lvTex[sl];
    }

    void MoveStageLevel(bool isUp = true)
    {
        int lv1, lv2, lv3, lv4;
        lv1 = 0;
        lv2 = easy;
        lv3 = lv2 + normal;
        lv4 = lv3 + hard - 1;

        if (isUp)
        {
            switch (stageLevel)
            {
                case STAGE_LEVEL.FIRST:
                    nextPos = correction * num + lv2 * (1.0f / (float)num - correction);
                    break;

                case STAGE_LEVEL.EASY:
                    nextPos = correction * num + lv2 * (1.0f / (float)num - correction);
                    break;

                case STAGE_LEVEL.NORMAL:
                    nextPos = correction * num + lv3 * (1.0f / (float)num - correction);
                    break;

                case STAGE_LEVEL.HARD:
                    nextPos = correction * num + lv4 * (1.0f / (float)num - correction);
                    break;

                case STAGE_LEVEL.END:
                    nextPos = correction * num + lv1 * (1.0f / (float)num - correction);
                    break;
            }
        }
        else
        {

            if (stageLevel == STAGE_LEVEL.FIRST)
                nextPos = correction * num + lv4 * (1.0f / (float)num - correction);
            else if (stageLevel==STAGE_LEVEL.EASY)
                nextPos = correction * num + lv1 * (1.0f / (float)num - correction);
            else if (stageLevel == STAGE_LEVEL.NORMAL)
                nextPos = correction * num + lv1 * (1.0f / (float)num - correction);
            else if (stageLevel == STAGE_LEVEL.HARD)
                nextPos = correction * num + lv2 * (1.0f / (float)num - correction);
            else if (stageLevel == STAGE_LEVEL.END)
                nextPos = correction * num + lv3 * (1.0f / (float)num - correction);
        }
    }


    public void JudgeYesOrNo(bool isEnter)
    {
        if (isEnter)
        {
            sound[(int)SOUND.OK].Play();
            CFadeManager.FadeOut(moveStageNum[currentStagenum]);
        }
        else
        {
            sound[(int)SOUND.CANCEL].Play();
            pop.SetActive(false);
            isPop = false;
        }
    }

    private void Check_Cont()
    {
        float LR;
        float UD;
        LR = Input.GetAxis("Horizontal_p"); //右ぷら
        UD = Input.GetAxis("Vertical_p"); //上ぷら

        con_L = false;
        con_R = false;
        con_U = false;
        con_D = false;

        if (LR > 0.5f)
        {
            con_R = true;
        }

        if (LR < -0.5f)
        {
            con_L = true;
        }

        if (UD > 0.5f)
        {
            con_U = true;
        }

        if (UD < -0.5f)
        {
            con_D = true;
        }
    }
}
