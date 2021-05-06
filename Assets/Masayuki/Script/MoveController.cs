using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{
    //現在のMoveControllerの状態
    public enum Status
    {
        INIT_POS,//初期位置にいる。
        MOVING,//初期位置または移動終了位置に向かって移動中。
        FINISH_POS//終了位置にいる。
    }

    private Transform m_target = default;
    private Status m_status = Status.INIT_POS;
    //初期位置
    private Vector3 m_init_pos;
    //移動終了位置
    private Vector3 m_finish_pos;
    //移動完了までにかかる秒数
    private float m_move_seconds = 3.0f;
    //現在の秒数
    private float m_now_move_time = 0.0f;

    public MoveController(Transform target ,Vector3 init_pos,Vector3 finish_pos,float move_seconds)
    {
        m_target = target;
        m_init_pos = init_pos;
        m_finish_pos = finish_pos;
        m_move_seconds = move_seconds;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Status GetStatus()
    {
        return m_status;
    }

    public void SetInitPos(Vector3 init_pos)
    {
        m_init_pos = init_pos;
    }
    public void SetFinishPos(Vector3 finish_pos)
    {
        m_finish_pos = finish_pos;
    }

    //終了位置へ動く
    //details  この関数を使う場合は毎フレーム呼ぶこと
    public void MoveFinishPos()
    {
        m_status = Status.MOVING;
        m_now_move_time += Time.deltaTime;
        float ratio = m_now_move_time / m_move_seconds;
        if (ratio >= 1.0f)
        {
            m_now_move_time = m_move_seconds;
            ratio = 1.0f;
            m_status = Status.FINISH_POS;
        }
        
        m_target.position = Vector3.Lerp(m_init_pos, m_finish_pos, ratio);
    }

    //初期位置へ動く
    //details この関数を使う場合は毎フレーム呼ぶこと
    public void MoveInitPos()
    {
        m_status = Status.MOVING;
        m_now_move_time -= Time.deltaTime;
        float ratio = m_now_move_time / m_move_seconds;
        if (ratio <= 0.0f)
        {
            m_now_move_time = 0.0f;
            ratio = 0.0f;
            m_status = Status.INIT_POS;
        }
        m_target.position = Vector3.Lerp(m_init_pos, m_finish_pos, ratio);
    }
}
