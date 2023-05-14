using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    void Start() {
        FollowX();
    }

    // Update is called once per frame
    void Update() {
        FollowX();
    }

    // match target object's location in the x axis
    void FollowX() {
        float newX = objectToFollow.transform.position.x;
        if (newX < minX) {
            newX = minX;
        }

        if (newX > maxX) {
            newX = maxX;
        }
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

    }
}
