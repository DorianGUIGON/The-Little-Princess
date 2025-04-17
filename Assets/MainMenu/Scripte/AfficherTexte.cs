using UnityEngine;
using UnityEngine.UI;

public class AfficherTexte : MonoBehaviour
{
    public Text texteUI;
    public MenuSpatial menuScript;

    void Update()
    {
        if (menuScript != null && !menuScript.EnMouvement)
        {
            int i = menuScript.IndexActuel;
            // Si on n'est pas sur la première planète et que la précédente n'a pas été visitée, on indique que la planète est verrouillée.
            if (i > 0 && !menuScript.planetVisited[i - 1])
            {
                texteUI.text = "Planète verrouillée";
            }
            else
            {
                // Affichage de la description si disponible, sinon le message par défaut.
                if (menuScript.descriptions != null && i < menuScript.descriptions.Length)
                {
                    texteUI.text = menuScript.descriptions[i] + "\nAppuyez sur ESPACE pour entrer";
                }
                else
                {
                    texteUI.text = "Appuyez sur ESPACE pour entrer";
                }
            }
        }
        else
        {
            texteUI.text = "";
        }
    }
}
