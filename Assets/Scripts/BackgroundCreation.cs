using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundCreation : MonoBehaviour
{
    public TileBase bgTile1;
    public TileBase bgTile2;
    public void Init(int actualSize, int mapSize)
    {
        int area = (mapSize - actualSize) / 2;

        for(int y=0; y<=mapSize; y++)
            for(int x=0; x<=mapSize; x++)
            {
                    GetComponent<Tilemap>().SetTile(new Vector3Int(x,y,0),(x+y)%2==0 ? bgTile1 : bgTile2);
            }
    }

    
}
