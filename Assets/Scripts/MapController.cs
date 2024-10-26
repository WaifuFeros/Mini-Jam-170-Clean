using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController instance;
    public Tilemap background;
    public Tilemap detritus;
    public int mapLength;
    void Awake()
    {
        instance = this;
        Init(mapLength);
    }

    public void Init(int newLength)
    {
        mapLength = newLength;
        switch(mapLength)
        {
            case 6:
            transform.localScale = Vector3.one * 4/3f;
            break;

            case 8:
            transform.localScale = Vector3.one;
            break;

            case 10:
            transform.localScale = Vector3.one * 0.8f;
            break;
        }
        background.GetComponent<BackgroundCreation>().Init(mapLength);
    }

    public bool IsInBackground(Vector3 pos)
    {
        Vector3Int cellPos = background.WorldToCell(pos);
        Debug.Log(cellPos);
        if(cellPos.x >= 0 && cellPos.y >= 0 && cellPos.x < mapLength && cellPos.y < mapLength)
            return true;
        else
            return false;
    }
}
