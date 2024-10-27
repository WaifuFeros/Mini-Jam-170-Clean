using UnityEngine;

public class SprayBullet : MonoBehaviour
{
    public int moveSpeed;
    private Vector3 targetPosition;
    private Rigidbody2D rb;
    public int strength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(Vector2Int targetPos, bool strong = false)
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = targetPos.x!=0 ? Quaternion.Euler(0,0,90) : Quaternion.identity;
        transform.GetComponent<SpriteRenderer>().flipX = targetPos.x==-1 ? true : false;
        rb.AddForce(targetPos * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(MapController.instance.detritus.HasTile(MapController.instance.background.WorldToCell(transform.position)))
        {
            MapController.instance.Cleaned(MapController.instance.background.WorldToCell(transform.position), strength);
            Destroy(gameObject);
        }
    }
}
