using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb;
    private bool touchingFloor;
    private bool canDoubleJump;

    [Header("Dashing")]
    [SerializeField] private float dashVelocity;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private int dashCharges = 1;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private TrailRenderer trailRenderer;
    
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update() {
        float inputX = Input.GetAxisRaw("Horizontal");
        Move(inputX);
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (touchingFloor) {
                Jump();
            } else if (canDoubleJump) {
                Jump();
                canDoubleJump = false;
            }
        }
        //dashing code
        var dashInput = Input.GetButtonDown("Dash");
        if (dashInput && canDash)
        {
            Dash();
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashVelocity; 
        }
        if (touchingFloor)
        {
            dashCharges = 1;
            canDash = true;
            // StartCoroutine(DashCooldown());
        }
    }

    private void Dash()
    {
        isDashing = true;
        canDash = false;
        trailRenderer.emitting = true;
        dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (dashingDir == Vector2.zero)
        {
            dashingDir = new Vector2(transform.localScale.x, 0);
        }
        StartCoroutine(StopDashing());
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        isDashing = false;
        dashCharges -= 1;
    }

    private void Move(float inputX) {
        rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
    }

    private void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            touchingFloor = true;
            canDoubleJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            touchingFloor = false;
        }
    }
}
