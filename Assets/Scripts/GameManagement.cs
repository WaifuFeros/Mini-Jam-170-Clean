using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    private affichageScript ui;
    public int movements = 0; // Player movements count 
    void Awake()
    {
        instance = this;
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<affichageScript>();
        ui.PrintMovements(movements);
        ui.addScore(0);
    }

    public Dictionary<string,(int,int)> itemFrequences = new Dictionary<string,(int,int)>
    {
        // Frequences : "String" item appears Item1 times every Item2 movements
        {"SO",(1,5)}, {"SP",(1,4)},{"MO",(0,8)},{"MP",(0,8)},{"GO",(1,50)},{"GP",(1,50)},{"P",(1,15)},

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

        if(movements==1)
        {
            AddDetritus("SO");
        }

        for(int y=0; y<MapController.instance.itemsGrid.GetLength(1); y++)
            for(int x=0; x<MapController.instance.itemsGrid.GetLength(0); x++)    
            {
                if(MapController.instance.itemsGrid[x,y].Item1 != null && MapController.instance.itemsGrid[x,y].Item1.type == "BEBER")
                    MapController.instance.Cleaned(new Vector3Int(x,y),1);
            }

        DifficultyEvolution();

        // Spawn detritu
        foreach(string type in itemFrequences.Keys)
            if(movements%itemFrequences[type].Item2==0)
                AddDetritus(type);
        
    }

    private void DifficultyEvolution()
    {
        if(movements%20==0) // Every 20 movements
        {
            if(itemFrequences["SO"].Item1<5)
                itemFrequences["SO"] = (itemFrequences["SO"].Item1+1,itemFrequences["SO"].Item2);
            if(itemFrequences["SP"].Item1<5)
                itemFrequences["SP"] = (itemFrequences["SP"].Item1+1,itemFrequences["SP"].Item2);
            
            if(itemFrequences["MO"].Item1<5)
                itemFrequences["MO"] = (itemFrequences["MO"].Item1+1,itemFrequences["MO"].Item2);
            if(itemFrequences["MP"].Item1<5)
                itemFrequences["MP"] = (itemFrequences["MP"].Item1+1,itemFrequences["MP"].Item2);
        }
        if(movements%30==0) // Every 30 movements 
        {
            if(itemFrequences["SO"].Item2>1)
                itemFrequences["SO"] = (itemFrequences["SO"].Item1,itemFrequences["SO"].Item2-1);
            if(itemFrequences["SP"].Item2>1)
                itemFrequences["SP"] = (itemFrequences["SP"].Item1,itemFrequences["SP"].Item2-1);
            
            if(itemFrequences["MO"].Item2>1)
                itemFrequences["MO"] = (itemFrequences["MO"].Item1,itemFrequences["MO"].Item2-1);
            if(itemFrequences["MP"].Item2>1)
                itemFrequences["MP"] = (itemFrequences["MP"].Item1,itemFrequences["MP"].Item2-1);

            if(itemFrequences["P"].Item2>3)
                itemFrequences["P"] = (itemFrequences["P"].Item1,itemFrequences["MP"].Item2-3);
        }
        if(movements%50==0) // Every 50 movements
        {
            // if(itemFrequences["GO"].Item2>1)
        }

        if(movements==50) // Map increase to a 8 length
        {
            Debug.Log("5");
            MapController.instance.MapInit(8);
        }
        if(movements==100) // Map increase to a 10 length
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
