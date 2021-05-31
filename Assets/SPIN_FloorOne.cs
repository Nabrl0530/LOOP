using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPIN_FloorOne : MonoBehaviour
{
    int Spin;
    int count;




    // サウンドmiya
    public sound_round sc_round;



    // Start is called before the first frame update
    void Start()
    {
        Spin = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(count > 0)
        {
            transform.Rotate(0, 1 * Spin, 0);
            count--;

            if(count == 0)
            {
                Spin = 0;



                // サウンドmiya
                if (sc_round) sc_round.Stop();
            }
        }
    }

    public void SetSpin(int spin)
    {
        if(count == 0 && Spin == 0)
        {
            count = 45;
            Spin = spin;


            // サウンドmiya
            if (sc_round) sc_round.Play();
        }        
    }
}
