using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class Projectile : MonoBehaviour {

    private Vector3 targetPos;
    private float levelWidth = 50; // set from game manager
    private float levelHeight = 50; // set from game manager
    private float projectileSpeed;

    private void Update() {
        var projectileTransform = gameObject.transform;
        projectileTransform.Translate(Time.deltaTime*projectileSpeed*targetPos.normalized);
        if (Math.Abs(projectileTransform.position.x) > levelWidth ||
            Math.Abs(projectileTransform.position.y) > levelHeight) {
            Destroy(gameObject);
        }
    }

    public void SetTrajectory(Vector3 target, float speed) {
        targetPos = target;
        projectileSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            // TODO: do damage
            Debug.Log("Hit player");
            Destroy(gameObject);
        }
    }
}
