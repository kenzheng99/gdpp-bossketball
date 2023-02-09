using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb;
    private bool touchingFloor;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        float inputX = Input.GetAxisRaw("Horizontal");
        Move(inputX);
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
        
        Debug.Log(touchingFloor);
    }

    private void Move(float inputX) {
        rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
    }

    private void Jump() {
        if (touchingFloor) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            touchingFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            touchingFloor = false;
        }
    }
}
