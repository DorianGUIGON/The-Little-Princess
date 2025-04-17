using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroMuséeManager : MonoBehaviour
{
    [Header("Référence UI")]
    public Text introText; // Assignez-y le composant Text "IntroText" créé dans le Canvas.

    [Header("Configuration d'affichage")]
    public float displayDuration = 3f; // Durée d'affichage de chaque message (en secondes)
    public bool attendreInputPourContinuer = false; // Voulez-vous que le joueur appuie sur une touche pour passer au message suivant ?

    [Header("Messages d'introduction")]
    [TextArea(3, 10)]
    public string messageAtterrissage = "Déjà à court de carburant ? Heureusement que j'ai trouvé ce musée pour me poser en urgence !";
    [TextArea(3, 10)]
    public string messageProspectus = "J'espère tout de même trouver ici de quoi repartir vers de nouveaux horizons.";
    [TextArea(3, 10)]
    public string messageLecture = "Oh ? Un jerrican d'essence, y en aurait-il d'autres cachés dans ce musée ?";
    [TextArea(3, 10)]
    public string messageFin = "Allons-y ! Allons visiter ce musée !";



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
        SceneManager.LoadScene("MuseumScene");
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
