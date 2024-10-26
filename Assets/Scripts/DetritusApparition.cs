using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.Tilemaps;

public class DetritusApparition : MonoBehaviour
{
    private Tilemap grid;
    private string[] detritusId = new string[] {"SO","SP","MO","MP","GO","GP"};
    void Start()
    {
        grid = GetComponent<Tilemap>();
        Vector3Int pos = new Vector3Int(grid.size.x, grid.size.y,0);
    }

    public void Apparition(string type)
    {
        DetrituData data = GameManagement.instance.detritus[detritusId.ToList().IndexOf(type)];
        int length = MapController.instance.mapActualLength + 1;
        Vector3Int spawnGridSize = (data.spawnZone<length/2 ? data.spawnZone : length/2-1) * new Vector3Int(2,2);

        Vector3Int origin = data.origin == new Vector3Int(-1,-1) ? 
        new Vector3Int(Random.Range(0, length - spawnGridSize.x), Random.Range(0, length - spawnGridSize.y)) :
        data.origin;

        int count = 0;
        Vector3Int pos;
        do
        {
            pos = new Vector3Int(origin.x + Random.Range(0,spawnGridSize.x), origin.y + Random.Range(0,spawnGridSize.y));
            count++;
        }
        while(grid.HasTile(pos) && count < 20);


        grid.SetTile(pos, data.tiles[Random.Range(0,data.tiles.Count())]);
    }

    public void Cleaned(Vector3Int pos)
    {

    }
}
