using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha_cont : MonoBehaviour
{
    GameObject camera_ob;
    Color color;
    public Vector3 to_vec;
    public float to_Angle;
    public Vector3 front;
    Material mat;
    Shader sh;

    public float a = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        camera_ob = GameObject.Find("Main Camera");
        //camera_ob = GameObject.Find("PlayerCenter");
        //color = this.gameObject.GetComponent<MeshRenderer>().material.color;
        // this.gameObject.GetComponent<MeshRenderer>().material.color

        //マテリアルの複製、セット できなかった（テクスチャが消える？）
        //sh = this.gameObject.GetComponent<MeshRenderer>().material.shader;
        //mat = new Material(sh);
        //this.gameObject.GetComponent<MeshRenderer>().material = mat;
        color = this.gameObject.GetComponent<MeshRenderer>().material.color;

        front = -this.gameObject.transform.position;
        front.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        to_vec = camera_ob.transform.position - this.gameObject.transform.position;
        to_vec.y = 0;
        to_Angle = Vector3.Angle(front, to_vec.normalized);

        if(to_Angle > 90)
        {

            color = this.gameObject.GetComponent<MeshRenderer>().material.color;

            a = 1 - (to_Angle - 45)/90;
            if(a < 0)
            {
                a = 0;
            }

            color.a = a;

            this.gameObject.GetComponent<MeshRenderer>().material.color = color;
        }
        else
        {
            color.a = 1.0f;
            this.gameObject.GetComponent<MeshRenderer>().material.color = color;
        }

        //color = this.gameObject.GetComponent<MeshRenderer>().material.color;

        //color.a = a;

        //this.gameObject.GetComponent<MeshRenderer>().material.color = color;

        //Vector3 rot = Quaternion.Euler(0, 45, 0) * transform.forward;
        //Vector3 rot = Vector3.forward;

        //transform.Translate(rot * 0.005f);

        //front = transform.forward;
    }
}
