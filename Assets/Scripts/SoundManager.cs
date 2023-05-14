using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource bossSFXSource;
    [SerializeField] private AudioSource UISFXSource;
    [SerializeField] private AudioSource playerSFXSource;

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
    public AudioClip bossEnteringRevolvingAttackSFX;
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
        UISFXSource.clip = buttonSelectUISFX;
        UISFXSource.Play();
    }
   public void PlayJumpSound()
    {
        playerSFXSource.clip = playerJumpSFX;
        playerSFXSource.PlayOneShot(playerJumpSFX);
    }

    public void PlayDashSound()
    {
        playerSFXSource.clip = playerJumpSFX;
        playerSFXSource.PlayOneShot(playerDashSFX);
    }
    public void PlayPlayerHurtSound()
    {
        playerSFXSource.clip = playerHurtSFX;
        playerSFXSource.PlayOneShot(playerHurtSFX);
    }
    public void PlaySuccesfulShotSound()
    {
        bossSFXSource.clip = succesfulShotSFX;
        bossSFXSource.PlayOneShot(succesfulShotSFX);
    }

    public void PlayBossHoopDamagedSound()
    {
        bossSFXSource.clip = bossHoopDamagedSFX;
        bossSFXSource.PlayOneShot(bossHoopDamagedSFX);
    }

    public void PlayBossHoopDestroyedSound()
    {
        bossSFXSource.clip = bossHoopDestroyedSFX;
        bossSFXSource.PlayOneShot(bossHoopDestroyedSFX);
    }

    public void PlayHoopDestroyedSound()
    {
        bossSFXSource.clip = hoopDestroyedSFX;
        bossSFXSource.PlayOneShot(hoopDestroyedSFX);
    }

    public void PlaySpiralAttackIntroSound()
    {
        bossSFXSource.clip = bossEnteringSpiralAttackSFX;
        bossSFXSource.PlayOneShot(bossEnteringSpiralAttackSFX);
    }

    public void PlayHomingAttackIntroSound()
    {
        bossSFXSource.clip = bossEnteringHomingAttackSFX;
        bossSFXSource.PlayOneShot(bossEnteringHomingAttackSFX);
    }
    public void PlayDashingAttackIntroSound()
    {
        bossSFXSource.clip = bossEnteringDashingAttackSFX;
        bossSFXSource.PlayOneShot(bossEnteringDashingAttackSFX);
    }

    public void PlayRevolvingAttackIntroSound()
    {
        bossSFXSource.clip = bossEnteringRevolvingAttackSFX;
        bossSFXSource.PlayOneShot(bossEnteringRevolvingAttackSFX);
    }

    public void PlayBossGenericRoarSound()
    {
        bossSFXSource.clip = bossGenericRoarSFX;
        bossSFXSource.PlayOneShot(bossGenericRoarSFX);
    }

    public void PlayBossPhaseTwoIntroSound()
    {
        bossSFXSource.clip = bossEnteringPhaseTwoSFX;
        bossSFXSource.PlayOneShot(bossEnteringPhaseTwoSFX);
    }
    public void PlayBossDeathSound()
    {
        bossSFXSource.clip = bossDeathSFX;
        bossSFXSource.PlayOneShot(bossDeathSFX);
    }

}
