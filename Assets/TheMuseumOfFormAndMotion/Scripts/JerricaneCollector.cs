using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JerricaneCollector : MonoBehaviour
{
    public static JerricaneCollector instance;

    [Header("Configuration du compteur")]
    public int jerricaneCount = 0;
    public int requiredCount = 5;

    [Header("UI Overlay")]
    public Text counterText;
    public Text instructionText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }


    public void AddJerricane()
    {
        jerricaneCount++;
        UpdateUI();
    }


    private void UpdateUI()
    {
        if (counterText != null)
            counterText.text = "Jerricanes : " + jerricaneCount + " / " + requiredCount;

        if (jerricaneCount >= requiredCount && instructionText != null)
            instructionText.text = "Appuyez sur Entrée pour retourner au menu principal";
    }

    void Update()
    {
        if (jerricaneCount >= requiredCount && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }
}
