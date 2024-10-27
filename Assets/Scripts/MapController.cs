using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController instance;
    private affichageScript ui;
    public Tilemap background;
    public Tilemap detritus;
    public Transform player;
    private PlayerAction playerAction;
    public (DetrituData,int)[,] itemsGrid; 
    public Vector3Int playerCellPos;
    public int mapActualLength;
    public int mapMaxLength;
    public int centerZone;
    public Transform fogMask;
    public float transitionSpeed;
    public bool isFogMoving;
    Vector3 targetScale;
    void Awake()
    {
        instance = this;
        MapInit(mapActualLength);
        playerCellPos = Vector3Int.one * mapActualLength/2;
        playerAction = player.GetComponent<PlayerAction>();
        itemsGrid = new (DetrituData,int)[mapMaxLength+1,mapMaxLength+1];
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<affichageScript>();
    }

    public void MapInit(int newLength)
    {
        mapActualLength = newLength;
        centerZone = (mapMaxLength - mapActualLength) / 2;

        targetScale = Vector2.one * mapActualLength * 0.8f;
        isFogMoving = true;
    }
    
    void Update()
    {
        Vector3Int mouseCellPos = MouseCellPos();
        if(playerAction.ItemRecycle().Count>0)
            ui.PrintDetrituRecycleInfo(playerAction.ItemRecycle().ToArray());
        else if(detritus.HasTile(mouseCellPos))
        {
            int distance = Math.Abs(mouseCellPos.x - playerCellPos.x) + Math.Abs(mouseCellPos.y - playerCellPos.y);
            if(distance > player.GetComponent<BatteryManagement>().leftPower-1)
                distance = player.GetComponent<BatteryManagement>().leftPower - distance - 1;
            ui.PrintDetrituMouseInfo(itemsGrid[mouseCellPos.x,mouseCellPos.y], distance);
        }
        else
        {
            ui.EraseDetrituInfo();  
        }

        if(isFogMoving)
        {
            fogMask.localScale = Vector2.MoveTowards(fogMask.localScale,targetScale, transitionSpeed*Time.deltaTime);
            if(fogMask.localScale==targetScale)
                isFogMoving = false;
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

    public DetrituData RemoveItem(Vector3Int targetPos)
    {
        detritus.SetTile(targetPos, null);
        return itemsGrid[targetPos.x, targetPos.y].Item1;
    }

    public void Cleaned(Vector3Int cellPos, int strength)
    {
        itemsGrid[cellPos.x, cellPos.y].Item2 -= strength;
        if(itemsGrid[cellPos.x, cellPos.y].Item2<=0)
        {
            RemoveItem(cellPos);
        }
    }

    public Vector3Int MouseCellPos()
    {
        return background.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
