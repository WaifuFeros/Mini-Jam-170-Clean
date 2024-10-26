using UnityEngine;
using UnityEngine.SceneManagement;
public class gestionButtonScript : MonoBehaviour
{
    public void starting()
    {
        SceneManager.LoadScene("Scenes/SceneTest");
    }
}
