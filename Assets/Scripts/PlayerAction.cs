using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    public affichageScript ui;
    private Movement mvt;
    private Animator animator;              // Référence a l'Animator
    private BatteryManagement battery;
    public GameObject sprayBullet;

    // Actions
    public bool wantDash = false;
    public bool wantTeleport = false;

    // A enlever quand victor aura fait le upgrade manager
    public int sprayLevel;
    public int gadgetLevel;
    public int recycleLevel;
    void Start()
    {
        mvt = GetComponent<Movement>();
        battery = GetComponent<BatteryManagement>();    
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!mvt.isMoving)
        {
            #region Recycle action (Input Space)
            List<Vector3Int> itemRecycle = ItemRecycle();
            if(itemRecycle.Count>0) // Can clean the case the player is above
            {
                // Afficher UI "Espace -> Nettoyer"

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    foreach(var i in itemRecycle)
                        Recycle(i);
                }
            }
            #endregion

            #region Spray action (Inputs ZQSD)
            if(Input.GetKeyDown(KeyCode.W))
                SprayBullet(Vector2Int.up,transform,sprayLevel);
            else if(Input.GetKeyDown(KeyCode.A))
                SprayBullet(Vector2Int.left,transform,sprayLevel);
            else if(Input.GetKeyDown(KeyCode.D))
                SprayBullet(Vector2Int.right,transform,sprayLevel);
            else if(Input.GetKeyDown(KeyCode.S))
                SprayBullet(Vector2Int.down,transform,sprayLevel);
            #endregion

            #region Bomb action (Input Mouse button or Shortuct G)
            if(Input.GetKeyDown(KeyCode.G) && gadgetLevel>0)
            {
                Bomb();
            }
            #endregion

            #region Dash action (Input Mouse button or Shortcut H)
            if(Input.GetKey(KeyCode.H) && gadgetLevel>1)
            {
                wantDash = true;
                // See movement script where the dash is managed
            }
            #endregion

            #region Teleport action (Input Mouse button or Shortcut J)
            if(Input.GetKeyDown(KeyCode.J) && gadgetLevel>=5)
            {
                wantTeleport = true;
            }
            if(Input.GetMouseButtonDown(0) && wantTeleport && MapController.instance.IsInBackground(MapController.instance.MouseCellPos()))
            {
                Teleport();
            }
            #endregion
        }

    }

    public void Recycle(Vector3Int targetPos, bool anim = true)
    {
        if(anim)
            animator.SetTrigger("Collect");
        DetrituData detritu = MapController.instance.RemoveItem(targetPos);
        if(detritu.type == "P")
        {
            // Amélioration
            Debug.Log("Amelioration");
        }
        else
        {
            battery.ChangePower(detritu.energy);
            ui.addScore(detritu.score);
        }
        GameManagement.instance.Action();
    }

    public List<Vector3Int> ItemRecycle()
    {
        bool arm = recycleLevel==5 ? true : false;
        List<Vector3Int> items = new List<Vector3Int>();
        for(int y= arm?-1:0; y <= (arm?1:0);y++)
            for(int x = arm?-1:0; x <=(arm?1:0); x++)
                if(MapController.instance.detritus.HasTile(new Vector3Int(MapController.instance.playerCellPos.x+x, MapController.instance.playerCellPos.y+y)))// && Math.Abs(x)+Math.Abs(y)<2)
                    items.Add(new Vector3Int(MapController.instance.playerCellPos.x+x, MapController.instance.playerCellPos.y+y));
        
        return items;
    }

    public void SprayBullet(Vector2Int dir, Transform shooter, int level)
    {
        Vector2 p = MapController.instance.background.CellToWorld(MapController.instance.background.WorldToCell(shooter.position));
        Vector2 pos = new Vector2(p.x+0.4f,p.y+0.4f);
        switch (level)
        {
            case 1: // Single spray bullet
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(dir, false);
            break;

            case 2: // Single spray bullet but that go through items
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(dir);
            break;

            case 3: // Two spray bullets in line 
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(dir);
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(dir * -Vector2Int.one);
            break;

            case 4: // Four spray bullets in cross
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(Vector2Int.right);
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(Vector2Int.down);
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(Vector2Int.left);
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(Vector2Int.up);
            break;

            case 5: // Cross bullets but each hitted item will induce a new cross bullet, once
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(Vector2Int.right,true,true);
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(Vector2Int.down,true,true);
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(Vector2Int.left,true,true);
            Instantiate(sprayBullet, pos, shooter.rotation).GetComponent<SprayBullet>().Init(Vector2Int.up,true,true);
            break;
        }
    }

    public void Bomb()
    {
        int damages = gadgetLevel>2 ? 3 : 1;
        for(int y=0; y<MapController.instance.itemsGrid.GetLength(1); y++)
            for(int x=0; x<MapController.instance.itemsGrid.GetLength(0); x++)    
            {
                MapController.instance.Cleaned(new Vector3Int(x,y), damages);
            }
    }

    public void Teleport()
    {
        Vector2 p = MapController.instance.background.CellToWorld(MapController.instance.MouseCellPos());

        Vector2 pos = new Vector2(p.x+0.4f,p.y+0.62f);
        transform.position = pos;
        MapController.instance.SetPlayerCellPos(pos);
        wantTeleport = false;
    }
}
