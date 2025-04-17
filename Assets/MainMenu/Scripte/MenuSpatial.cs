using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSpatial : MonoBehaviour
{
    public Transform[] planetes;  
    public Transform cameraTransform;  
    public float vitesseDeplacement = 5f; 

    private int indexActuel = 0;
    private bool enMouvement = false;
    public Transform fusee; // Référence à la fusée
    public float hauteurArc = 2f; // Hauteur de l'arc


    public bool EnMouvement => enMouvement;


    void Update()
    {
        if (!enMouvement)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && indexActuel < planetes.Length - 1)
            {
                indexActuel++;
                StartCoroutine(DeplacerCamera(planetes[indexActuel].position));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && indexActuel > 0)
            {
                indexActuel--;
                StartCoroutine(DeplacerCamera(planetes[indexActuel].position));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChargerMiniJeu();
        }
    }

    IEnumerator DeplacerCamera(Vector3 cible)
    {
        enMouvement = true;
        Vector3 positionDepart = cameraTransform.position;
        Vector3 positionCible = new Vector3(cible.x, positionDepart.y, positionDepart.z); // Déplacement en ligne droite

        float temps = 0;
        while (temps < 1f)
        {
            temps += Time.deltaTime * vitesseDeplacement;
            Vector3 nouvellePos = Vector3.Lerp(positionDepart, positionCible, temps);
            cameraTransform.position = nouvellePos;

            // Déplacer la fusée en même temps que la caméra
            fusee.position = nouvellePos + new Vector3(0, -1, 0);  // Ajuste pour que la fusée soit sous la caméra

            // Faire tourner la fusée vers la prochaine planète
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
                SceneManager.LoadScene(4);
                break;
            case 2:
                SceneManager.LoadScene("StartScene");
                break;
        }
    }
}
