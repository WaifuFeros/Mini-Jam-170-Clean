using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public affichageScript ui;
    private Movement mvt;
    private BatteryManagement battery;
    public GameObject sprayBullet;
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
            // Recycle action
            List<Vector3Int> itemRecycle = MapController.instance.ItemRecycle(true);
            foreach(var t in itemRecycle)
                Debug.Log(t);
            if(itemRecycle.Count>0) // Can clean the case the player is above
            {
                // Afficher UI "Espace -> Nettoyer"

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    foreach(var i in itemRecycle)
                        Recycle(i);
                }
            }

            // Test
            if(Input.GetKeyDown(KeyCode.W))
                SprayBullet(Vector2Int.up);
            else if(Input.GetKeyDown(KeyCode.A))
                SprayBullet(Vector2Int.left);
            else if(Input.GetKeyDown(KeyCode.D))
                SprayBullet(Vector2Int.right);
            else if(Input.GetKeyDown(KeyCode.S))
                SprayBullet(Vector2Int.down);
        }

    }

    void Recycle(Vector3Int targetPos)
    {
        DetrituData detritu = MapController.instance.RemoveItem(targetPos);
        if(detritu.type == "P")
        {
            // Amélioration
            Debug.Log("Amelioration");
        }
        else
        {
            battery.ChangePower(detritu.energy);
            ui.addScore(detritu.score);
        }
        GameManagement.instance.Action();
    }

    void SprayBullet(Vector2Int dir)
    {
        Instantiate(sprayBullet, new Vector2(transform.position.x, transform.position.y-0.2f), transform.rotation).GetComponent<SprayBullet>().Init(dir,true);
    }
}
