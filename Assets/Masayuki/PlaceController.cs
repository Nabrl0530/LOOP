using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlaceManager : MonoBehaviour
{
    private enum FLOOR_NUMBER
    {
        ONE,
        TWO,
        THREE,
    }

    private enum HEIGHT_PRESET
    {
        ONE,
        TWO,
        THREE
    }


    [SerializeField]
    private FLOOR_NUMBER m_floor_number = FLOOR_NUMBER.ONE;

    [SerializeField]
    [Range(0.0f, 360.0f)]
    private float m_angle = 0.0f;

    [SerializeField]
    private float m_snap_angle = 30.0f;

    [SerializeField]
    [Range(0.0f, 6.0f)]
    private float m_distance = 0.0f;

    [SerializeField]
    private bool m_use_default_distance = true;


    [SerializeField]
    private HEIGHT_PRESET m_heiht_preset = HEIGHT_PRESET.ONE;

    [SerializeField]
    [Range(0.0f, 6.0f)]
    private float m_height = 0.0f;

    [SerializeField]
    private bool m_use_default_height = true;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject parent_pipe = null;

        parent_pipe = Floor_Number_Specific();

        if (parent_pipe != null)
        {
            this.gameObject.transform.parent = parent_pipe.transform;
        }

        Adjust_Angle(m_snap_angle);

        Transform my_transform = this.gameObject.transform;
        Vector3 my_pos = my_transform.position;

        Vector3 target_vec = parent_pipe.transform.forward;

        Vector3 result_vec = Quaternion.AngleAxis(m_angle, my_transform.up) * target_vec;

        my_pos.x = result_vec.x * m_distance;
        my_pos.y = result_vec.y * m_distance;
        my_pos.z = result_vec.z * m_distance;

        if (m_use_default_height)
        {
            if (m_heiht_preset == HEIGHT_PRESET.ONE)
            {
                m_height = 0.0f;
            }
            else
            if (m_heiht_preset == HEIGHT_PRESET.TWO)
            {
                m_height = 2.0f;
            }
            else
            if (m_heiht_preset == HEIGHT_PRESET.THREE)
            {
                m_height = 4.0f;
            }
        }

        my_pos += my_transform.up * m_height;


        this.gameObject.transform.position = my_pos;
    }

    private void Adjust_Angle(float angle)
    {
        if (angle <= 0)
        {
            return;
        }
        for (float i = 0; i < 360; i += angle)
        {
            if (i < m_angle && m_angle <= i + angle)
            {
                m_angle = i + angle;
            }
        }
    }

    //選択されたフロアナンバー固有の処理を行う
    //return GameObject 親となるパイプのGameObject
    private GameObject Floor_Number_Specific()
    {
        GameObject parent_pipe = null;
        if (m_floor_number == FLOOR_NUMBER.ONE)
        {
            parent_pipe = GameObject.FindGameObjectWithTag("ONE");
            if (m_use_default_distance)
            {
                m_distance = 2.4f;
            }
        }
        else
        if (m_floor_number == FLOOR_NUMBER.TWO)
        {
            parent_pipe = GameObject.FindGameObjectWithTag("TWO");
            if (m_use_default_distance)
            {
                m_distance = 3.7f;
            }
        }
        else
        if (m_floor_number == FLOOR_NUMBER.THREE)
        {
            parent_pipe = GameObject.FindGameObjectWithTag("THREE");
            if (m_use_default_distance)
            {
                m_distance = 5.1f;
            }
        }

        return parent_pipe;
    }
}
