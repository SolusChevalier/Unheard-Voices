using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    #region FIELDS

    public static MusicManager Instance;
    public AudioClip mainMenuMusic;
    public AudioClip gameSceneMusic;
    public AudioClip Level1Music;
    private AudioSource audioSource;
    public AudioClip deathMusic;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
    }

    #endregion UNITY METHODS

    #region METHODS

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(mainMenuMusic);
                break;

            case "Game":
                PlayMusic(gameSceneMusic);
                break;

            case "Level 1":
                PlayMusic(gameSceneMusic);
                break;

            default:
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion METHODS
}