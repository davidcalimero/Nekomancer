using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip summon;
    public AudioSource audioSource;

    public static AudioManager instance;

    public void PlaySummon()
    {
        audioSource.clip = summon;
        audioSource.Play();
    }
}
