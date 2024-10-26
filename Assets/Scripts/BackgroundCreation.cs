using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundCreation : MonoBehaviour
{
    public TileBase bgTile1;
    public TileBase bgTile2;
    public void Init(int size)
    {
        for(int y=0; y<size; y++)
            for(int x=0; x<size; x++)
            {
                GetComponent<Tilemap>().SetTile(new Vector3Int(x,y,0),(x+y)%2==0 ? bgTile1 : bgTile2);
            }
    }

    
}
