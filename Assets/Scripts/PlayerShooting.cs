using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] private float launchPower;

    private LineRenderer lineRenderer;
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update() {
            
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 launchVelocity = (mouseWorldPosition - transform.position).normalized * launchPower;
            RenderLine(launchVelocity);
            
            Shoot(launchVelocity);
        }
    }

    void Shoot(Vector2 launchVelocity) {
        
        
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchVelocity.x, launchVelocity.y), ForceMode2D.Impulse);
    }

    private void RenderLine(Vector2 velocity) {
        int numSegments = 1000;
        lineRenderer.positionCount = numSegments;
        Vector2[] trajectory = Plot(transform.position, velocity, 1, numSegments);
        Vector3[] positions = new Vector3[numSegments];
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = trajectory[i];
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector2[] Plot(Vector2 pos, Vector2 velocity, float gravityScale, int steps) {
        Vector2[] results = new Vector2[steps];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * gravityScale * timestep * timestep;

        Vector2 moveStep = velocity * timestep;
        for (int i = 0; i < steps; i++) {
            moveStep += gravityAccel;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

}
