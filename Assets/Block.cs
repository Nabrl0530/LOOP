using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Door door;
    Door_HIT door_HIT;
    public Block_Under Block_Under;
    Player player;
    GameObject Pipe1;
    GameObject Pipe2;
    GameObject Pipe3;

    bool Warp_STANBY;
    bool ON_Player;
    bool HIT;
    bool WARP_NOW;
    int count = 0;
    int warp_count = 0;
    Vector3 Base_Size;

    public float len;  //長さ

    private float rot_z;   //回転速度
    private float Size = 1.0f;
    public Vector3 Act_move;  //アクションによる移動量
    public int undercount;

    // Start is called before the first frame update
    void Start()
    {
        Warp_STANBY = false;
        ON_Player = false;
        HIT = false;

        rot_z = 1.0f;
        undercount = 0;

        Base_Size = transform.localScale;

        Pipe1 = GameObject.Find("FloorOne");
        Pipe2 = GameObject.Find("FloorTwo");
        Pipe3 = GameObject.Find("FloorThree");
    }

    // Update is called once per frame
    void Update()
    {
        if (!WARP_NOW && !ON_Player &&  Block_Under.Get_HIT())
        {
            len = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
            if (len >= 12.0f)
            {
                transform.SetParent(Pipe3.transform);
            }
            else if (len >= 8.5f)
            {
                transform.SetParent(Pipe2.transform);
            }
            else
            {
                transform.SetParent(Pipe1.transform);
            }
        }
    }

    void FixedUpdate()
    {
        if(HIT)
        {
            count++;
            if(count == 30)
            {
                WARP_NOW = true;
                transform.parent = null;

                warp_count = 0;
                //物理挙動による移動の無効化
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                Size = 1.0f;
                rot_z = 1.0f;

                Act_move = (door_HIT.gameObject.transform.position - transform.position) / 50;
                Warp_STANBY = false;
            }
        }
        else
        {
            count = 0;
        }

        HIT = false;

        //ワープ処理

        if(WARP_NOW)
        {
            warp_count++;

            //額縁によるワープ移動（吸い込み）
            if (warp_count < 51)
            {
                transform.Rotate(0, 0, rot_z);

                rot_z += 1.5f;

                if (warp_count >= 25)
                {

                    Size -= 0.04f;

                    transform.localScale = new Vector3(Base_Size.x * Size, Base_Size.y * Size, Base_Size.z * Size);
                    transform.position += Act_move;
                }

                if (warp_count == 50)
                {
                    transform.position = door.Getpair_pos();

                    Quaternion angle = Quaternion.identity;

                    angle.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
                    transform.rotation = angle;

                    //出現時の向きを合わせる
                    Vector3 View = new Vector3(0, 0, 0);
                    View.y = transform.position.y;
                    this.transform.LookAt(View);
                }
            }
            else if (warp_count < 121)
            {
                if (warp_count == 71)
                {
                    Size = 0;
                }

                if (warp_count >= 71)
                {
                    transform.position += (transform.forward * 0.06f);
                    Size += 0.02f;
                    transform.localScale = new Vector3(Size, Size, Size);
                }

                if (warp_count == 120)
                {                    
                    this.gameObject.GetComponent<BoxCollider>().enabled = true;

                    Size = 1.0f;
                    transform.localScale = new Vector3(Base_Size.x * Size, Base_Size.y * Size, Base_Size.z * Size);                   
                }
            }
            else if(warp_count ==121)
            {
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                WARP_NOW = false;
            }
        }

        if(ON_Player)
        {
            if(!Block_Under.Get_HIT())
            {
                undercount++;
                if(undercount >= 5)
                {
                    player.block_out();
                }
            }
            else
            {
                undercount = 0;
            }
        }
    }

    public void Set_STANBY()
    {
        Warp_STANBY = true;
    }

    public void Set_ON()
    {
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        ON_Player = true;
        Warp_STANBY = true;
    }

    public void Clare_ON()
    {
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        ON_Player = false;
    }

    public void SetPlayer(Player scr)
    {
        player = scr;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Door_HIT"))
        {
            if(Warp_STANBY && !ON_Player)
            {
                HIT = true;
                door_HIT = other.gameObject.GetComponent<Door_HIT>();
                door = other.gameObject.GetComponent<Door_HIT>().GetDoor();
            }
        }
    }
}
