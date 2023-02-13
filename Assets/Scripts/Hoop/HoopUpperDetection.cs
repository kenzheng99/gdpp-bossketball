using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopUpperDetection : MonoBehaviour {
    private BasketDetection basket;

    // Start is called before the first frame update
    private void Start() {
        basket = transform.parent.GetComponent<BasketDetection>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
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
