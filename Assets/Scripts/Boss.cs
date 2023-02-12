using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public BossHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        //placeholder to test health bar
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            BossTakeDamage(10);
        }
    }

    //call this function with hoop detection when a shot is succesfully made
    public void BossTakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
