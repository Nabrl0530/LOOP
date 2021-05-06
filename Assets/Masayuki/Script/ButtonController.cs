using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ボタンとなるオブジェクトを入れてください。")]
    private GameObject m_button = default;
    [SerializeField]
    [Tooltip("ボタンを押した時に何秒かけてボタンの位置を下げるか")]
    private float m_move_time = 2.0f;
    [SerializeField]
    [Tooltip("ボタンを押した時にどれだけ下がるか")]
    private float m_down_limit = -0.23f;

    private GameObject m_init_pos_remember = default;
    private MoveController m_move_controller = default;
    private bool m_release = false;

    //ボタンが押されているか
    //true押されている
    public bool PressingButton()
    {
        if (m_move_controller.GetStatus() == MoveController.Status.FINISH_POS)
        {
            return true;
        }
        return false;
    }

    public void PressButton()
    {
        m_move_controller.MoveFinishPos();
    }
    public void ReleaseButton()
    {
        m_release = true;
    }

    // Start is called before the first frame update
    void Start()
    {

        m_init_pos_remember = new GameObject("InitPosRemember");
        m_init_pos_remember.transform.parent = m_button.transform.parent.transform;
        m_init_pos_remember.transform.localPosition = m_button.transform.localPosition;
        m_button.transform.parent = m_init_pos_remember.transform;

        Vector3 temp = new Vector3(0.0f, 0.0f, 0.0f);
        m_button.transform.localPosition = temp;

        {
            Vector3 init_pos = m_init_pos_remember.transform.position;
            Vector3 finish_pos = CalcFinishPos();
            m_move_controller = new MoveController(m_button.transform, init_pos, finish_pos, m_move_time);
        }

    }

    // Update is called once per frame
    void Update()
    {
        m_move_controller.SetInitPos(m_init_pos_remember.transform.position);
        m_move_controller.SetFinishPos(CalcFinishPos());
        if (m_release == true)
        {
            m_move_controller.MoveInitPos();
            if (m_move_controller.GetStatus() == MoveController.Status.INIT_POS)
            {
                m_release = false;
            }
        }

    }

    private void LateUpdate()
    {

    }


    private Vector3 CalcFinishPos()
    {
        Vector3 vec = m_init_pos_remember.transform.position;
        vec.y += m_down_limit;
        return vec;
    }
}
