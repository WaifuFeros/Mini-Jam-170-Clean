using UnityEngine;
using UnityEngine.SceneManagement;

public class gestionGameOverButton : MonoBehaviour
{
    public void restart()
    {
        SceneManager.LoadScene("Scenes/SceneTest");
    }
    public void menu()
    {
        SceneManager.LoadScene("Scenes/Start Scene");
    }
}
