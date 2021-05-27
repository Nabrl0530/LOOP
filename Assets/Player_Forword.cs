using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Forword : MonoBehaviour
{
    // éQè∆
    public Player_State sc_state;
    public Player sc_move;

    // ïœêî
    GameObject m_Block = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PosReset()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        sc_state.Set_CanClimb_Forword(true);
        if (other.gameObject.tag == "Block")
        {
            sc_state.Set_IsBlock(true);
            m_Block = other.gameObject;
            sc_move.Set_Block(other.GetComponent<Block>());
        }
        if (other.gameObject.tag == "Stage")
        {
            sc_state.Set_IsStage(true);
        }
    }

    void OnTriggerSTAY(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            sc_state.Set_IsBlock(true);
        }
        if (other.gameObject.tag == "Stage")
        {
            sc_state.Set_IsStage(true);
        }
    }

        void OnTriggerExit(Collider other)
    {
        sc_state.Set_CanClimb_Forword(false);
        if (other.gameObject.tag == "Block")
        {
            sc_state.Set_IsBlock(false);
        }

        if (other.gameObject.tag == "Stage")
        {
            sc_state.Set_IsStage(false);
        }
    }

    public GameObject Get_Block()
    {
        return m_Block;
    }
}
