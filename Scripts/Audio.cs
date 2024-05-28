using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    [SerializeField] AudioClip background;
    [SerializeField] public AudioClip ballThrow;
    [SerializeField] public AudioClip click;
    [SerializeField] public AudioClip levelUp;

   public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

  
   public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
