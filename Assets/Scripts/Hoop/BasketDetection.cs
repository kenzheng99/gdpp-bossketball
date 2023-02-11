using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketDetection : MonoBehaviour {
    // private GameObject hoopUpper;
    // private GameObject hoopLower;

    private bool inHoopArea;
    private int finalCollide = 0; // 0:none, 1:upper enter, 2:upper exit, 3:lower enter, 4:lower exit

    private void Start() {
        // hoopUpper = hoop.transform.GetChild(0).gameObject;
        // hoopLower = hoop.transform.GetChild(1).gameObject;
    }

    private void Update() {
        
        if (inHoopArea) {
            // do shit
        }
    }

    public void FinalCollision(int colType) {
        finalCollide = colType;
        Debug.Log(finalCollide);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ball")) {
            inHoopArea = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            inHoopArea = false;
        }
    }
}
