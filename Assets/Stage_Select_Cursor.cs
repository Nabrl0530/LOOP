using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Select_Cursor : MonoBehaviour
{
    RectTransform rt;
    public Image Image;
    bool Next;

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        Image.color = new Color(1, 1, 1, 0);
        Next = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Next)
        {
            rt.position = new Vector2(1100, 465);
        }
        else
        {
            rt.position = new Vector2(760, 465);
        }
        
    }

    public void SetON()
    {
        Image.color = new Color(1, 1, 1, 1);
    }

    public void SetOFF()
    {
        Image.color = new Color(1, 1, 1, 0);
        Next = true;
    }

    public void SetNext()
    {
        Next = !Next;
    }
}
