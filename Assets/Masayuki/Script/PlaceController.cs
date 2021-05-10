using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlaceController : MonoBehaviour
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
    [Tooltip("���̉��w�ڂɐe�q�t�����邩?")]
    private FLOOR_NUMBER m_floor_number = FLOOR_NUMBER.ONE;

    [SerializeField]
    [Tooltip("���x�̈ʒu�ɔz�u���邩")]
    [Range(0.0f, 360.0f)]
    private float m_place_angle = 0.0f;

    [SerializeField]
    [Tooltip("�z�u���̃X�i�b�v")]
    private float m_snap_angle = 45.0f;

    [SerializeField]
    [Tooltip("�z�u�p�x�Ǝ��g�̊p�x�����킹�邩")]
    private bool m_adjust_place_angle_and_rotation = true;

    [SerializeField]
    [Tooltip("��]�̕␳�l")]
    private Vector3 m_offset_rotation = default;

    [SerializeField]
    [Range(0.0f, 17.0f)]
    [Tooltip("�X�e�[�W���S����ǂꂾ������邩")]
    private float m_distance = 0.0f;

    [SerializeField]
    [Tooltip("�X�e�[�W���S����ǂꂾ������邩�̃p�����[�^�[�ɁA�f�t�H���g�l���g�p���邩")]
    private bool m_use_default_distance = false;


    [SerializeField]
    [Tooltip("�����̃v���Z�b�g")]
    private HEIGHT_PRESET m_heiht_preset = HEIGHT_PRESET.ONE;

    [SerializeField]
    [Range(0.0f, 6.0f)]
    [Tooltip("�����𒲐��ł��܂�")]
    private float m_height = 0.0f;

    [SerializeField]
    [Tooltip("�����𒲐����̃X�i�b�v")]
    private float m_snap_height = 0.0f;

    [SerializeField]
    [Tooltip("�����̒����Ƀf�t�H���g�l���g�p���邩")]
    private bool m_use_default_height = false;





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

        Vector3 result_vec = Quaternion.AngleAxis(m_place_angle, my_transform.up) * target_vec;

        my_pos.x = result_vec.x * m_distance;
        my_pos.y = result_vec.y * m_distance;
        my_pos.z = result_vec.z * m_distance;

        if (m_use_default_height)
        {
            if (m_heiht_preset == HEIGHT_PRESET.ONE)
            {
                m_height = 1.23f;
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

        Adjust_Height(m_snap_height);
        my_pos += my_transform.up * m_height;


        this.gameObject.transform.position = my_pos;

        if(m_adjust_place_angle_and_rotation)
        {
            Vector3 parent_rot = this.gameObject.transform.parent.rotation.eulerAngles;
            this.gameObject.transform.rotation = Quaternion.Euler(parent_rot.x + m_offset_rotation.x,parent_rot.y + m_offset_rotation.y + m_place_angle,parent_rot.z + m_offset_rotation.z);
        }
    }

    private void Adjust_Angle(float angle)
    {
        if (angle <= 0)
        {
            return;
        }
        Snap(0.0f, 360.0f, angle, ref m_place_angle);
    }

    private void Adjust_Height(float height)
    {
        if(height <= 0)
        {
            return;
        }
        Snap(0.0f, 9.0f, height, ref m_height);
    }

    private void Snap(float initial,float finish,float add,ref float target)
    {
        for(float i = initial; i < finish; i += add)
        {
            if(i < target && target <= i + add)
            {
                target = i + add ;
            }
        }
    }

    //�I�����ꂽ�t���A�i���o�[�ŗL�̏������s��
    //return GameObject �e�ƂȂ�p�C�v��GameObject
    private GameObject Floor_Number_Specific()
    {
        GameObject parent_pipe = null;
        if (m_floor_number == FLOOR_NUMBER.ONE)
        {
            parent_pipe = GameObject.FindGameObjectWithTag("ONE");
            if (m_use_default_distance)
            {
                m_distance = 7.25f;
            }
        }
        else
        if (m_floor_number == FLOOR_NUMBER.TWO)
        {
            parent_pipe = GameObject.FindGameObjectWithTag("TWO");
            if (m_use_default_distance)
            {
                m_distance = 10.7f;
            }
        }
        else
        if (m_floor_number == FLOOR_NUMBER.THREE)
        {
            parent_pipe = GameObject.FindGameObjectWithTag("THREE");
            if (m_use_default_distance)
            {
                m_distance = 14.56f;
            }
        }

        return parent_pipe;
    }
}