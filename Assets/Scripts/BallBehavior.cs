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
    private int destroyTime = 3;
    private Timer despawnTimer;

    private Material ballMaterial;
    
    private void Start() {
        ballMaterial = GetComponent<MeshRenderer>().material;
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
            var ballColor = ballMaterial.color;
            ballColor.a -= Time.deltaTime * (1/(float)destroyTime);
            ballMaterial.color = ballColor;
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
    }
}
