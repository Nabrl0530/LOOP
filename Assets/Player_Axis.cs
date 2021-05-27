using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Axis : MonoBehaviour
{
    public Player_State sc_state;
    // 変数
    public GameObject child;
    public float len = 1.0f;
    Vector3 Position_Latest_m;
    [SerializeField]
    private float RotateSpeed = 1;
    [SerializeField]
    private float Rotate_Tolerance_Block = 0.1f;

    private Vector3 View_Direction; //向かなきゃいけない方向
    float Speed_Move = 40;

    Rigidbody Rigid;
    bool Use;
    BoxCollider col;

    // Start is called before the first frame update
    void Start()
    {
        // 過去の位置
        Position_Latest_m = this.transform.position;

        Rigid = this.GetComponent<Rigidbody>();
        col = this.GetComponent<BoxCollider>();

        View_Direction = new Vector3(0, 0, -1);
        Use = false;       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        // 原田君用('ω')
        if (sc_state.Get_AnimationState() == (int)Player_State.e_PlayerAnimationState.PUSH_PUSHING)
        {
            // 情報
            Vector3 difference = this.transform.position - Position_Latest_m;
            Position_Latest_m = this.transform.position;
            // 位置回転更新
            child.transform.parent = null;

            Vector3 pos = new Vector3(0, 0, 0);
            pos = child.transform.position;
            pos += child.transform.forward * len;
            this.transform.position = pos;
            this.transform.rotation = child.transform.rotation;

            child.transform.parent = this.transform;

            // 回転
            difference.y = 0;
            if (difference.magnitude > Rotate_Tolerance_Block)
            {
                // 回転計算//親を回転
                Quaternion rot = Quaternion.LookRotation(difference);
                rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed);
                this.transform.rotation = rot;
            }//difference.magnitude > Rotate_Tolerance
        }
        */

        if(Use)
        {
            Quaternion rot = Quaternion.LookRotation(View_Direction);

            rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed);
            this.transform.rotation = rot;

            Rigid.velocity *= 0.95f;
            
        }       
    }

    public void Addspeed()
    {
        if (Rigid.velocity.magnitude < 4)
        {
            Vector3 vec_m = transform.forward;
            //vec_m.y += 0.35f;
            Rigid.AddForce(vec_m * Speed_Move);
            //Debug.Log(Rigid.velocity.magnitude);
        }
    }

    public void Set_View(Vector3 view)
    {
        View_Direction = view;
    }

    public void SetUse(bool _is)
    {
        Use = _is;

        col.enabled = true;

        if(!Use)
        {
            col.enabled = false;
            Rigid.velocity = new Vector3(0, 0, 0);
        }
    }

    public void LookConect(Vector3 view)
    {
        this.transform.forward = view;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
