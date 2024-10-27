using System;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public affichageScript ui;
    private Movement mvt;
    private BatteryManagement battery;
    void Start()
    {
        mvt = GetComponent<Movement>();
        battery = GetComponent<BatteryManagement>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(!mvt.isMoving)
        {
            if(MapController.instance.CanClean()) // Can clean the case the player is above
            {
                // Afficher UI "Espace -> Nettoyer"
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Recycle(MapController.instance.playerCellPos);
                }
            }
        }

    }

    void Recycle(Vector3Int targetPos)
    {
        DetrituData detritu = MapController.instance.Clean(targetPos);
        battery.ChangePower(detritu.energy);
        ui.addScore(detritu.score);
        GameManagement.instance.Action();
    }
}
