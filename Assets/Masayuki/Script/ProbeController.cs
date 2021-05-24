using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProbeController : MonoBehaviour
{
    private GameObject m_camera = default;

    ReflectionProbe probe;

    void Start()
    {
        this.probe = GetComponent<ReflectionProbe>();
        m_camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {

        this.probe.transform.position = m_camera.transform.forward * 0.5f ;

        probe.RenderProbe();
    }
}