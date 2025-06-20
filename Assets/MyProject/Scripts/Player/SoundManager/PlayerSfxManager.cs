using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfxManager : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        EventBus.GamePaused += audioSource.Pause;
        EventBus.GameResumed += audioSource.UnPause;
    }
    void OnDestroy()
    {
        EventBus.GamePaused -= audioSource.Pause;
        EventBus.GameResumed -= audioSource.UnPause;
    }

    public void PlaySound(string audioClipName, bool loop = false)
    {
        audioSource.Stop();
        if (loop)
        {
            AudioClip clip = SFXManager.Instance.audioClipDictionary[audioClipName];
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
            return;
        }
        audioSource.Stop();
        audioSource.PlayOneShot(SFXManager.Instance.audioClipDictionary[audioClipName]);
    }
    public void StopSound()
    {
        audioSource.Stop();
    }

}
