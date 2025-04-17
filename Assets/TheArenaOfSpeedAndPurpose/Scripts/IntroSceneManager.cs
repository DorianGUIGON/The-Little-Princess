using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    [Header("Référence UI")]
    public Text introText; // Assignez-y le composant Text "IntroText" créé dans le Canvas.

    [Header("Configuration d'affichage")]
    public float displayDuration = 3f; // Durée d'affichage de chaque message (en secondes)
    public bool attendreInputPourContinuer = false; // Voulez-vous que le joueur appuie sur une touche pour passer au message suivant ?

    [Header("Messages d'introduction")]
    [TextArea(3, 10)]
    public string messageAtterrissage = "Mince, je n'ai plus de carburant. J'ai été obligé de me poser ici j'espère trouver un moyen de repartir";
    [TextArea(3, 10)]
    public string messageProspectus = "En regardant autour de moi, je remarque un prospectus au sol.";
    [TextArea(3, 10)]
    public string messageLecture = "Place disponible dans l'écurie Renault pour participer à la Spéciale de Montecarlo suite a la disparition de notre ancien Pilote.\n Prix pour la participation 100L de carburant";
    [TextArea(3, 10)]
    public string messageFin = "Génial ! J'ai trouver un moyen de repartir à l'aventure";


    void Start()
    {
        StartCoroutine(DisplayIntroSequence());
    }

    IEnumerator DisplayIntroSequence()
    {
        introText.text = messageAtterrissage;
        Debug.Log("Affichage messageAtterrissage");
        if (attendreInputPourContinuer)
        {
            yield return StartCoroutine(WaitForInput());
        }
        else
        {
            yield return new WaitForSeconds(displayDuration);
        }

        introText.text = messageProspectus;
        Debug.Log("Affichage messageProspectus");
        if (attendreInputPourContinuer)
        {
            yield return StartCoroutine(WaitForInput());
        }
        else
        {
            yield return new WaitForSeconds(displayDuration);
        }

        introText.text = messageLecture;
        Debug.Log("Affichage messageLecture");
        if (attendreInputPourContinuer)
        {
            yield return StartCoroutine(WaitForInput());
        }
        else
        {
            yield return new WaitForSeconds(displayDuration);
        }

        introText.text = messageFin;
        Debug.Log("Affichage messageFin");
        if (attendreInputPourContinuer)
        {
            yield return StartCoroutine(WaitForInput());
        }
        else
        {
            yield return new WaitForSeconds(displayDuration);
        }

        introText.text = "";
        SceneManager.LoadScene("MonacoScene");
    }


    IEnumerator WaitForInput()
    {
        // Attend que le joueur appuie sur la touche ESPACE pour continuer
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }
}
