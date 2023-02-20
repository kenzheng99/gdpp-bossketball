using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public Boss _boss;
    [SerializeField] private ParticleSystem hoopDestroyedParticles;
    [SerializeField] private ParticleSystem succesfulShotParticles;
    [SerializeField] private int successfulShotDamage;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (GameManager.Instance.bossPhaseTwo)
        {
            if (col.gameObject.CompareTag("Ball"))
            {
                SoundManager.Instance.PlayHoopDestroyedSound();
                Destroy(col.gameObject);
                //boss takes damage
                _boss.BossTakeDamage(successfulShotDamage);
                if (_boss.currentHealth > 0)
                {
                    //play particle effect 
                    var particleEmission = succesfulShotParticles.emission;
                    var particleDuration = succesfulShotParticles.duration;
                    particleEmission.enabled = true;
                    succesfulShotParticles.Play();
                    Invoke(nameof(StopSuccesfulShotParticles), particleDuration - 1);
                    SoundManager.Instance.PlayBossHoopDamagedSound();
                }

                else
                {
                    //play hoopDestroyedParticles and destroy eye
                    var particleEmission = hoopDestroyedParticles.emission;
                    var particleDuration = hoopDestroyedParticles.duration;
                    particleEmission.enabled = true;
                    hoopDestroyedParticles.Play();
                    var collider = GetComponent<CircleCollider2D>();
                    collider.enabled = false;
                    SoundManager.Instance.PlayBossHoopDestroyedSound();
                    // turn off collider to prevent player shooting into it again (particle effect needs time to run)
                    //GetComponent<CircleCollider2D>().enabled = false;
                    Invoke(nameof(DestroyEye), particleDuration - 1);
                }
            }
        }
    }

    void StopSuccesfulShotParticles()
    {
        succesfulShotParticles.Stop();
    }
    void DestroyEye()
    {
        Destroy(gameObject);
    }
}
