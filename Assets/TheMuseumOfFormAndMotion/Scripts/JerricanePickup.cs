using UnityEngine;
using TMPro; // Utilisé si vous préférez TextMeshPro pour le prompt. Sinon, vous pouvez utiliser UnityEngine.UI

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
        // Calculer la distance entre ce jerricane et la caméra
        float distance = Vector3.Distance(transform.position, cameraTransform.position);
        bool isNear = distance < pickupDistanceThreshold;

        // Afficher ou masquer le prompt en fonction de la proximité
        if (pickupPromptText != null)
        {
            pickupPromptText.enabled = isNear;
            if (isNear)
            {
                pickupPromptText.text = "Appuyez sur P pour récupérer";
            }
        }

        // Si la caméra est proche et que le joueur appuie sur P, récupérer le jerricane
        if (isNear && Input.GetKeyDown(KeyCode.P))
        {
            if (JerricaneCollector.instance != null)
                JerricaneCollector.instance.AddJerricane();

            // Détruire le jerricane pour qu'il ne soit collecté qu'une seule fois
            Destroy(gameObject);
        }
    }
}
