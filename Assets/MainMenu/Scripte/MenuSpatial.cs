using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSpatial : MonoBehaviour
{
    [Header("Configuration des planètes et textes")]
    public Transform[] planetes;
    public string[] descriptions; // Description de chaque planète (un mini-jeu)

    [Header("Références caméra et fusee")]
    public Transform cameraTransform;
    public float vitesseDeplacement = 5f;
    public Transform fusee;
    public float hauteurArc = 2f;

    // Pour suivre l'état d'accès de chaque planète
    public bool[] planetVisited;


    [Header("UI Feedback")]
    public Text lockedMessageUI;

    private int indexActuel = 0;
    private bool enMouvement = false;

    // Propriété pour accéder à l'index courant depuis d'autres scripts (par exemple AfficherTexte)
    public int IndexActuel { get => indexActuel; }
    public bool EnMouvement => enMouvement;

    void Start()
    {
        // Initialisation du tableau d'accès en fonction du nombre de planètes
        planetVisited = new bool[planetes.Length];

        // Charger l'état de visite à partir des PlayerPrefs
        for (int i = 0; i < planetVisited.Length; i++)
        {
            planetVisited[i] = PlayerPrefs.GetInt("PlanetVisited_" + i, 0) == 1;
        }
    }

    void Update()
    {
        UpdatePlanetVisuals();

        if (!enMouvement)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && indexActuel < planetes.Length - 1)
            {
                int nextIndex = indexActuel + 1;
                // Pour accéder à la planète d'index nextIndex, il faut que la précédente (nextIndex - 1) ait été visitée.
                if (nextIndex == 0 || (nextIndex > 0 && planetVisited[nextIndex - 1]))
                {
                    indexActuel = nextIndex;
                    StartCoroutine(DeplacerCamera(planetes[indexActuel].position));
                }
                else
                {
                    Debug.Log("Planète verrouillée !");
                    StartCoroutine(DisplayLockedMessage());
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && indexActuel > 0)
            {
                // La flèche gauche est toujours autorisée
                indexActuel--;
                StartCoroutine(DeplacerCamera(planetes[indexActuel].position));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Vérifier si la planète est accessible
            if (indexActuel == 0 || (indexActuel > 0 && planetVisited[indexActuel - 1]))
            {
                // Marquer la planète actuelle comme visitée
                planetVisited[indexActuel] = true;
                PlayerPrefs.SetInt("PlanetVisited_" + indexActuel, 1);
                PlayerPrefs.Save();

                ChargerMiniJeu();
            }
            else
            {
                Debug.Log("Planète verrouillée, impossible d'entrer.");
                StartCoroutine(DisplayLockedMessage());
            }
        }
    }

    IEnumerator DisplayLockedMessage()
    {
        if (lockedMessageUI != null)
        {
            lockedMessageUI.text = "La planète suivante est verrouillée, veuillez jouer à celle-ci pour accéder à la suivante";
            yield return new WaitForSeconds(3f);
            lockedMessageUI.text = "";
        }
    }

    IEnumerator DeplacerCamera(Vector3 cible)
    {
        enMouvement = true;
        Vector3 positionDepart = cameraTransform.position;
        Vector3 positionCible = new Vector3(cible.x, positionDepart.y, positionDepart.z);

        float temps = 0;
        while (temps < 1f)
        {
            temps += Time.deltaTime * vitesseDeplacement;
            Vector3 nouvellePos = Vector3.Lerp(positionDepart, positionCible, temps);
            cameraTransform.position = nouvellePos;

            // Déplacer la fusee en suivant la caméra
            fusee.position = nouvellePos + new Vector3(0, -1, 0);
            fusee.LookAt(positionCible);

            yield return null;
        }
        enMouvement = false;
    }

    void ChargerMiniJeu()
    {
        switch (indexActuel)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene("IntroCourse");
                break;
            case 2:
                SceneManager.LoadScene("StartScene");
                break;
        }
    }

    void UpdatePlanetVisuals()
    {
        // Met à jour la couleur des planètes pour indiquer si elles sont verrouillées ou non (si elles possèdent un Renderer)
        for (int i = 0; i < planetes.Length; i++)
        {
            Renderer rend = planetes[i].GetComponent<Renderer>();
            if (rend != null)
            {
                // Pour i == 0, la planète est toujours accessible.
                // Pour i > 0, la planète est accessible si la précédente (i - 1) a été visitée.
                if (i == 0 || (i > 0 && planetVisited[i - 1]))
                {
                    rend.material.color = Color.blue;
                }
                else
                {
                    rend.material.color = Color.red;
                }
            }
        }
    }
}
