using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
