using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopUpperDetection : MonoBehaviour {
    private BasketDetection basket;

    // Start is called before the first frame update
    void Start() {
        basket = transform.parent.gameObject.GetComponent<BasketDetection>();
        Debug.Log("upper initiate");
    }

    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Upper enter");
        if (col.gameObject.CompareTag("Ball")) {
            basket.FinalCollision(1);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            basket.FinalCollision(2);
        }
    }
}
