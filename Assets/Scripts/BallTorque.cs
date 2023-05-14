using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTorque : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddTorque(rb.velocity.magnitude/10, ForceMode2D.Force);
    }
}
