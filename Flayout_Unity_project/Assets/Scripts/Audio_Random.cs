using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Audio_Random : MonoBehaviour
{
    [SerializeField] private AudioClip[] _tracks;
    [SerializeField] private Slider _sliderMusicVolume;
    [SerializeField] private Slider _sliderMainVolume;
    [SerializeField] private AudioSource _musicAudioSource;

    private void OnEnable()
    {
        int index = Random.Range(0, _tracks.Length);
        _musicAudioSource.clip = _tracks[index];
        YandexGame.GetDataEvent += GetData;
        _musicAudioSource.Play();
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            GetData();
    }

    private void OnDisable()
    {
        _musicAudioSource.Stop();
        YandexGame.GetDataEvent -= GetData;
    }

    public void OnChangedMusicVolume()
    {
        _musicAudioSource.volume =
            YandexGame.savesData.MusicVolume =
                _sliderMusicVolume.value;
    }

    public void OnChangedMainVolume()
    {
        AudioListener.volume =
            YandexGame.savesData.MainVolume =
                _sliderMainVolume.value;
    }

    private void GetData()
    {
        AudioListener.volume = 
            _sliderMainVolume.value = 
                YandexGame.savesData.MainVolume;

        _musicAudioSource.volume =
            _sliderMusicVolume.value =
                YandexGame.savesData.MusicVolume;
    }
 }
