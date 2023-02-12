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
        if (hitGround) {
            var secondsOnGround = (DateTime.Now - whenHitGround).Seconds;
            if (secondsOnGround >= destroyTime)
            {
                Destroy(gameObject);
            }

            Color ballColor = ballMaterial.color;
            ballColor.a -= Time.deltaTime * (1/(float)destroyTime);
            ballMaterial.color = ballColor;

            // ballColor.a -= Time.deltaTime * (float)0.1;
            // GetComponent<MeshRenderer>().material.color = ballColor ;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Ground")) {
            hitGround = true;
            whenHitGround = DateTime.Now;
        }
    }
}
