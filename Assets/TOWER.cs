using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOWER : MonoBehaviour
{
    public GameObject hole1;
    public GameObject hole2;
    public GameObject hole1_base;
    public GameObject hole2_base;
    public GameObject hole3_base;

    int pos_hole1;  //ŒŠ‚P‚Ì‚‚³
    int pos_hole2;  //ŒŠ‚Q‚Ì‚‚³
    int pos_hole_b; //•ÏX‘O‚Ì‚‚³
    int rot_hole1;  //ŒŠ‚P‚Ì‰ñ“]
    int rot_hole2;  //ŒŠ‚Q‚Ì‰ñ“]

    int MOVE;
    int SPIN;

    int count;  //ˆÚ“®A‰ñ“]ˆ—ƒJƒEƒ“ƒg

    // Start is called before the first frame update
    void Start()
    {
        MOVE = 0;
        SPIN = 0;
        pos_hole1 = 1;
        pos_hole2 = 2;
        rot_hole1 = 1;
        rot_hole2 = 4;
    }

    // Update is called once per frame
    void Update()
    {
        //ŒŠ‚P‚Ì‚‚³•ÏX
        if (Input.GetKey(KeyCode.J) && MOVE == 0 && SPIN == 0)
        {
            MOVE = 1;
            count = 90;



            //ˆÊ’u‚ðã‚ÉˆÚ“®
            pos_hole1++;

            //ãŒÀ‚ð’´‚¦‚½‚çˆê”Ô‰º‚É
            if(pos_hole1 == 4)
            {
                pos_hole1 = 1;
            }
            
            //“¯‚¶Œü‚«‚ÅŒŠ‚Ì‚‚³‚ªd‚È‚éê‡‚Í‚à‚¤ˆê’ii‚ß‚é
            if(pos_hole1 == pos_hole2 && rot_hole1 == rot_hole2)
            {
                pos_hole1++;
            }
            if (pos_hole1 == 4)
            {
                pos_hole1 = 1;
            }




        }

        //ŒŠ‚Q‚Ì‚‚³•ÏX
        if (Input.GetKey(KeyCode.K) && MOVE == 0 && SPIN == 0)
        {
            MOVE = 2;
            count = 90;
        }

        //ŒŠ‚P‚ÌŒü‚«•ÏX
        if (Input.GetKey(KeyCode.U) && MOVE == 0 && SPIN == 0)
        {
            SPIN = 1;
            count = 90;
        }

        //ŒŠ‚Q‚ÌŒü‚«•ÏX
        if (Input.GetKey(KeyCode.I) && MOVE == 0 && SPIN == 0)
        {
            SPIN = 2;
            count = 90;
        }
    }

    void FixedUpdate()
    {
        if (count > 0)
        {
            if (MOVE == 1)
            {

            }

            if(SPIN == 1)
            {

            }

            count--;
        }

    }
}
