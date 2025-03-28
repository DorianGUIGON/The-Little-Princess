using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalRotation = 0f;
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;

    private Transform player;
    private Transform playerCamera;
    private Rigidbody rigidBody;


    void Start()
    {
        player = this.transform;
        playerCamera = Camera.main.transform;
        rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        /* Keyboard */

        float inputX = Input.GetAxis("Horizontal"); // Left, Right
        float inputY = Input.GetAxis("Vertical"); // Up, Down

        Vector3 move = this.transform.right * inputX + this.transform.forward * inputY;
        this.transform.position += move * this.speed * Time.deltaTime; // Front and side movement

        /* Jump */

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        /* Mouse */

        float mouseX = Input.GetAxis("Mouse X"); // Horizontal
        float mouseY = Input.GetAxis("Mouse Y"); // Vertical

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -75f, 75f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); // Vertical rotation (only camera)

        this.transform.Rotate(Vector3.up * mouseX); // Horizontal rotation (all)
    }
}
