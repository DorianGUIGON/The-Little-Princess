using UnityEngine;
using TMPro;

public class JerricanePickup : MonoBehaviour
{
    [Header("Référence à la caméra")]
    public Transform cameraTransform;

    [Header("Configuration")]
    public float pickupDistanceThreshold = 5f;

    [Header("UI Prompt (optionnel)")]
    public TextMeshProUGUI pickupPromptText;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, cameraTransform.position);
        bool isNear = distance < pickupDistanceThreshold;

        if (pickupPromptText != null)
        {
            pickupPromptText.enabled = isNear;
            if (isNear)
            {
                pickupPromptText.text = "Appuyez sur P pour récupérer";
            }
        }

        if (isNear && Input.GetKeyDown(KeyCode.P))
        {
            if (JerricaneCollector.instance != null)
                JerricaneCollector.instance.AddJerricane();

            Destroy(gameObject);
        }
    }
}
