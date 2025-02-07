using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceUI;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderSound;

    private float _soundVolume;
    private float _musicVolume;

    private void Start()
    {
        LoadVolume();
    }

    public void UISOund()
    {
        audioSourceUI.Play();
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log(sliderMusic.value) * 20);
        _musicVolume = sliderMusic.value;
    }

    public void SetSoundVolume()
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log(sliderSound.value) * 20);
        _soundVolume = sliderSound.value;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", _soundVolume);
    }

    private void LoadVolume()
    {
        _musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        _soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

        sliderMusic.value = _musicVolume;
        sliderSound.value = _soundVolume;

        SetMusicVolume();
        SetSoundVolume();
    }
}
