using UnityEngine;

public class BatteryManagement : MonoBehaviour
{
    private affichageScript ui;
    private Movement movement;
    private Animator animator;
    public int leftPower;
    public int maxPower;
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
        }
        else if(leftPower>maxPower)
            leftPower = maxPower;
        
        ui.ChangePower(leftPower, maxPower);
        return leftPower;
    }
}
