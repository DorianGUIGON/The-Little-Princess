using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Vector2 input;

    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float steerSpeed;

    private bool isSlippery = false;
    public float slipperyMultiplier = 1.5f;
    public float slipperySteerReduction = 0.5f;

    public void SetInputs(Vector2 input)
    {
        this.input = input;
    }

    void FixedUpdate()
    {
        float speedMultiplier = isSlippery ? 1.5f : 1f;
        float steerMultiplier = isSlippery ? 1.5f : 1f;

        // Accélération
        float speed = (input.y > 0 ? forwardMoveSpeed : backwardMoveSpeed) * speedMultiplier;
        if (input.y == 0) speed = 0;
        rg.AddForce(transform.forward * speed, ForceMode.Acceleration);

        float rotation = input.x * steerSpeed * steerMultiplier * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slippery"))
        {
            Debug.Log("🚗💨 Ça glisse et accélère !");
            isSlippery = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slippery"))
        {
            Debug.Log("Retour à la normale");
            isSlippery = false;
        }
    }
}
