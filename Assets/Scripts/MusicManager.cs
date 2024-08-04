using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MUSIC_VOLUME";

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume = .25f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .25f);
        audioSource.volume = volume;

    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1) volume = 0;

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float Volume => volume;

}
