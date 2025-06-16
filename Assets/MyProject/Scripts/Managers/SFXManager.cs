using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [Header("Audio Clip Dictionary")]
    [SerializedDictionary("Audio Clip Name", "Audio Clip")]
    [SerializeField] public SerializedDictionary<string, AudioClip> audioClipDictionary;
    //[SerializeField] public AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        Destroy(gameObject);
    }


    // public void PlaySound(string audioClipName)
    // {
    //     audioSource.Stop();
    //     audioSource.PlayOneShot(audioClipDictionary[audioClipName]);
    // }

    // public void StopSound()
    // {
    //     audioSource.Stop();
    // }
}
