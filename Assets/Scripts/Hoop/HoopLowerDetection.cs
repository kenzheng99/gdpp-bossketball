using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopLowerDetection : MonoBehaviour
{
    private BasketDetection basket;

    // Start is called before the first frame update
    void Start() {
        basket = transform.parent.gameObject.GetComponent<BasketDetection>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ball")) {
            basket.FinalCollision(3);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            basket.FinalCollision(4);
        }
    }
}
