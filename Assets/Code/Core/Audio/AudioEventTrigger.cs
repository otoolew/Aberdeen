using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventTrigger : MonoBehaviour {
    public AudioEvent m_BasicAudioEvent;
    public AudioSource m_EventAudio;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_BasicAudioEvent.Play(m_EventAudio);
            Debug.Log("Playing AudioEventTrigger");
        }
	}
}
