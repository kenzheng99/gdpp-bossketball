using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopUpperDetection : MonoBehaviour {
    private GameObject hoopUpper;
    // Start is called before the first frame update
    void Start() {
        hoopUpper = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ball")) {
            
        }
    }
}
