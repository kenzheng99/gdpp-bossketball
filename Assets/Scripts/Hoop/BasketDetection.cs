using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketDetection : MonoBehaviour {
    private GameObject hoop;
    private GameObject hoopUpper;
    private GameObject hoopLower;

    private bool inHoopArea;

    private void Start() {
        hoop = gameObject;
        hoopUpper = hoop.transform.GetChild(0).gameObject;
        hoopLower = hoop.transform.GetChild(1).gameObject;
        Debug.Log(hoop);
        Debug.Log(hoopUpper);
        Debug.Log(hoopLower);
    }

    private void Update() {
        if (inHoopArea) {
            // do shit
        }
        
    }

    // public void enterUpper() {
    //     
    // }

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
