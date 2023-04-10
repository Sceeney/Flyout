using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Listener : MonoBehaviour
{
    private AudioListener audio_List;
    public static float Listener;
    void Start()
    {
        audio_List = GetComponent<AudioListener>();
        Listener = 1f;
        Listener = PlayerPrefs.GetFloat("Listener");

    }

    void Update()
    {
            AudioListener.volume = Listener;
    }
}
