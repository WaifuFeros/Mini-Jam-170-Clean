using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    public affichageScript ui;
    void Awake()
    {
        instance = this;
    }

    public int movements = 0; // Player movements count 
    public Dictionary<string,(int,int)> itemFrequences = new Dictionary<string,(int,int)>
    {
        // Frequences : "String" item appears Item1 times every Item2 movements
        {"SO",(1,5)}, {"SP",(1,4)},{"MO",(0,1)},{"MP",(0,1)},{"GO",(0,1)},{"GP",(0,1)},{"P",(1,15)},

        // Special apparition
        {"BEBER",(3,10)} // Beber has 3% chance to appears every 10 movements

    };

    public DetrituData[] detritus;
    void Start()
    {
        Init();
    }

    void Init()
    {

    }

    public void Action()
    {
        movements++;
        ui.PrintMovements(movements);
        DifficultyEvolution();

        // Spawn detritu
        foreach(string type in itemFrequences.Keys)
            if(movements%itemFrequences[type].Item2==0)
                AddDetritus(type);
        
    }

    private void DifficultyEvolution()
    {
        if(movements==50)
        {
            Debug.Log("5");
            MapController.instance.MapInit(8);
        }
        if(movements==100)
        {

            MapController.instance.MapInit(10);
        }
    }

    private void AddDetritus(string type)
    {
        if(type=="BEBER")
        {
            if(itemFrequences[type].Item1 > Random.Range(0,100))
                MapController.instance.detritus.GetComponent<DetritusApparition>().Apparition(type);
        }
        else
        {
            for(int i =0; i < itemFrequences[type].Item1; i++)
            {
                MapController.instance.detritus.GetComponent<DetritusApparition>().Apparition(type);
            }
        }
    }
}
