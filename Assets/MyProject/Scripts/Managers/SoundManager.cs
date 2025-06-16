using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    private Coroutine fadeCoroutine;
    [Header("Audio Clip Dictionary")]
    [SerializedDictionary("Audio Clip Name", "Audio Clip")]
    [SerializeField] private SerializedDictionary<string, AudioClip> audioClipDictionary;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private float fadeDuration = 5f;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }


    public void PlaySound(string audioClipName, bool loop = false)
    {
        audioSource.Stop();
        if (loop)
        {
            AudioClip clip = audioClipDictionary[audioClipName];
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
            return;
        }
        audioSource.loop = false;
        audioSource.PlayOneShot(audioClipDictionary[audioClipName]);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
    public void PlaySoundWithFade(string audioClipName, bool loop = false, float volume = 1f)
    {
        if (!audioClipDictionary.TryGetValue(audioClipName, out var clip))
        {
            Debug.LogWarning($"Audio clip '{audioClipName}' not found!");
            return;
        }


        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (loop)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.volume = 0f;
            audioSource.Play();
            fadeCoroutine = StartCoroutine(FadeVolume(volume, fadeDuration));
        }
        else
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(clip);
        }
    }
    public void StopSoundWithFade(float duration = 0f)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        if (duration == 0)
        {
            duration = fadeDuration;
        }
        fadeCoroutine = StartCoroutine(FadeOutAndStop(duration));
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    IEnumerator FadeVolume(float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < duration)
        {

            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOutAndStop(float duration)
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }


}
