using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;
    private Dictionary<string, StoryNode> storyNodes;
    private StoryNode currentNode;
    public GameObject overlay;
    public GameObject ChoicePanel;
    public Button Start;
    public Button Suivant;
    public Button Precedent;

    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Supprime les duplicatas
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadStory();

    }

    void LoadStory()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("story");
        if (jsonFile == null)
        {
            Debug.LogError("story.json introuvable !");
            return;
        }

        StoryData data = JsonUtility.FromJson<StoryData>(jsonFile.text);
        storyNodes = new Dictionary<string, StoryNode>();
        foreach (var node in data.nodes)
        {
            storyNodes[node.nodeId] = node;
        }
    }

    public void StartStory()
    {
        Start.gameObject.SetActive(false);

        if (storyNodes == null || storyNodes.Count == 0)
        {
            Debug.Log("storyNodes est null ou on ne detecte aucun noeud");
            return;
        }
        Debug.Log("Story est bien détecté !");
        currentNode = storyNodes["start"];
        overlay.SetActive(true);
        ChoicePanel.SetActive(false);
        DisplayCurrentNode();
    }

    void DisplayCurrentNode()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager.Instance est null !");
            return;
        }

        if (currentNode == null)
        {
            Debug.LogError("currentNode est null !");
            return;
        }

        DialogueManager.Instance.StartDialogue(new string[] { currentNode.dialogueText });

        Suivant.gameObject.SetActive(true);
        Precedent.gameObject.SetActive(true);

        if (currentNode.choices.Count > 0)
        {
            Debug.Log("Il y a des choix à afficher");
            Suivant.gameObject.SetActive(false);
            Precedent.gameObject.SetActive(false);
            DialogueManager.Instance.ShowChoices(currentNode.nodeId, currentNode.choices);
        }
    }

    public void ChooseOption(string nextNodeId)
    {
        if (storyNodes.ContainsKey(nextNodeId))
        {
            currentNode = storyNodes[nextNodeId];
            ChangeScene(currentNode.sceneName);
        }
    }

    public void ShowNextNode()
    {
        Debug.Log("Show Next Node donc Suivant appuyé");
        if (currentNode.choices.Count > 0)
        {
            Debug.Log("Le nœud actuel a des choix, impossible d'avancer automatiquement.");
            return;
        }

        if (!string.IsNullOrEmpty(currentNode.nextNodeId) && storyNodes.ContainsKey(currentNode.nextNodeId))
        {
            currentNode = storyNodes[currentNode.nextNodeId];
            ChangeScene(currentNode.sceneName); 
        }
        else
        {
            Debug.Log("Fin de l'histoire ou nœud introuvable !");
        }
    }

    void ChangeScene(string sceneName)
    {
        Debug.Log($"Changement vers la scène : {sceneName}");

        SceneManager.LoadScene(sceneName);
        StartCoroutine(WaitAndSetupScene(sceneName));

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scène chargée : {scene.name}");

        overlay = GameObject.Find("overlay");
        Debug.Log(overlay);
        ChoicePanel = overlay?.transform.Find("ChoicePanel")?.gameObject;
        Debug.Log(ChoicePanel);

        Suivant = overlay?.transform.Find("Suivant")?.GetComponent<Button>();
        Debug.Log(Suivant);

        Precedent = overlay?.transform.Find("Precedent")?.GetComponent<Button>();
        Debug.Log(Precedent);


        if (scene.name == currentNode.sceneName)
        {
            Debug.Log("Mise à jour des références et affichage du nouveau contenu");
            DisplayCurrentNode();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator WaitAndSetupScene(string sceneName)
    {
        yield return new WaitForSeconds(0.1f);

        Debug.Log("Chargement de la scène terminé : " + sceneName);

        overlay = GameObject.Find("overlay");
        if (overlay == null)
        {
            Debug.LogError("Overlay introuvable !");
        }
        else
        {
            ChoicePanel = overlay.transform.Find("ChoicePanel")?.gameObject;
            ChoicePanel.SetActive(false);
            Suivant = overlay.transform.Find("Suivant")?.GetComponent<Button>();
            Precedent = overlay.transform.Find("Precedent")?.GetComponent<Button>();

            if (Suivant != null)
            {
                Suivant.onClick.RemoveAllListeners();
                Debug.Log("Listeners supprimés. Ajout du nouveau listener...");
                Suivant.onClick.AddListener(() => {
                    Debug.Log("Le bouton Suivant a bien été cliqué !");
                    ShowNextNode();
                });

                Debug.Log("Listener ajouté !");
            }

            else
            {
                Debug.LogError("Bouton Suivant introuvable !");
            }


            Suivant.gameObject.SetActive(false);
            Precedent.gameObject.SetActive(false);

            if (sceneName == currentNode.sceneName)
            {
                DisplayCurrentNode();
            }
        }
    }
}

[System.Serializable]
public class StoryData { public List<StoryNode> nodes; }

[System.Serializable]
public class StoryNode
{
    public string nodeId;
    public string sceneName;
    public string dialogueText;
    public List<Choice> choices;
    public string nextNodeId;
}

[System.Serializable]
public class Choice
{
    public string choiceText;
    public string nextNodeId;
}
