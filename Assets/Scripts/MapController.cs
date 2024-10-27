using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController instance;
    public Tilemap background;
    public Tilemap detritus;
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
}
