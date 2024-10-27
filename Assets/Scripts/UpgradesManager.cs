using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    /// <summary>
    /// 1 : Simple recycling
    /// 2 : 
    /// 3 :
    /// 4 : 
    /// 5 : Arm -> let recycle every items in adjacent and diagonal cases 
    /// </summary>
    public int recyclingLevel = 0;
    /// <summary>
    /// 1 : Simple bullet
    /// 2 : Simple piercing bullet
    /// 3 : Two piercing bullets in a line shape
    /// 4 : Four piercing bullets in a cross shape
    /// 5 : Four piercing bullets in a cross shape that induce a cross bullets for every hitted item
    /// </summary>
    public int sprayLevel = 0;
    /// <summary>
    /// 1 : Bomb (1 damage to every items)
    /// 2 : Dash (For 3 powers, go to the other side of the map)
    /// 3 : Strong Bomb (3 damages to every items)
    /// 4 : Recycling Dash (recycle item during the dash)
    /// 5 : Teleportation
    /// </summary>
    public int gadgetLevel = 0;

    public void ChangeLevel(string which)
    {
        switch (which)
        {
            case "Recycle": recyclingLevel++; break;
            case "Spray": sprayLevel++; break;
            case "Gadget": gadgetLevel++; break;
        }
        GetComponent<BatteryManagement>().maxPower+=2;
    }
}
