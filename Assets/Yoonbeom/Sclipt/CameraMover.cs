
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CameraMover : MonoBehaviour
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
        CameraMove();
    }
    private void CameraMove()
    {
        WaitTime--;

      
            yMove = 0;
            if (Input.GetKey(KeyCode.UpArrow) && WaitTime < 0 && Step < 5)
            {
                yMove = Velocity * Time.deltaTime;
                WaitTime = 30;
                Step++;
            }
            if (Input.GetKey(KeyCode.DownArrow) && WaitTime < 0 && Step > 1)
            {
                yMove = -Velocity * Time.deltaTime;
                WaitTime = 30;
                Step--;
            }
            this.gameObject.transform.Translate(new Vector3(0, yMove, 0));

        
      
    }

   




}
