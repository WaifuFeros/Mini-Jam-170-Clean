using System.Collections.Generic;
using System.Linq;
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
    public Dictionary<string,(int,int)> detritusFrequences = new Dictionary<string,(int,int)>
    {
        {"SO",(1,5)}, {"SP",(0,4)},{"MO",(0,1)},{"MP",(0,1)},{"GO",(0,1)},{"GP",(0,1)}
    };
    public DetrituData[] detritus;
    public (float,int) piecesSpawner; // Probability A for a piece to spawn every B movements
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    void Init()
    {
        piecesSpawner = (0.5f, 5);
    }

    public void Action()
    {
        movements++;

        // Spawn detritu
        foreach(string type in detritusFrequences.Keys)
            if(movements%detritusFrequences[type].Item2==0)
                AddDetritus(type);

        if(movements%piecesSpawner.Item2==0)
            if(Random.Range(0f, 1f) < piecesSpawner.Item1) 
                return;
                // Spawn a piece
        
        DifficultyEvolution();
    }

    private void DifficultyEvolution()
    {
        // if(movements%20==0)
        // {
        //     detritusSpawner.Item1++;
        // }

        // if(movements%30==0 && detritusSpawner.Item2>1)
        // {
        //     detritusSpawner.Item2--;
        // }
    }

    private void AddDetritus(string type)
    {
        for(int i =0; i < detritusFrequences[type].Item1; i++)
        {
            MapController.instance.detritus.GetComponent<DetritusApparition>().Apparition(type);
        }
    }
}
