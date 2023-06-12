using UnityEngine;

public class Stadium_audio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _tracks;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        int index = Random.Range(0, _tracks.Length);
        _audioSource.clip = _tracks[index];
        _audioSource.Play();
    }

    private void OnDisable()
    {
        _audioSource.Stop();
    }
}
