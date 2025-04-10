using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button Suivant;
    public GameObject choicePanel;
    public List<Button> choiceButtons;
    public Button Precedent;
    public static DialogueManager Instance;
    public GameObject overlay;

    private Queue<string> dialogues;
    private Stack<Queue<string>> dialogueHistory = new Stack<Queue<string>>();
    private GameObject dialogueBox;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Supprime l'objet en double
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Garde une seule instance persistante
        dialogues = new Queue<string>();
    }

    void Start()
    {
        Debug.Log("DialogueManager est chargé dans la scène : " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    public void StartDialogue(string[] sentences)
    {
        overlay = GameObject.Find("overlay");
        choicePanel = overlay.transform.Find("ChoicePanel")?.gameObject;
        dialogueBox = overlay.transform.Find("DialogueBox")?.gameObject;
        dialogueText = dialogueBox.transform.Find("DialogueText")?.GetComponent<TMP_Text>();
        Precedent = overlay.transform.Find("Precedent")?.GetComponent<Button>();
        Suivant = overlay.transform.Find("Suivant")?.GetComponent<Button>();

        choiceButtons = new List<Button>();
        foreach (Transform child in choicePanel.transform)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
                choiceButtons.Add(btn);
        }


        if (dialogues.Count > 0)
        {
            Debug.Log("on push dans history");
            dialogueHistory.Push(new Queue<string>(dialogues));
        }

        Debug.Log(dialogues == null ? "dialogues est NULL" : "dialogues est OK");
        dialogues.Clear();

        foreach (string sentence in sentences)
        {
            dialogues.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogues.Count == 0)
        {
            Debug.Log("Fin du dialogue");
            return;
        }

        string sentence = dialogues.Dequeue();
        dialogueText.text = sentence;
    }

    public void ShowChoices(string nodeId, List<Choice> choices)
    {
        Debug.Log("On affiche un choix");

        if (choicePanel == null)
        {
            Debug.LogError("choicePanel est NULL !");
            return;
        }

        choicePanel.SetActive(true);
        Debug.Log(choices.Count);

        if (choiceButtons == null || choiceButtons.Count == 0)
        {
            Debug.LogError("choiceButtons est NULL ou VIDE !");
            return;
        }


        for (int i = 0; i < choiceButtons.Count; i++)
        {
            
            if (i < choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = choices[i].choiceText;

                int index = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() =>
                {
                    StoryManager.Instance.ChooseOption(choices[index].nextNodeId);
                    choicePanel.SetActive(false);
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
        if(choices.Count == 0)
        {
            Suivant.gameObject.SetActive(true);
            Precedent.gameObject.SetActive(true);
        }
    }

    public void GoBack()
    {
        if (dialogueHistory.Count > 0)
        {
            dialogues = dialogueHistory.Pop();
            DisplayNextSentence();
        }
        else
        {
            Debug.Log("Aucun dialogue précédent !");
        }

        Precedent.interactable = dialogueHistory.Count > 0;
    }
}
