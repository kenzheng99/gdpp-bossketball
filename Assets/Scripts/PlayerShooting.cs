using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] private float launchPower;
    void Start()
    {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
        }
    }

    void Shoot() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 20;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 launchVelocity = (mouseWorldPosition - transform.position).normalized * launchPower;
        
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchVelocity.x, launchVelocity.y), ForceMode2D.Impulse);
    }

}
