using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorsorMove : MonoBehaviour
{
    [SerializeField]
    private int Speed ;

    private float yMove;
    private int Turm = 0;
    private int Step =1;
    private int NowStep;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {

        MoveCursor();
        ChoiceStage();
       
    }

    private void ChoiceStage()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            switch (Step)
            {
                case 1:
                    SceneManager.LoadScene("Stage1");
                    break;
            }
        }
    }
    private void MoveCursor()
    {
        yMove = 0;
        Turm--;
        if (Input.GetKey(KeyCode.UpArrow) && Turm < 0 && Step < 6)
        {
            yMove = Speed * Time.deltaTime;
            Turm = 30;
            Step++;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Turm < 0 && Step > 1)
        {
            yMove = -Speed * Time.deltaTime;
            Turm = 30;
            Step--;
        }

        this.transform.Translate(new Vector3(0, yMove, 0));

    }
}
