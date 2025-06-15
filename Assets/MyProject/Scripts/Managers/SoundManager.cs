using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    [Header("Audio Clip Dictionary")]
    [SerializedDictionary("Audio Clip Name", "Audio Clip")]
    [SerializeField] private SerializedDictionary<string, AudioClip> audioClipDictionary;
    [SerializeField] private AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }
        Destroy(gameObject);
    }


    public void PlaySound(string audioClipName)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClipDictionary[audioClipName]);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

}
