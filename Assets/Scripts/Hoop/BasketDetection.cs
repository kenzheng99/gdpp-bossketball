using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketDetection : MonoBehaviour {

    public Boss _boss;
    private float enterY;
    [SerializeField] private int successfulShotDamage;
    [SerializeField] private int hoopHealth = 5;
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
                _boss.BossTakeDamage(successfulShotDamage);
                if (hoopHealth > 0)
                {
                    //boss takes normal 1 damage

                    //play particle effect and destroy ball
                    var particleEmission = succesfulShotParticles.emission;
                    var particleDuration = succesfulShotParticles.duration;
                    particleEmission.enabled = true;
                    succesfulShotParticles.Play();
                    Invoke(nameof(StopSuccesfulShotParticles), particleDuration - 1);
                    
                }
                // hoop is dead
                else
                {
                    //play hoopDestroyedParticles and destroy hoop
                    var particleEmission = hoopDestroyedParticles.emission;
                    var particleDuration = hoopDestroyedParticles.duration;
                    particleEmission.enabled = true;
                    hoopDestroyedParticles.Play();
                    // turn off collider to prevent player shooting into it again (particle effect needs time to run)
                    GetComponent<BoxCollider2D>().enabled = false;
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
