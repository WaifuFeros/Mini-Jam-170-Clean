using UnityEngine;

public class BatteryManagement : MonoBehaviour
{
    public affichageScript ui;
    public int leftPower;
    public int maxPower;

    public int ChangePower(int add)
    {
        leftPower += add;
        if(leftPower <= 0)
        {

            // Fin de partie
        }
        else if(leftPower>maxPower)
            leftPower = maxPower;
        
        ui.ChangePower(leftPower);
        return leftPower;
    }
}
