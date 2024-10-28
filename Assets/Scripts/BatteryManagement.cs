using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatteryManagement : MonoBehaviour
{
    private affichageScript ui;
    private Movement movement;
    private Animator animator;
    public int leftPower;
    public int maxPower;
    [Min(0)] public float gameOverDelay = 3;
    public int gameOverSceneIndex = 2;

    void Start ()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<affichageScript>();
        ui.ChangePower(leftPower,maxPower);
    }
    public int ChangePower(int add)
    {
        leftPower += add;
        if(leftPower <= 0 && !movement.invicible)
        {
            animator.SetTrigger("Death");
            movement.isDead = true;
            // Fin de partie
            StartCoroutine(GameOver());
        }
        else if(leftPower>maxPower)
            leftPower = maxPower;
        
        ui.ChangePower(leftPower, maxPower);
        return leftPower;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);

        SceneManager.LoadScene(gameOverSceneIndex);
    }
}
