using System;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
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
                    Clean(MapController.instance.playerCellPos);
                }
            }
        }

    }

    void Clean(Vector3Int targetPos)
    {
        MapController.instance.Clean(targetPos);
        GameManagement.instance.Action();
    }
}
