using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] private float minLaunchPower;
    [SerializeField] private float maxLaunchPower;
    [SerializeField] private float powerScaleRate;
    [SerializeField] private int lineLength;

    private float launchPower;
    private Vector3 launchVelocity;
    private LineRenderer lineRenderer;
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update() {
        // start charging shot
        if (Input.GetMouseButtonDown(0)) {
            launchPower = minLaunchPower;
        }
        
        // while charging shot
        if (launchPower > 0) {
            launchPower += powerScaleRate * Time.deltaTime;
            
            launchPower = Math.Min(launchPower, maxLaunchPower);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            launchVelocity = (mouseWorldPosition - transform.position).normalized * launchPower;
            RenderLine(launchVelocity);
        }
            
        // shoot ball
        if (Input.GetMouseButtonUp(0)) {
            Shoot();
            launchPower = 0;
            lineRenderer.positionCount = 0;
        }
    }

    void Shoot() {
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchVelocity.x, launchVelocity.y), ForceMode2D.Impulse);
    }

    private void RenderLine(Vector2 velocity) {
        lineRenderer.positionCount = lineLength;
        float gravityScale = ballPrefab.GetComponent<Rigidbody2D>().gravityScale;
        Vector2[] trajectory = Plot(transform.position, velocity, gravityScale, lineLength);
        Vector3[] positions = new Vector3[lineLength];
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
