using UnityEngine;

public class BossHealthBar : MonoBehaviour {

    [SerializeField] private RectTransform healthBarFill;
    [SerializeField] private RectTransform healthBarEndcap;
    [SerializeField] private float minPosition;
    [SerializeField] private float maxPosition;
    private int maxHealth;

    public void SetMaxHealth(int maxHealth) {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(int health) {
        if (health <= 0) {
            healthBarFill.gameObject.SetActive(false);
            healthBarEndcap.gameObject.SetActive(false);
        } else {
            healthBarFill.gameObject.SetActive(true);
            healthBarEndcap.gameObject.SetActive(true);
            float healthRatio = (float) health / maxHealth;
            float newXPosition = minPosition + (maxPosition - minPosition) * healthRatio;
            healthBarFill.localPosition = new Vector2(newXPosition, healthBarFill.localPosition.y);
            
        }
    }
}
