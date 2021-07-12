using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPIN_FloorOne : MonoBehaviour
{
    int Spin;
    int count;

    bool NO_SPIN;
    Player player;

    // サウンドmiya
    public sound_round sc_round;



    // Start is called before the first frame update
    void Start()
    {
        Spin = 0;
        count = 0;

        player = GameObject.Find("PlayerCenter").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(NO_SPIN)
        {
            return;
        }

        if(count > 0)
        {
            transform.Rotate(0, 1 * Spin, 0);
            player.Spin_Stage(Spin);
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
        if(NO_SPIN)
        {
            return;
        }

        if(count == 0 && Spin == 0)
        {
            count = 45;
            Spin = spin;


            // サウンドmiya
            if (sc_round) sc_round.Play();
        }        
    }

    public void Set_No_SPIN(bool _is)
    {
        NO_SPIN = _is;
    }

    public bool SPIN_NOW()
    {
        if (count > 0)
        {
            return true;
        }

        return false;
    }
}
