using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio Player")]
public class AudioPlayerSO : ScriptableObject
{
    #region Show in inspector

    [SerializeField] private AudioMixerGroup _output;
    [SerializeField] private bool _loop;

    [Range(0, 1)]
    [SerializeField] private float _volume;

    [Range(-3, 3)]
    [SerializeField] private float _pitch;

    #endregion


    #region Public properties

    public AudioSource AudioSource { get; set; }

    #endregion


    #region Public methods

    public void SetVolume(float volume)
    {
        if (AudioSource.IsNull())
        {
            AudioSource = CreateNewAudioSource();
        }
        AudioSource.volume = volume;
    }

    public void SetPitch(float pitch)
    {
        if (AudioSource.IsNull())
        {
            AudioSource = CreateNewAudioSource();
        }
        AudioSource.pitch = pitch;
    }

    public void Play(AudioClip clip)
    {
        if (AudioSource == null)
        {
            AudioSource = CreateNewAudioSource();
        }

        AudioSource.volume = _volume;
        AudioSource.pitch = _pitch;
        if (_loop)
        {
            AudioSource.loop = _loop;
            AudioSource.clip = clip;
            AudioSource.Play();
        }
        else
        {
            AudioSource.PlayOneShot(clip);
        }
    }

    public void Stop()
    {
        if (AudioSource == null)
        {
            AudioSource = CreateNewAudioSource();
        }
        AudioSource.Stop();
    }

    #endregion


    private AudioSource CreateNewAudioSource()
    {
        GameObject go = new GameObject("AudioSource (created by AudioPlayer)");
        AudioSource source = go.AddComponent<AudioSource>();
        source.loop = false;
        source.playOnAwake = false;
        return source;
    }
}