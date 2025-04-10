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
            texteUI.text = "Appuyez sur ESPACE pour entrer";
        }
        else
        {
            texteUI.text = "";
        }
    }
}
