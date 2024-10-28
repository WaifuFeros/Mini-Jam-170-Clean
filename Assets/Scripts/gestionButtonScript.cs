using UnityEngine;
using UnityEngine.SceneManagement;
public class gestionButtonScript : MonoBehaviour
{
    public void starting()
    {
        SceneManager.LoadScene("Scenes/MariusDevScene");
    }
    public void exiting()
    {
        Application.Quit();
    }
}
