using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambienceSource;

    //ui
    public AudioClip buttonSelectUISFX;

    //player
    public AudioClip playerJumpSFX;
    public AudioClip playerDashSFX;
    public AudioClip playerHurtSFX;

    //boss
    public AudioClip bossEnteringSpiralAttackSFX;
    public AudioClip bossEnteringDashingAttackSFX;
    public AudioClip bossEnteringHomingAttackSFX;
    public AudioClip bossHoopDamagedSFX;
    public AudioClip bossHoopDestroyedSFX;
    public AudioClip bossEnteringPhaseTwoSFX;
    public AudioClip bossDeathSFX;
    public AudioClip bossGenericRoarSFX;
    
    //particles
    public AudioClip succesfulShotSFX;
    public AudioClip hoopDestroyedSFX;


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

    public void PlayBossHoopDamagedSound()
    {
        sfxSource.clip = bossHoopDamagedSFX;
        sfxSource.PlayOneShot(bossHoopDamagedSFX);
    }

    public void PlayBossHoopDestroyedSound()
    {
        sfxSource.clip = bossHoopDestroyedSFX;
        sfxSource.PlayOneShot(bossHoopDestroyedSFX);
    }

    public void PlayHoopDestroyedSound()
    {
        sfxSource.clip = hoopDestroyedSFX;
        sfxSource.PlayOneShot(hoopDestroyedSFX);
    }

    public void PlaySpiralAttackIntroSound()
    {
        sfxSource.clip = bossEnteringSpiralAttackSFX;
        sfxSource.PlayOneShot(bossEnteringSpiralAttackSFX);
    }

    public void PlayHomingAttackIntroSound()
    {
        sfxSource.clip = bossEnteringHomingAttackSFX;
        sfxSource.PlayOneShot(bossEnteringHomingAttackSFX);
    }
    public void PlayDashingAttackIntroSound()
    {
        sfxSource.clip = bossEnteringDashingAttackSFX;
        sfxSource.PlayOneShot(bossEnteringDashingAttackSFX);
    }

    public void PlayBossGenericRoarSound()
    {
        sfxSource.clip = bossGenericRoarSFX;
        sfxSource.PlayOneShot(bossGenericRoarSFX);
    }

    public void PlayBossPhaseTwoIntroSound()
    {
        sfxSource.clip = bossEnteringPhaseTwoSFX;
        sfxSource.PlayOneShot(bossEnteringPhaseTwoSFX);
    }
    public void PlayBossDeathSound()
    {
        sfxSource.clip = bossDeathSFX;
        sfxSource.PlayOneShot(bossDeathSFX);
    }

}
