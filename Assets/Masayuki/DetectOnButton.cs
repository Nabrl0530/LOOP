using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        this.transform.parent.GetComponent<ButtonController>().PressButton();
    }

    private void OnTriggerExit(Collider other)
    {
        this.transform.parent.GetComponent<ButtonController>().ReleaseButton();
    }
}
