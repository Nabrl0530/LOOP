using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ドアにレーザが当たった時に、ドアを開く処理の管理
//ドアとレイとの当たり判定を行うコライダーのLayerはIgnore RaycastをTagはRayCollederDoorをセットする必要があります
public class DoorController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("描画される方のドア")]
    private GameObject m_visible_door = default;
    [SerializeField]
    [Tooltip("レイとの当たり判定用のドア")]
    private GameObject m_ray_collider_door = default;
    [SerializeField]
    [Tooltip("レイが当たった時にドアを開くか")]
    private bool m_open_door_when_ray_hit = true;
    [SerializeField]
    [Tooltip("ドアを何秒で開くようにするか")]
    private float m_move_time = 3.0f;
    [SerializeField]
    [Tooltip("ドアの動く距離。Y軸方向に動きます")]
    private float m_moving_distance = -2.05f;
    [SerializeField]
    [Tooltip("このリストにボタンを入れた場合は、リスト内にあるどのボタンを押してもドアが開きます。")]
    private List<GameObject> m_or_buttons = default;
    [SerializeField]
    [Tooltip("このリストにボタンを入れた場合は、リスト内にある全てのボタンを押さなければドアが開きません。")]
    private List<GameObject> m_and_buttons = default;


   

    private MoveController m_move_controller = default;

    private bool m_collision_with_light = false;
    //ボタンが押されてドアを開く処理を行っている場合true
    private bool m_request_open_door_from_button = false;
    //レイと衝突していたらドアを開く
    //details   一番近いドアの当たり判定に当たると以降、直線上にドアの当たり判定があってもドアを開く処理を行いません。
    //          また、ドアとレイとの当たり判定を行うコライダーのLayerはIgnore RaycastをTagはRayCollederDoorをセットする必要があります。
    public static void CollideWithRayOpenDoor(Vector3 origin, Vector3 direction)
    {
        int layer = 0;
        layer = ~layer;
        RaycastHit[] hit = Physics.RaycastAll(origin, direction, 30.0f, layer);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("RayColliderDoor"))
            {
                DoorController temp = hit[i].collider.transform.parent.GetComponent<DoorController>();
                if (temp.m_open_door_when_ray_hit == true)
                {
                    temp.OpenDoor();
                    temp.m_collision_with_light = true;
                    return;
                }
            }
        }
        
        //GameObject[] ray_collider_doors = GameObject.FindGameObjectsWithTag("RayColliderDoor");
        //foreach (GameObject ray_collider_door in ray_collider_doors)
        //{
        //    DoorController temp = ray_collider_door.transform.parent.GetComponent<DoorController>();
            
    }

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 init_pos = m_ray_collider_door.transform.position;
        Vector3 finish_pos = CalcFinshPos();
        m_move_controller = new MoveController(m_visible_door.transform, init_pos,finish_pos,m_move_time);
    }

    // Update is called once per frame
    private void Update()
    {
        m_move_controller.SetInitPos(m_ray_collider_door.transform.position);
        m_move_controller.SetFinishPos(CalcFinshPos());
        CheckButton();
    }

    private void LateUpdate()
    {
            if (m_collision_with_light == false && m_request_open_door_from_button == false)
            {
                CloseDoor();
            }
        
        m_collision_with_light = false;
        m_request_open_door_from_button = false;
    }

    private void OpenDoor()
    {
        m_move_controller.MoveFinishPos();
        
    }

    private void CloseDoor()
    {
        m_move_controller.MoveInitPos();
    }

    private Vector3 CalcFinshPos()
    {
        Vector3 pos = m_ray_collider_door.transform.position;
        pos  += m_ray_collider_door.transform.up * m_moving_distance;
        return pos;
    }

    private void CheckButton()
    {
        bool can_open_door = false;
        foreach (var and_button in m_and_buttons)
        {
            if (and_button.GetComponent<ButtonController>().PressingButton() == false)
            {
                can_open_door = false;
                break;
            }
            else
            {
                can_open_door = true;
            }
        }
        foreach (var or_button in m_or_buttons)
        {
            if(or_button.GetComponent<ButtonController>().PressingButton() == true)
            {
                can_open_door = true;
            }
        }
        if (can_open_door == true && m_collision_with_light == false)
        {
            m_request_open_door_from_button = true;
            OpenDoor();
        }
    }
}
