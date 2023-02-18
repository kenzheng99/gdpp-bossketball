using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] private float minLaunchPower;
    [SerializeField] private float maxLaunchPower;
    [SerializeField] private float powerScale;
    [SerializeField] private int lineLength;
    [SerializeField] private float cooldownTime;
    [SerializeField] private Animator anim;

    private Vector2 launchVelocity;
    private LineRenderer lineRenderer;
    private bool mouseHeldDown;
    private Timer cooldownTimer;
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        cooldownTimer = new Timer(0);
    }

    void Update() {
        if (GameManager.Instance.CurrentState == GameState.PLAYER_DEAD) {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {
            mouseHeldDown = true;
        }

        if (mouseHeldDown && cooldownTimer.Done()) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            launchVelocity = (Vector2)(mouseWorldPosition - transform.position) * powerScale;
            if (launchVelocity.magnitude > maxLaunchPower) {
                launchVelocity = launchVelocity.normalized * maxLaunchPower;
            } else if (launchVelocity.magnitude < minLaunchPower) {
                launchVelocity = launchVelocity.normalized * minLaunchPower;
            }
            RenderLine(launchVelocity);
        }
        
        if (Input.GetMouseButtonUp(0)) {
            mouseHeldDown = false;
            if (cooldownTimer.Done()) {
                Shoot();
                lineRenderer.positionCount = 0;
                cooldownTimer = new Timer(cooldownTime);
            }
        }
        
        cooldownTimer.Tick(Time.deltaTime);
    }

    void Shoot() {
        anim.SetTrigger("throwTrigger");
        // face towards shooting direction
        float direction = launchVelocity.x > 0 ? 1 : -1;
        Vector3 newScale = transform.localScale;
        newScale.x = direction;
        transform.localScale = newScale;
        // instantiate ball
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchVelocity.x, launchVelocity.y), ForceMode2D.Impulse);
    }

    private void RenderLine(Vector2 velocity) {
        int adjustedLineLength = (int)(lineLength * velocity.magnitude);
        lineRenderer.positionCount = adjustedLineLength;
        float gravityScale = ballPrefab.GetComponent<Rigidbody2D>().gravityScale;
        Vector2[] trajectory = Plot(transform.position, velocity, gravityScale, adjustedLineLength);
        Vector3[] positions = new Vector3[adjustedLineLength];
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
