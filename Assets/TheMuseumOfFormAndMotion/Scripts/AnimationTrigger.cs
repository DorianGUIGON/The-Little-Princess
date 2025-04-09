using UnityEngine;
using TMPro;

public class AnimationTrigger : MonoBehaviour
{
    public Transform player;
    public float distanceThreshold = 5f;

    public Camera playerCamera;
    public Camera cinematicCamera;

    public TextMeshProUGUI animationText;
    public TextMeshProUGUI cursorText;

    private Animator animator;
    private PlayerController playerController;
    private bool isInCinematicView = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();

        if (playerCamera != null) playerCamera.enabled = true;
        if (cinematicCamera != null) cinematicCamera.enabled = false;

        if (cursorText != null) cursorText.enabled = true;
        if (animationText != null) animationText.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        bool isNear = distance < distanceThreshold;

        animator.SetBool("IsNear", isNear);

        // Affichage de l'instruction "Appuyez sur E pour activer/désactiver le mode cinématique"
        if (animationText != null)
        {
            animationText.enabled = isNear;
        }

        if (isNear && Input.GetKeyDown(KeyCode.E) && !isInCinematicView)
        {
            EnterCinematicView();
        }
        else if (isInCinematicView && Input.GetKeyDown(KeyCode.E))
        {
            ExitCinematicView();
        }
    }

    void EnterCinematicView()
    {
        if (playerCamera != null) playerCamera.enabled = false;
        if (cinematicCamera != null) cinematicCamera.enabled = true;

        if (playerController != null) playerController.controlsEnabled = false;

        if (cursorText != null) cursorText.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isInCinematicView = true;
    }

    void ExitCinematicView()
    {
        if (playerCamera != null) playerCamera.enabled = true;
        if (cinematicCamera != null) cinematicCamera.enabled = false;

        if (playerController != null) playerController.controlsEnabled = true;

        if (cursorText != null) cursorText.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isInCinematicView = false;
    }
}
