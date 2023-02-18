using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketDetection : MonoBehaviour {

    public Boss _boss;
    private float enterY;
    public ParticleSystem succesfulShotParticles;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ball")) {
            enterY = col.gameObject.transform.position.y;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            if (other.gameObject.transform.position.y < enterY) {
                Debug.Log("Score");
                
                //boss takes damage
                _boss.BossTakeDamage(5);
                //instantiate particle effect and destroy ball
                var particleEmission = succesfulShotParticles.emission;
                var particleDuration = succesfulShotParticles.duration;
                particleEmission.enabled = true;
                succesfulShotParticles.Play();
                Destroy(other.gameObject);
                Invoke(nameof(StopParticle), particleDuration-1);
            }
        }
    }
    void StopParticle()
    {
        var particleEmission = succesfulShotParticles.emission;
        particleEmission.enabled = false;
        succesfulShotParticles.Stop();
    }
}
