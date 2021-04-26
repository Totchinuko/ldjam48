using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourcePlayer : MonoBehaviour
{
    #region Show in inspector

    [SerializeField] private AudioPlayerSO _audioPlayer;

    #endregion


    #region Init

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion


    #region Enable/Disable

    private void OnEnable()
    {
        _audioPlayer.AudioSource = _audioSource;
    }

    private void OnDisable()
    {
        _audioPlayer.AudioSource = null;
    }

    #endregion


    #region Private

    private AudioSource _audioSource;

    #endregion
}
