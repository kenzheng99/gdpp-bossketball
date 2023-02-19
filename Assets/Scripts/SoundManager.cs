using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambienceSource;
    public AudioClip buttonSelectUISFX;
    public AudioClip playerJumpSFX;
    public AudioClip playerDashSFX;
    public AudioClip playerHurtSFX;
    public AudioClip playerDeathSFX;
    public AudioClip bossEnteringSpiralAttackSFX;
    public AudioClip bossEnteringDashingAttackSFX;
    public AudioClip bossHoopDestroyedSFX;
    public AudioClip bossEnteringPhaseTwoSFX;
    public AudioClip bossDeathSFX;
    public AudioClip succesfulShotSFX;  


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
        sfxSource.clip = playerJumpSFX;
        sfxSource.PlayOneShot(playerJumpSFX);
    }

    public void PlayDashSound()
    {
        sfxSource.clip = playerJumpSFX;
        sfxSource.PlayOneShot(playerDashSFX);
    }
    public void PlayPlayerHurtSound()
    {
        sfxSource.clip = playerHurtSFX;
        sfxSource.PlayOneShot(playerHurtSFX);
    }
    public void PlaySuccesfulShotSound()
    {
        sfxSource.clip = succesfulShotSFX;
        sfxSource.PlayOneShot(succesfulShotSFX);
    }

    public void PlaySpiralAttackIntroSound()
    {
        sfxSource.clip = bossEnteringSpiralAttackSFX;
        sfxSource.PlayOneShot(bossEnteringSpiralAttackSFX);
    }

    public void PlayDashingAttackIntroSound()
    {
        sfxSource.clip = bossEnteringDashingAttackSFX;
        sfxSource.PlayOneShot(bossEnteringDashingAttackSFX);
    }

}
