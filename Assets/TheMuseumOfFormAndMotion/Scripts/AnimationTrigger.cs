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
    private bool wasNear = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();

        playerCamera.enabled = true;
        cinematicCamera.enabled = false;

        cursorText.enabled = true;
        animationText.enabled = false;
    }

    void Update()
    {
        bool isNear = Vector3.Distance(transform.position, player.position) < distanceThreshold;

        animator.SetBool("IsNear", isNear);

        if (!wasNear && isNear) animationText.enabled = true;
        else if (wasNear && !isNear) animationText.enabled = false;

        if (isNear && Input.GetKeyDown(KeyCode.E) && !isInCinematicView) EnterCinematicView();
        else if (isInCinematicView && Input.GetKeyDown(KeyCode.E)) ExitCinematicView();

        wasNear = isNear;
    }

    void EnterCinematicView()
    {
        playerController.controlsEnabled = false;

        playerCamera.enabled = false;
        cinematicCamera.enabled = true;
        cursorText.enabled = false;

        isInCinematicView = true;
    }

    void ExitCinematicView()
    {
        playerController.controlsEnabled = true;

        playerCamera.enabled = true;
        cinematicCamera.enabled = false;
        cursorText.enabled = true;

        isInCinematicView = false;
    }
}
