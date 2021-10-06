using System;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    public static IEnumerator FadeIn(AudioSource audioSource, float targetVolume, float duration)
    {
        float time = 0;
        float startValue = 0;
        float endValue = targetVolume;

        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = endValue;
    }

    public static void StartFadeIn(AudioSource audioSource, float targetVolume, float duration)
    {
        EditorCoroutineUtility.StartCoroutineOwnerless(FadeIn(audioSource, targetVolume, duration));
    }
    
    public static IEnumerator FadeOut(AudioSource audioSource, float targetVolume, float duration)
    {
        float time = 0;
        float startValue = targetVolume;
        float endValue = 0;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = endValue;
    }
    public static void StartFadeOut(AudioSource audioSource, float targetVolume, float duration)
    {
        EditorCoroutineUtility.StartCoroutineOwnerless(FadeOut(audioSource, targetVolume, duration));
    }
    
    
}
