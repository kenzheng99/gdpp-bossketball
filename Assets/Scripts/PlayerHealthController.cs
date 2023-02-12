using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public int health;
    public int totalHearts;
    public bool isInvulnerable = false;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private float invulnerableDurationSeconds;
    [SerializeField] private float invulnerabilityDeltaTime;
    [SerializeField] private GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        if (health > totalHearts)
        {
            health = totalHearts;
        }
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < totalHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        Debug.Log("player takin damage");
        if (!isInvulnerable)
        {
            health -= damage;
            StartCoroutine(BecomeTemporarilyInvulnerable());
        }
    }
    // collide with any projectiles or w the enemy itself
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("in contact w enemy object");
            PlayerTakeDamage(1);
        }
    }

    private IEnumerator BecomeTemporarilyInvulnerable()
    {
        Debug.Log("Player turned invincible!");
        isInvulnerable = true;

        for (float i = 0; i < invulnerableDurationSeconds; i += invulnerabilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (playerModel.transform.localScale == Vector3.one)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }
            yield return new WaitForSeconds(invulnerabilityDeltaTime);
        }
        Debug.Log("Player is no longer invincible!");
        isInvulnerable = false;
        ScaleModelTo(Vector3.one);
    }

    private void ScaleModelTo(Vector3 scale)
    {
        playerModel.transform.localScale = scale;
    }
}
