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

    int pos_hole1;  //穴１の高さ
    int pos_hole2;  //穴２の高さ
    int pos_hole_b; //変更前の高さ
    int rot_hole1;  //穴１の回転
    int rot_hole2;  //穴２の回転
    Vector3 moveVec;

    int MOVE;
    int SPIN;

    int count;  //移動、回転処理カウント

    // Start is called before the first frame update
    void Start()
    {
        MOVE = 0;
        SPIN = 0;
        pos_hole1 = 1;
        pos_hole2 = 2;
        rot_hole1 = 1;
        rot_hole2 = 4;
        moveVec = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //穴１の高さ変更
        if (Input.GetKey(KeyCode.J) && MOVE == 0 && SPIN == 0)
        {
            MOVE = 1;
            count = 90;



            //位置を上に移動
            pos_hole1++;

            //上限を超えたら一番下に
            if(pos_hole1 == 4)
            {
                pos_hole1 = 1;
            }
            
            //同じ向きで穴の高さが重なる場合はもう一段進める
            if(pos_hole1 == pos_hole2 && rot_hole1 == rot_hole2)
            {
                pos_hole1++;
            }
            if (pos_hole1 == 4)
            {
                pos_hole1 = 1;
            }




        }
        */
        /*
        //穴２の高さ変更
        if (Input.GetKey(KeyCode.K) && MOVE == 0 && SPIN == 0)
        {
            MOVE = 2;
            count = 90;
        }

        //穴１の向き変更
        if (Input.GetKey(KeyCode.U) && MOVE == 0 && SPIN == 0)
        {
            SPIN = 1;
            count = 90;
        }

        //穴２の向き変更
        if (Input.GetKey(KeyCode.I) && MOVE == 0 && SPIN == 0)
        {
            SPIN = 2;
            count = 90;
        }
        */
    }

    void FixedUpdate()
    {
        if (count > 0)
        {
            if (MOVE == 1)
            {
                hole1.transform.position += moveVec;
            }

            if (MOVE == 2)
            {
                hole2.transform.position += moveVec;
            }

            if (SPIN == 1)
            {
                hole1_base.transform.Rotate(0, 90 / 5, 0);
            }

            if (SPIN == 2)
            {
                hole2_base.transform.Rotate(0, 90 / 5, 0);
            }

            if (SPIN == 3)
            {
                hole3_base.transform.Rotate(0, 90 / 5, 0);
            }

            count--;
        }

        if(count > 0)
        {
            count--;

            if(MOVE != 0 && count == 0)
            {
                MOVE = 0;
            }

            if (SPIN != 0 && count == 0)
            {
                SPIN = 0;
            }
        }

    }

    //穴１の高さ変更
    public void HoleMove_1()
    {
        if(MOVE !=0 || count != 0)
        {
            return;
        }

        //位置を上に移動
        pos_hole_b = pos_hole1;
        pos_hole1++;

        //上限を超えたら一番下に
        if (pos_hole1 == 4)
        {
            pos_hole1 = 1;
        }

        //同じ向きで穴の高さが重なる場合はもう一段進める
        //if (pos_hole1 == pos_hole2 && rot_hole1 == rot_hole2)
        if (pos_hole1 == pos_hole2)
        {
            pos_hole1++;
        }

        //やっぱり超えたら下段に
        if (pos_hole1 == 4)
        {
            pos_hole1 = 1;
        }

        //一段上に
        if(pos_hole1 == pos_hole_b + 1)
        {
            moveVec.y = 0.08f;
        }
        //二段上に
        else if(pos_hole1 == pos_hole_b + 2)
        {
            moveVec.y = 0.16f;
        }
        //一段下に
        else if(pos_hole1 == pos_hole_b - 1)
        {
            moveVec.y = -0.08f;
        }
        //二段下に
        else if (pos_hole1 == pos_hole_b - 2)
        {
            moveVec.y = -0.16f;
        }

        MOVE = 1;
        count = 50;

    }

    //穴２の高さ変更
    public void HoleMove_2()
    {
        if (MOVE != 0 || count != 0)
        {
            return;
        }

        //位置を上に移動
        pos_hole_b = pos_hole2;
        pos_hole2++;

        //上限を超えたら一番下に
        if (pos_hole2 == 4)
        {
            pos_hole2 = 1;
        }

        //同じ向きで穴の高さが重なる場合はもう一段進める
        //if (pos_hole1 == pos_hole2 && rot_hole1 == rot_hole2)
        if (pos_hole2 == pos_hole1)
        {
            pos_hole2++;
        }

        //やっぱり超えたら下段に
        if (pos_hole2 == 4)
        {
            pos_hole2 = 1;
        }

        //一段上に
        if (pos_hole2 == pos_hole_b + 1)
        {
            moveVec.y = 0.08f;
        }
        //二段上に
        else if (pos_hole2 == pos_hole_b + 2)
        {
            moveVec.y = 0.16f;
        }
        //一段下に
        else if (pos_hole2 == pos_hole_b - 1)
        {
            moveVec.y = -0.08f;
        }
        //二段下に
        else if (pos_hole2 == pos_hole_b - 2)
        {
            moveVec.y = -0.16f;
        }

        MOVE = 2;
        count = 50;
    }

    //穴１の向き変更
    public void HoleSpin_1()
    {
        if (SPIN != 0 || count != 0)
        {
            return;
        }

        if (pos_hole1 == 1)
        {
            SPIN = 1;
        }
        else if(pos_hole1 == 2)
        {
            SPIN = 2;
        }
        else if (pos_hole1 == 3)
        {
            SPIN = 3;
        }

        count = 50;
    }

    //穴２の向き変更
    public void HoleSpin_2()
    {
        if (SPIN != 0 || count != 0)
        {
            return;
        }

        if (pos_hole2 == 1)
        {
            SPIN = 1;
        }
        else if (pos_hole2 == 2)
        {
            SPIN = 2;
        }
        else if (pos_hole2 == 3)
        {
            SPIN = 3;
        }

        count = 50;
    }
}
