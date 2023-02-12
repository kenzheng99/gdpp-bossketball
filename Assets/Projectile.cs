using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class Projectile : MonoBehaviour {

    private Vector3 targetPos;

    private float projectileSpeed;
    // Start is called before the first frame update
    private void Start() {
        // targetPos = gameObject.transform.position;
        // projectileSpeed = 10;
    }

    // Update is called once per frame
    private void Update() {
        Vector3 direction = targetPos - gameObject.transform.position;
        // gameObject.transform.position = Vector3.MoveTowards(
        //     gameObject.transform.position,
        //     direction,
        //     projectileSpeed * Time.deltaTime);
        
        gameObject.transform.Translate(Time.deltaTime*projectileSpeed*targetPos.normalized);
        Debug.Log("dir"+ direction);
        Debug.Log("target pos"+ targetPos);
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
