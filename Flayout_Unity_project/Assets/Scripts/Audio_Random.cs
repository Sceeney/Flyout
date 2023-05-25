using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Random : MonoBehaviour
{
 private AudioSource audioSource;
 public AudioClip[] shoot;
 private AudioClip shootClip;

        [Space(20)]
    public Slider Slider_Main_audio;
        [Space(5)]
    public AudioSource audio_Music;
    public Slider Slidel_Music;
        [Space(5)]
    private float music_Volume = 1f;

 void OnEnable()
 {
    audio_Music = GetComponent<AudioSource>();
        music_Volume = 1f;
        Slidel_Music.value = 1f;
        Slidel_Music.value = PlayerPrefs.GetFloat("Slidel_Music.value");
        Slider_Main_audio.value = PlayerPrefs.GetFloat("Listener"); 
        music_Volume = PlayerPrefs.GetFloat("music_Volume");

     audioSource = gameObject.GetComponent<AudioSource>();
        int index = Random.Range(0, shoot.Length);
         shootClip = shoot[index];
         audioSource.clip = shootClip;
         audioSource.Play();
    print("1");
 }
    void Update()
    {
        audio_Music.volume = music_Volume;
    }

    public void Volume_Music()
    {
        music_Volume = Slidel_Music.value;
        PlayerPrefs.SetFloat("music_Volume", music_Volume);
        PlayerPrefs.SetFloat("Slidel_Music.value", Slidel_Music.value);
    }
    public void Main_audio()
    {
        Audio_Listener.Listener = Slider_Main_audio.value;
        PlayerPrefs.SetFloat("Listener", Audio_Listener.Listener);
        PlayerPrefs.SetFloat("Slider_Main_audio", Slider_Main_audio.value);
    }
 }
