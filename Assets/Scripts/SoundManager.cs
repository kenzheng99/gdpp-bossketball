using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource AmbienceSource;
    public AudioClip battleMusic;
    public AudioClip playerShootSFX;
    public AudioClip playerFootstepSFX;
    public AudioClip playerJumpSFX;
    public AudioClip playerDamagedSFX;
    public AudioClip bossProjectileAttackSFX;
    public AudioClip bossDamagedSFX;
    public AudioClip succesfulShotSFX;
    public AudioClip swishSFX;
    public AudioClip dunkSFX;

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

   public void PlayShootingSound()
    {


    }

}
