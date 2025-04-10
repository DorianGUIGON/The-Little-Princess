using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MonacoScene");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
