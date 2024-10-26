using Unity.VisualScripting;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    void Awake()
    {
        instance = this;
    }

    public int movements = 0; // Player movements count 
    public (int,int) detritusSpawner; // How much detritus spawn, with A detritus spawning every B movements
    public (float,int) piecesSpawner; // Probability A for a piece to spawn every B movements
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    void Init()
    {
        detritusSpawner = (1,5);
        piecesSpawner = (0.5f, 5);
    }


    void FixedUpdate()
    {
        
    }

    public void Action()
    {
        movements++;
        if(movements%detritusSpawner.Item2==0)
        // Spawn Item1 detritus

        if(movements%piecesSpawner.Item2==0)
            if(Random.Range(0f, 1f) < piecesSpawner.Item1) 
                return;
                // Spawn a piece

        if(movements>30)
        {
            MapController.instance.Init(8);
        }
        else if(movements>40)
        {
            MapController.instance.Init(10);
        }
    }

    private void MapIncrease()
    {
    }
}
