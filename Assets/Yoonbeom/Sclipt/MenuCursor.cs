/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.SceneManagement;
public class MenuCursor : MonoBehaviour
{
    [SerializeField]
    private int Velocity;
    private float yMove;
    private int WaitTime = 0;
    private int Step = 1;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        CursorMove();
        ChoiceStage();
    }
    private void CursorMove()
    {
        yMove = 0;
        WaitTime--;
        if (Input.GetKey(KeyCode.UpArrow) && WaitTime < 0 && Step > 1)
        {
            yMove = Velocity * Time.deltaTime;
            WaitTime = 30;
            Step--;
        }
        if (Input.GetKey(KeyCode.DownArrow) && WaitTime < 0 && Step < 3)
        {
            yMove = -Velocity * Time.deltaTime;
            WaitTime = 30;
            Step++;
        }

        this.transform.Translate(new Vector3(0, yMove, 0));

    }

    private void ChoiceStage()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            switch (Step)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;

            }
        }
        
    }
}
*/