using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    void Start()
    {
        // Cargar volúmenes guardados
        float savedMusicVol = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        float savedSfxVol = PlayerPrefs.GetFloat("sfxVolume", 0.5f);

        musicSlider.value = savedMusicVol;
        sfxSlider.value = savedSfxVol;

        musicSource.volume = savedMusicVol;
        SetSFXVolume(savedSfxVol);

        // Añadir listeners
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
    }

    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void ChangeSFXVolume(float value)
    {
        SetSFXVolume(value);
        PlayerPrefs.SetFloat("sfxVolume", value);
    }

    private void SetSFXVolume(float value)
    {
        foreach (AudioSource sfx in sfxSources)
        {
            sfx.volume = value;
        }
    }
}
