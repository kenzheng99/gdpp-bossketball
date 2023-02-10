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
        Debug.Log("oh no");
    }

    private void Update() {
        // if (if)
        // check if entered hoop area
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        // if (col )
        inHoopArea = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        inHoopArea = false;
    }
}
