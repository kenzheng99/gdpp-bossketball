using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Unity.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BallBehavior : MonoBehaviour {
    private bool hitGround;
    private int destroyTime = 2;
    private Timer despawnTimer;
    [SerializeField] private ParticleSystem ballProjectileParticles;
    private Material[] ballMaterials;
    [SerializeField] private GameObject ballModel;


    private void Start() {
        ballMaterials = ballModel.GetComponent<MeshRenderer>().materials;
    }

    private void Update()
    {
        // once a ball hits ground, start fading it and destroy after destroyTime seconds
        if (hitGround) {
            if (despawnTimer.Done()) {
                Destroy(gameObject);
            }
            despawnTimer.Tick(Time.deltaTime);

            // decrease ball material alpha to fade it

            var ballColor0 = ballMaterials[0].color;
            var ballColor1 = ballMaterials[1].color;
            ballColor0.a -= Time.deltaTime * (1/(float)destroyTime);
            ballColor1.a -= Time.deltaTime * (1/(float)destroyTime);
            ballMaterials[0].color = ballColor0;
            ballMaterials[1].color = ballColor0;
        }

        // case to destroy balls that fall off the map
        if (gameObject.transform.position.y < -20) {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Ground")) {
            hitGround = true;
            despawnTimer = new Timer(destroyTime);
        }

        // when coming in contact with BossProjectile explodes into particles
        if (col.gameObject.CompareTag("Enemy"))
        {
            //destroy both the ball and boss projectile
            Destroy(gameObject);
            Destroy(col.gameObject);

            //instantiate particles
            ballProjectileParticles = ParticleSystem.Instantiate(ballProjectileParticles, gameObject.transform.position, Quaternion.identity);
        }
    }
}
