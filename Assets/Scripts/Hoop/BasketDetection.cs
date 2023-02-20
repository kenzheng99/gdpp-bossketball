using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketDetection : MonoBehaviour {

    public Boss _boss;
    private float enterY;
    [SerializeField] private int successfulShotDamage;
    [SerializeField] private int hoopDestroyedDamage;
    [SerializeField] private int hoopHealth = 3;
    [SerializeField] private ParticleSystem succesfulShotParticles;
    [SerializeField] private ParticleSystem hoopDestroyedParticles;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ball")) {
            enterY = col.gameObject.transform.position.y;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            if (other.gameObject.transform.position.y < enterY) {

                // hoop loses 1 health
                hoopHealth -= 1;
                //destroy ball
                Destroy(other.gameObject);
                // as long as hoop is not dead
                if (hoopHealth > 0)
                {
                    //boss takes normal 3 damage
                    _boss.BossTakeDamage(successfulShotDamage);
                    //play particle effect and destroy ball
                    var particleEmission = succesfulShotParticles.emission;
                    var particleDuration = succesfulShotParticles.duration;
                    particleEmission.enabled = true;
                    succesfulShotParticles.Play();
                    Invoke(nameof(StopSuccesfulShotParticles), particleDuration - 1);

                    if (_boss.hasEnteredPhaseTwo == false)
                    {
                        SoundManager.Instance.PlayBossHoopDamagedSound();
                    }
                    SoundManager.Instance.PlaySuccesfulShotSound();
                }
                // hoop is dead
                else
                {
                    _boss.BossTakeDamage(hoopDestroyedDamage);
                    //play hoopDestroyedParticles and destroy hoop
                    var particleEmission = hoopDestroyedParticles.emission;
                    var particleDuration = hoopDestroyedParticles.duration;
                    particleEmission.enabled = true;
                    hoopDestroyedParticles.Play();

                    if (_boss.hasEnteredPhaseTwo == false)
                    {
                        SoundManager.Instance.PlayBossHoopDestroyedSound();
                    }
                    SoundManager.Instance.PlayHoopDestroyedSound();
                    // turn off collider to prevent player shooting into it again (particle effect needs time to run)
                    GetComponent<BoxCollider2D>().enabled = false;
                    // make object seem like it disappears
                    gameObject.transform.localScale = Vector3.zero;
                    // then actually destroy the hoop after particle plays
                    Invoke(nameof(DestroyHoop), particleDuration);

                }
            }
        }
    }
    void StopSuccesfulShotParticles()
    {
        succesfulShotParticles.Stop();
    }

    void DestroyHoop()
    {
        Destroy(gameObject);
    }
}
