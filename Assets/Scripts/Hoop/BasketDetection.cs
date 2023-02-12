using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketDetection : MonoBehaviour {

    private bool inHoopArea;
    private bool firstCollision;
    private bool enterTop;
    private int finalCollide = 0; // 0:none, 1:upper enter, 2:upper exit, 3:lower enter, 4:lower exit

    public void FinalCollision(int colType) {
        if (inHoopArea) {
            finalCollide = colType;
            if (!enterTop && firstCollision && finalCollide == 1) {
                enterTop = true;
            }
            firstCollision = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ball")) {
            firstCollision = true;
            inHoopArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            inHoopArea = false;
            if (enterTop && finalCollide == 4) {
                Debug.Log("Score");
                // damage Boss 
            }
            enterTop = false;
        }
    }


}
