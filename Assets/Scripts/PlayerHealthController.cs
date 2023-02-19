using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public int maxHealth;


    [SerializeField] private float invulnerableDurationSeconds;
    [SerializeField] private float invulnerabilityDeltaTime;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private AudioSource playerAudio;

    private int health;
    private bool isInvulnerable = false;
    private GameManager gameManager;
    private Vector3 startPosition;
    private Animator anim;

    void Start() {
        health = maxHealth;
        gameManager = GameManager.Instance;
        startPosition = transform.position;
        anim = playerModel.GetComponent<Animator>();
    }
    
    private void PlayerTakeDamage(int damage) {
        if (isInvulnerable) {
            return;
        }
        health -= damage;
        gameManager.UpdatePlayerHealth(health);
        SoundManager.Instance.PlayPlayerHurtSound();
        if (health <= 0) {
            anim.SetTrigger("deathTrigger");
            playerAudio.enabled = false;
        } else {
            StartCoroutine(BecomeTemporarilyInvulnerable());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy")) {
            PlayerTakeDamage(1);
            anim.SetTrigger("getHitTrigger");
        }
    }

    public void ResetPlayer() {
        health = maxHealth;
        gameManager.UpdatePlayerHealth(health);
        transform.position = startPosition;
    }

    private IEnumerator BecomeTemporarilyInvulnerable()
    {
        isInvulnerable = true;
        // when invulnerable can go through boss w/o getting hurt
        Physics2D.IgnoreLayerCollision(6, 8, true);
           
        for (float i = 0; i < invulnerableDurationSeconds; i += invulnerabilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (playerModel.transform.localScale == Vector3.one) {
                ScaleModelTo(Vector3.zero);
            } else {
                ScaleModelTo(Vector3.one);
            }
            yield return new WaitForSeconds(invulnerabilityDeltaTime);
        }
        isInvulnerable = false;
        Physics2D.IgnoreLayerCollision(6, 8, false);
        ScaleModelTo(Vector3.one);
    }

    private void ScaleModelTo(Vector3 scale) {
        playerModel.transform.localScale = scale;
    }
}
