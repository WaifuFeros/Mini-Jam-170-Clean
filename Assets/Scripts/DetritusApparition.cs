using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.Tilemaps;

public class DetritusApparition : MonoBehaviour
{
    private Tilemap grid; private Vector3Int size = new Vector3Int(8,8);
    public DetrituData banana;
    void Start()
    {
        grid = GetComponent<Tilemap>();
        Debug.LogWarning(grid.size);
        Vector3Int pos = new Vector3Int(grid.size.x, grid.size.y,0);

        for(int i = 0; i < 200; i++)
            Apparition(banana);
    }

    void Apparition(DetrituData data)
    {
        Vector3Int spawnGridSize = (data.spawnZone<size.x/2 ? data.spawnZone : size.x/2-1) * new Vector3Int(2,2);

        Vector3Int origin = data.origin == new Vector3Int(-1,-1) ? 
        new Vector3Int(Random.Range(1, size.x - spawnGridSize.x), Random.Range(1, size.y - spawnGridSize.y)) :
        data.origin;

        int count = 0;
        Vector3Int pos;
        do
        {
            pos = new Vector3Int(origin.x + Random.Range(0,spawnGridSize.x), origin.y + Random.Range(0,spawnGridSize.y));
            count++;
        }
        while(grid.HasTile(pos) && count < 20);

        grid.SetTile(pos, data.tile);
    }
}
