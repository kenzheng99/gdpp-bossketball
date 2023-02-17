using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketDetection : MonoBehaviour {

    public Boss _boss;
    private float enterY;
    [SerializeField] private ParticleSystem succesfulShotParticles;

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
                succesfulShotParticles = ParticleSystem.Instantiate(succesfulShotParticles, this.gameObject.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
        }
    }
}
