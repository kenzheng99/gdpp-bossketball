using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambienceSource;
    public AudioClip battleMusic;
    public AudioClip playerFootstepSFX;
    public AudioClip playerJumpSFX;
    public AudioClip playerDamagedSFX;
    public AudioClip bossProjectileAttackSFX;
    public AudioClip bossHoopDestroyedSFX;
    public AudioClip succesfulShotSFX;
    public AudioClip buttonSelectUISFX;

    private void Start()
    {
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartMusic()
    {
        musicSource.Play();
    }

   public void PlayButtonSelectSound()
    {
        sfxSource.clip = buttonSelectUISFX;
        sfxSource.Play();
    }
   public void PlayJumpSound()
    {


    }

}
