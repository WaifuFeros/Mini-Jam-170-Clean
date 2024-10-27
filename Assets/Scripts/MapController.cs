using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController instance;
    public affichageScript ui;
    public Tilemap background;
    public Tilemap detritus;
    public Transform player;
    public DetrituData[,] detritusGrid; 
    public Vector3Int playerCellPos;
    public int mapActualLength;
    public int mapMaxLength;
    public int centerZone;
    void Awake()
    {
        instance = this;
        Init(mapActualLength);
        playerCellPos = Vector3Int.one * mapActualLength/2;
    }

    public void Init(int newLength)
    {
        mapActualLength = newLength;
        detritusGrid = new DetrituData[mapMaxLength,mapMaxLength];
        centerZone = (mapMaxLength - mapActualLength) / 2;
        background.GetComponent<BackgroundCreation>().Init(mapActualLength, mapMaxLength);
    }

    void Update()
    {
        Vector3Int mouseCellPos = MouseCellPos();
        if(detritus.HasTile(playerCellPos))
            ui.PrintDetrituInfo(detritusGrid[playerCellPos.x,playerCellPos.y], 0);
        else if(detritus.HasTile(mouseCellPos))
        {
            int distance = Math.Abs(mouseCellPos.x - playerCellPos.x) + Math.Abs(mouseCellPos.y - playerCellPos.y);
            if(distance > player.GetComponent<BatteryManagement>().leftPower)
                distance = player.GetComponent<BatteryManagement>().leftPower - distance;
            ui.PrintDetrituInfo(detritusGrid[MouseCellPos().x,MouseCellPos().y], distance);
        }
        else
        {
            ui.PrintDetrituInfo(null,0,true);   
        }
    }

    
    public Vector3Int SetPlayerCellPos(Vector3 pos)
    {
        playerCellPos = background.WorldToCell(pos);
        return playerCellPos;
    }
    public bool IsInBackground()
    {
        Debug.Log(playerCellPos);
        if(playerCellPos.x > centerZone && playerCellPos.y > centerZone && playerCellPos.x <= mapActualLength+centerZone && playerCellPos.y <= mapActualLength+centerZone)
            return true;
        else
            return false;
    }

    public bool CanClean()
    {
        if(detritus.HasTile(playerCellPos))
            return true;
        else
            return false;
    }

    public DetrituData Clean(Vector3Int targetPos)
    {
        detritus.SetTile(targetPos, null);
        return detritusGrid[targetPos.x, targetPos.y];
    }

    public Vector3Int MouseCellPos()
    {
        return background.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
