using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipSample
{
    public string name;
    public AudioClip clip;

    public AudioClipSample(string name, AudioClip clip)
    {
        this.name = name;
        this.clip = clip;
    }

    public AudioClip GetClip()
    {
        return clip;
    }

    public string GetName()
    {
        return name;
    }
}

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    #region FIELDS

    public static SFXManager Instance;
    public List<AudioClipSample> Samples;
    public AudioClip[] AudioSamples;
    public AudioClip buttonClick;
    public AudioClip pause;
    public AudioClip Start;
    public AudioClip CanvasSceneChange;

    private AudioSource audioSource;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        foreach (AudioClip cl in AudioSamples)
        {
            Samples.Add(new AudioClipSample(cl.name, cl));
        }
    }

    #endregion UNITY METHODS

    #region METHODS

    public void PlaySFX(string clipName)
    {
        foreach (AudioClipSample sample in Samples)
        {
            if (sample.GetName() == clipName)
            {
                audioSource.PlayOneShot(sample.GetClip());
                return;
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    #endregion METHODS
}