using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAchievement : MonoBehaviour
{
    Data data;
    [SerializeField] Scroll scroll;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }


    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.1f);
        data = Data.Instance;
        string s;

        for(int i=0;i<13;i++)
        {
            s = "stage" + (i+1).ToString("00");
            if (data.GetStageStatus(i) == (int)Data.STAGE_STATUS.NONE)
                transform.Find(s).Find("lock").gameObject.SetActive(true);

            else if(data.GetStageStatus(i) == (int)Data.STAGE_STATUS.NEW)
            {
                GameObject obj = transform.Find(s).Find("lock").gameObject;
                obj.SetActive(true);
                obj.GetComponent<Animator>().SetTrigger("open");
                scroll.Open(i);
            }
        }
    }
}
