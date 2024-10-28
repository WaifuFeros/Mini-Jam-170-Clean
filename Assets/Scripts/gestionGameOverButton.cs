using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class gestionGameOverButton : MonoBehaviour
{
    public TextMeshProUGUI sco;
    private void Start()
    {

        sco.text = "Score : " + affichageScript.score.ToString();
    }
    public void restart()
    {
        SceneManager.LoadScene("Scenes/MariusDevScene");
    }
    public void menu()
    {
        SceneManager.LoadScene("Scenes/Start Scene");
    }
}
