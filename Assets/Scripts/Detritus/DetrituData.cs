using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "DetrituType", menuName = "MiniJam170/Detritu Type")]

public class DetrituData : ScriptableObject
{
    public string type;
    public List<TileBase> tiles;
    public int spawnZone;
    public Vector3Int origin;
    public int life;
    public int energy; 
    public int score;
}
