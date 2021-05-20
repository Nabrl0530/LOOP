using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeimg;
    // Start is called before the first frame update
    void Start()
    {
        fadeimg.SetActive(true);
 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
