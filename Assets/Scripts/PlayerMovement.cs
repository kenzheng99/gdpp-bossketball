using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator anim;

    private Rigidbody2D rb;
    private bool touchingFloor;
    private bool canDoubleJump;

    [Header("Dashing")]
    [SerializeField] private float dashVelocity;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private PlayerHealthController _playerHealthController;
    [SerializeField] private AudioSource playerAudio;

    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private TrailRenderer trailRenderer;
    
    void Start() 
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update() {
        if (GameManager.Instance.CurrentState == GameState.PLAYER_DEAD) {
            return;
        }
        
        float inputX = Input.GetAxisRaw("Horizontal");
        Move(inputX);
        
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && touchingFloor && isDashing == false)
        {
            playerAudio.enabled = true;
        }
        else
        {
            playerAudio.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (touchingFloor) {
                Jump();
                // play jump sound
                SoundManager.Instance.PlayJumpSound();
            } else if (canDoubleJump) {
                Jump();
                SoundManager.Instance.PlayJumpSound();
                canDoubleJump = false;
            }
        }
        //dashing code
        var dashInput = Input.GetButtonDown("Dash");
        if (dashInput && canDash)
        {
            Physics2D.IgnoreLayerCollision(6, 8, true);
            Physics2D.IgnoreLayerCollision(6, 9, true);
            Dash();
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashVelocity; 
        }
        if (touchingFloor)
        {
            canDash = true;
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
        // play dash sound
        SoundManager.Instance.PlayDashSound();
        // face towards dashing direction
        float direction = dashingDir.x > 0 ? 1 : -1;
        Vector3 newScale = transform.localScale;
        newScale.x = direction;
        transform.localScale = newScale;
        
        anim.SetTrigger("dashTrigger");
        StartCoroutine(StopDashing());
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        isDashing = false;
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 9, false);
    }

    private void Move(float inputX) {
        if (inputX == 0) { // stopped
            anim.SetBool("isRunning", false);
        } else { // running
            if (touchingFloor) {
                anim.SetBool("isRunning", true);
                float direction = inputX > 0 ? 1 : -1;
                Vector3 newScale = transform.localScale;
                newScale.x = direction;
                transform.localScale = newScale;
            }
        }
        rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
    }

    private void Jump() {
        anim.SetTrigger("jumpTrigger");
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
