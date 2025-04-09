using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCouleur : MonoBehaviour
{

    [System.Serializable]
    public class ColorObject
    {
        public GameObject obj;
        [HideInInspector] public Color originalColor;
        [HideInInspector] public Material matInstance;
    }

    public List<ColorObject> objects = new List<ColorObject>();

    private bool showR = false;
    private bool showG = false;
    private bool showB = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var colorObj in objects)
        {
            Renderer rend = colorObj.obj.GetComponent<Renderer>();
            if (rend != null)
            {
                colorObj.matInstance = rend.material; // Crée une instance
                colorObj.originalColor = colorObj.matInstance.color;
                ApplyColorMask(colorObj); // Met à jour au démarrage
            }
        }

        Debug.Log("ColorObject list initialized with " + objects.Count + " objects.");
    }

    void Update()
    {
        // if a is pressed toggle red
        if (Input.GetKeyDown(KeyCode.A))
        {
            showR = !showR;
            ToggleRed(showR);
            Debug.Log("Red toggled: " + showR);
        }

        // if z is pressed toggle green
        if (Input.GetKeyDown(KeyCode.Z))
        {
            showG = !showG;
            ToggleGreen(showG);
            Debug.Log("Green toggled: " + showG);
        }

        // if e is pressed toggle blue
        if (Input.GetKeyDown(KeyCode.E))
        {
            showB = !showB;
            ToggleBlue(showB);
            Debug.Log("Blue toggled: " + showB);
        }
    }

    public void ToggleRed(bool value) { showR = value; UpdateAll(); }

    public void ToggleGreen(bool value) { showG = value; UpdateAll(); }

    public void ToggleBlue(bool value) { showB = value; UpdateAll(); }

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
        float r = showR ? original.r : 0f;
        float g = showG ? original.g : 0f;
        float b = showB ? original.b : 0f;
        obj.matInstance.color = new Color(r, g, b, original.a);
    }
}
