using System;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector3 targetPos;
    private float levelWidth = 200; // set from game manager
    private float levelHeight = 200; // set from game manager
    private float projectileSpeed;

    private Timer trajectoryTimer;

    private void Start() {
        trajectoryTimer = new Timer(0.1f);
    }

    private void Update() {
        Transform projectileTransform = gameObject.transform;
        projectileTransform.Translate(Time.deltaTime*projectileSpeed*targetPos.normalized);
        if (Math.Abs(projectileTransform.position.x) > levelWidth ||
            Math.Abs(projectileTransform.position.y) > levelHeight) {
            Destroy(gameObject);
        }
        trajectoryTimer.Tick(Time.deltaTime);
    }

    public void SetTrajectory(Vector3 target, float speed) {
        targetPos = target;
        projectileSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            // TODO: do damage
            Destroy(gameObject);
        }
    }
}
