using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "DetrituType", menuName = "MiniJam170/Detritu Type")]

public class DetrituData : ScriptableObject
{
    public TileBase tile;
    public int spawnZone;
    public Vector3Int origin;
}
