using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] AudioSource musicSource;
   [SerializeField] AudioSource SFXSource;
    [Header("--------------------------------")]
   public AudioClip background;
   public AudioClip Menu;
   public AudioClip Lose;
   public AudioClip grunt;
   public AudioClip attack;
   public AudioClip gunshot;

   private void Start()
   {
    musicSource.clip = background;
    musicSource.Play();
   }

   public void PlaySFX(AudioClip clip)
   {
    SFXSource.PlayOneShot(clip);
   }


}
