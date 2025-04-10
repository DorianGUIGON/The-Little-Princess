using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorManager : MonoBehaviour
{
    [System.Serializable]
    public class ColorObject
    {
        public GameObject obj;
        [HideInInspector] public Color originalColor;
        [HideInInspector] public Material matInstance;
        [HideInInspector] public string spriteName;
    }

    public static ColorManager Instance;
    public List<ColorObject> objects = new List<ColorObject>();

    private bool showR = false;
    private bool showG = false;
    private bool showB = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeObjectsColor();
    }


    void Update()
    {
        // if a is pressed toggle red
        if (Input.GetKeyDown(KeyCode.F))
        {
            showR = !showR;
            ToggleRed(showR);
            Debug.Log("Red toggled: " + showR);
        }

        // if z is pressed toggle green
        if (Input.GetKeyDown(KeyCode.G))
        {
            showG = !showG;
            ToggleGreen(showG);
            Debug.Log("Green toggled: " + showG);
        }

        // if e is pressed toggle blue
        if (Input.GetKeyDown(KeyCode.H))
        {
            showB = !showB;
            ToggleBlue(showB);
            Debug.Log("Blue toggled: " + showB);
        }
    }


    void InitializeObjectsColor()
    {
        objects.Clear();
        GameObject[] all = FindObjectsOfType<GameObject>();

        foreach (var go in all)
        {
            if (go.GetComponent<Renderer>() != null && !go.name.Contains("test"))
            {
                var obj = new ColorObject
                {
                    obj = go,
                    matInstance = go.GetComponent<Renderer>().material,
                    originalColor = go.GetComponent<Renderer>().material.color,
                    spriteName = go.GetComponent<SpriteRenderer>() != null ? go.GetComponent<SpriteRenderer>().sprite.name : ""
                };

                ApplyColorMask(obj);
                objects.Add(obj);
            }
        }

        Debug.Log("Nouvelle liste d'objets : " + objects.Count + " objets dans la scène.");
    }

    public void ToggleRed(bool value) { showR = value; UpdateAll(); }

    public void ToggleGreen(bool value) { showG = value; UpdateAll(); }

    public void ToggleBlue(bool value) { showB = value; UpdateAll(); }

    public bool AreColorsActive()
    {
        return showR || showG || showB;
    }


    private void UpdateAll()
    {
        foreach (var colorObj in objects)
        {
            ApplyColorMask(colorObj);
        }
    }


    private void ApplyColorMask(ColorObject obj)
    {
        Color original = obj.originalColor;
            
        if (obj.obj.GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer spriteRenderer = obj.obj.GetComponent<SpriteRenderer>();


            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Sprite color applied");
        }
        else
        {
            float minColorValue = Mathf.Max(Mathf.Min(original.r, original.g, original.b) - 0.1f, 0);
            Color greyedColor = new Color(minColorValue, minColorValue, minColorValue, original.a);

            float r = showR ? original.r : greyedColor.r;
            float g = showG ? original.g : greyedColor.g;
            float b = showB ? original.b : greyedColor.b;
            obj.matInstance.color = new Color(r, g, b, original.a);
        }
    }
}
