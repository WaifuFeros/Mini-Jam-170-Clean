using System;
using System.Collections.Generic;
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
    public (DetrituData,int)[,] detritusGrid; 
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
        detritusGrid = new (DetrituData,int)[mapMaxLength,mapMaxLength];
        centerZone = (mapMaxLength - mapActualLength) / 2;
        background.GetComponent<BackgroundCreation>().Init(mapActualLength, mapMaxLength);
    }

    void Update()
    {
        Vector3Int mouseCellPos = MouseCellPos();
        if(ItemRecycle(true).Count>0)
            ui.PrintDetrituRecycleInfo(ItemRecycle(true).ToArray());
        else if(detritus.HasTile(mouseCellPos))
        {
            int distance = Math.Abs(mouseCellPos.x - playerCellPos.x) + Math.Abs(mouseCellPos.y - playerCellPos.y);
            if(distance > player.GetComponent<BatteryManagement>().leftPower-1)
                distance = player.GetComponent<BatteryManagement>().leftPower - distance - 1;
            ui.PrintDetrituMouseInfo(detritusGrid[mouseCellPos.x,mouseCellPos.y], distance);
        }
        else
        {
            ui.EraseDetrituInfo();  
        }
    }

    
    public Vector3Int SetPlayerCellPos(Vector3 pos)
    {
        playerCellPos = background.WorldToCell(pos);
        return playerCellPos;
    }
    public bool IsInBackground(Vector3Int cellPos)
    {
        if(cellPos.x > centerZone && cellPos.y > centerZone && cellPos.x <= mapActualLength+centerZone && cellPos.y <= mapActualLength+centerZone)
            return true;
        else
        {
            Debug.Log(cellPos);
            return false;
        }
    }

    public List<Vector3Int> ItemRecycle(bool arm = false)
    {
        List<Vector3Int> items = new List<Vector3Int>();
        for(int y= arm?-1:0; y <= (arm?1:0);y++)
            for(int x = arm?-1:0; x <=(arm?1:0); x++)
                if(detritus.HasTile(new Vector3Int(playerCellPos.x+x, playerCellPos.y+y)))// && Math.Abs(x)+Math.Abs(y)<2)
                    items.Add(new Vector3Int(playerCellPos.x+x, playerCellPos.y+y));
        
        return items;
    }

    public DetrituData RemoveItem(Vector3Int targetPos)
    {
        detritus.SetTile(targetPos, null);
        return detritusGrid[targetPos.x, targetPos.y].Item1;
    }

    public void Cleaned(Vector3Int cellPos, int strength)
    {
        detritusGrid[cellPos.x, cellPos.y].Item2 -= strength;
        if(detritusGrid[cellPos.x, cellPos.y].Item2<=0)
        {
            RemoveItem(cellPos);
        }
    }

    public Vector3Int MouseCellPos()
    {
        return background.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
