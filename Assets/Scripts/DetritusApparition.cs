using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.Tilemaps;

public class DetritusApparition : MonoBehaviour
{
    private Tilemap grid;
    void Start()
    {
        grid = GetComponent<Tilemap>();
        Vector3Int pos = new Vector3Int(grid.size.x, grid.size.y,0);
    }

    public void Apparition(string type)
    {
        DetrituData data = GameManagement.instance.detritus.FirstOrDefault(d => d.type == type);
        int length = MapController.instance.mapActualLength + 1;
        Vector3Int spawnGridSize = (data.spawnZone<length/2 ? data.spawnZone : length/2-1) * new Vector3Int(2,2);

        Vector3Int origin = data.origin == new Vector3Int(-1,-1) ? 
        new Vector3Int(Random.Range(0, length - spawnGridSize.x), Random.Range(0, length - spawnGridSize.y)) :
        data.origin;

        origin += Vector3Int.one * (MapController.instance.centerZone+1);
        int count = 0;
        Vector3Int pos;
        do
        {
            pos = new Vector3Int(origin.x + Random.Range(0,spawnGridSize.x), origin.y + Random.Range(0,spawnGridSize.y));
            count++;
        }
        while((grid.HasTile(pos) || MapController.instance.playerCellPos==pos) && count < 20);

        if(count < 20)
        {
            Debug.Log($"{pos} {data}");
            MapController.instance.itemsGrid[pos.x, pos.y] = (data,data.life);
            grid.SetTile(pos, data.tiles[Random.Range(0,data.tiles.Count())]);
        }
    }

    public void Cleaned(Vector3Int pos)
    {

    }
}
