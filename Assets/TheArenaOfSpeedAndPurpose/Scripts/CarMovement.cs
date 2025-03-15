using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Vector2 input;

    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float steerSpeed;

    public void SetInputs(Vector2 input)
    {
        this.input = input;
    }

    void FixedUpdate() // Apply physics here
    {
        // Accelerate
        float speed = input.y > 0 ? forwardMoveSpeed : backwardMoveSpeed;
        if (input.y == 0) speed = 0;
        rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);
        // Steer
        float rotation = input.x * steerSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0, Space.World);
    }

    public float GetSpeed()
    {
        return rg.velocity.magnitude;
    }
}
