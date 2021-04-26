using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CursorMover : MonoBehaviour
{
 [SerializeField]
 private int Velocity;
 private float xMove;

 private int WaitTime = 0;
 private int Step = 1;
 [SerializeField]
  private int Count;

    void Start()
    {
    }
    void Update()
    {
        

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        xMove = 0;
        WaitTime--;
        if (Input.GetKey(KeyCode.RightArrow) && WaitTime < 0 && Step <= 7)
        {
            xMove = Velocity * Time.deltaTime;
            WaitTime = 30;
            Step++;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && WaitTime < 0 && Step >= 1)
        {
            xMove = -Velocity * Time.deltaTime;
            WaitTime = 30;
            Step--;
        }

        if(Input.GetKey(KeyCode.Return))
        {
            switch(Step)
            {
                case 1:
                    SceneManager.LoadScene("yb_SampleScene");
                    break;
            }
        }

        this.transform.Translate(new Vector3(xMove, 0, 0));
    }

   

  
}
