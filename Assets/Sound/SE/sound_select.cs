using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_select : MonoBehaviour
{
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        audio.Play();
    }
    public void Stop()
    {
        audio.Stop();
    }
}
