using UnityEngine;

public class BatteryManagement : MonoBehaviour
{
    public affichageScript ui;
    private Movement movement;
    private Animator animator;
    public int leftPower;
    public int maxPower;
    void Start ()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }
    public int ChangePower(int add)
    {
        leftPower += add;
        if(leftPower <= 0)
        {
            animator.SetTrigger("Death");
            movement.isDead = true;
            // Fin de partie
        }
        else if(leftPower>maxPower)
            leftPower = maxPower;
        
        ui.ChangePower(leftPower);
        return leftPower;
    }
}
