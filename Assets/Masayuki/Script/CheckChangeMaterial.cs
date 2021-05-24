using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChangeMaterial : MonoBehaviour
{
    private GameObject m_camera = default;

    [SerializeField]
    private Material m_default_mat = default;
    [SerializeField]
    private Material m_silhouette_mat = default;
    [SerializeField]
    private List<string> m_tag_list = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer mesh = this.GetComponent<MeshRenderer>();
        Vector3 direction = this.transform.position - m_camera.transform.position; 
        RaycastHit hit;
        if (Physics.Raycast(m_camera.transform.position, direction, out hit))
        {
            bool is_changed = false;
            //ÉåÉCÇ…è·äQï®ÉåÉCÉÑÅ[Ç…ì¸Ç¡ÇƒÇÈï®Ç™ìñÇΩÇ¡ÇΩÇÁ
            Debug.Log(hit.collider.tag);
            foreach(string str in m_tag_list)
            {
                if (hit.collider.tag == str)
                {
                    mesh.material = m_silhouette_mat;
                    is_changed = true;
                }
            }
            if (!is_changed)
            {
                mesh.material = m_default_mat;
            }
           
        }
        else
        {
            mesh.material = m_default_mat;
        }
    }
}
