using UnityEngine;

public class TruckController : MonoBehaviour
{
    public Rigidbody rb;
    public float accelerationForce = 1500f;
    public float reverseForce = 1000f;
    public float brakeForce = 3000f;

    private float input = 0f;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float speed = rb.velocity.magnitude * 3.6f; // km/h

        if (input > 0)
        {
            rb.AddForce(transform.right * input * accelerationForce);
        }
        else if (input < 0)
        {
            rb.AddForce(-transform.right * Mathf.Abs(input) * reverseForce);
        }
    }

    public void SetInput(float value)
    {
        input = value;
    }

    public void Stop()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeForce * Time.deltaTime);
    }

    public float GetSpeedKMH()
    {
        return rb.velocity.magnitude * 3.6f;
    }
}
