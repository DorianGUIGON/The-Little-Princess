using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public bool controlsEnabled = true;

    private float verticalRotation = 0f;
    public float mouseSensitivity = 2f;
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;

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
        if (!controlsEnabled) return;

        /* Movements */
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 move = transform.right * inputX + transform.forward * inputY;
        transform.position += move * speed * Time.deltaTime;

        /* Jump */
        if (!isJumping && Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }

        /* Vision */
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -75f, 75f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);

        /* Menu */
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f) isJumping = false;
    }
}