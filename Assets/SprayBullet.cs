using System.Collections.Generic;
using UnityEngine;

public class SprayBullet : MonoBehaviour
{
    public int moveSpeed;
    private Vector3 targetPosition;
    private Rigidbody2D rb;
    public int strength;
    private bool strong;
    private List<Vector3Int> cleaned = new List<Vector3Int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(Vector2Int targetPos, bool isStrong = false)
    {
        strong = isStrong;
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = targetPos.x!=0 ? Quaternion.Euler(0,0,90) : Quaternion.identity;
        transform.GetComponent<SpriteRenderer>().flipX = targetPos.x==-1 ? true : false;
        rb.AddForce(targetPos * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int cellPos = MapController.instance.background.WorldToCell(transform.position);
        if(MapController.instance.detritus.HasTile(cellPos))
        {
            if(!cleaned.Contains(cellPos))
            {
                MapController.instance.Cleaned(cellPos, strength);
                cleaned.Add(cellPos);
            }
            
        }

        if(cleaned.Count > 0 && !strong || !MapController.instance.IsInBackground(cellPos))
            Destroy(gameObject);
    }
}
