using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundCreation : MonoBehaviour
{
    private Tilemap grid;
    public TileBase bgTile;

    public Transform playerPos;
    void Start()
    {
        grid = GetComponent<Tilemap>();
        Init(new Vector3Int(8,8,1));

    }

    void Init(Vector3Int dim)
    {
        for(int y=0; y<dim.y; y++)
            for(int x=0; x<dim.x; x++)
            {
                grid.SetTile(new Vector3Int(x,y,0),bgTile);
            }
    }

    // Update is called once per frame
    void Update()
    {
        // // Debug.LogWarning(grid.WorldToCell(playerPos.position));
        // int xMov = (int)Input.GetAxis("Horizontal");
        // int yMov = (int)Input.GetAxis("Vertical");
        // Debug.Log($"{xMov} {yMov}");
        // if(xMov+yMov > 0)
        //     playerPos.position = MovePlayer(xMov,yMov);
    }

    // Vector3 MovePlayer(int xMov, int yMov)
    // {
    //     var c = grid.WorldToCell(playerPos.position);
    //     Vector3Int targetPos = new Vector3Int(c.x+xMov,c.y+yMov,c.z);

    //     if(targetPos.x > 0 && targetPos.y > 0 && targetPos.x < grid.size.x && targetPos.y < grid.size.y)
    //     {
    //         Vector3 newPos = grid.CellToWorld(targetPos);
    //         return new Vector3(newPos.x - grid.cellSize.x * 0.6f, newPos.y - grid.cellSize.y * 0.3f);
    //     }
    //     else
    //     {
    //         return playerPos.position;
    //     }
    // }
}
