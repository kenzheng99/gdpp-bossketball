using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BallBehavior : MonoBehaviour {
    private bool hitGround;
    private DateTime whenHitGround;
    [SerializeField] private int destroyTime = 2;

    private Material ballMaterial;
    
    private void Start() {
        ballMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        // once a ball hits ground, start fading it and destroy after destroyTime seconds
        if (hitGround) {
            // difference between current time and hit ground time
            var secondsOnGround = (DateTime.Now - whenHitGround).Seconds;
            // Destroy if on ground for amount of time
            if (secondsOnGround >= destroyTime)
            {
                Destroy(gameObject);
            }

            // decrease ball material alpha to fade it
            var ballColor = ballMaterial.color;
            ballColor.a -= Time.deltaTime * (1/(float)destroyTime);
            ballMaterial.color = ballColor;
        }

        // case to destroy balls that fall off the map
        if (gameObject.transform.position.y < -50) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Ground")) {
            hitGround = true;
            whenHitGround = DateTime.Now;
        }
    }
}
