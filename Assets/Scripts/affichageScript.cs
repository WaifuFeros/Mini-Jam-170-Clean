using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class affichageScript : MonoBehaviour
{
    private UpgradesManager upgradesManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI info;
    public TextMeshProUGUI mvt;
    public TextMeshProUGUI detrituInfo;

    public GameObject upgrade_slot_recyclage;
    public GameObject upgrade_slot_spray;
    public GameObject upgrade_slot_bombe;
    public GameObject upgrade_slot_dash;
    public GameObject upgrade_slot_tp;
    public GameObject upgrade_choose;
    public GameObject upgrade_choice_1;
    public GameObject upgrade_choice_2;
    public GameObject upgrade_choice_3;
    public GameObject panel;

    public GameObject objet_1_lvl;
    public GameObject objet_2_lvl;
    public GameObject objet_3_lvl;
    public GameObject objet_4_lvl;
    public GameObject objet_5_lvl;

    public Sprite upgrade_lvl_1;
    public Sprite upgrade_lvl_2;
    public Sprite upgrade_lvl_3;
    public Sprite upgrade_lvl_4;
    public Sprite upgrade_lvl_5;
    public Sprite bombe;
    public Sprite dash;
    public Sprite tp;
    public Sprite croix;


    private int score = 0;
    private int power = 0;
    private int indexSlot = 0;
    private int choice_3 = 0;

    bool choix_1_impossible = false;
    bool choix_2_impossible = false;
    bool choix_3_impossible = false;

    private string chooseUpgradeState = "";

    private List<GameObject> list_slot = new List<GameObject>();
    private List<GameObject> list_objet_lvl = new List<GameObject>();
    private List<Sprite> list_lvl = new List<Sprite>();
    private List<string> list_description = new List<string>() {
        "Recycler te donne + 1 d'�nergie",
        "Recycler ajoute la moiti� du score",
        "Recycler te donner + 3 d'�nergie",
        "Recycler ajoute la totalit� du score comme le nettoyage",
        "bras articul� (permet de recycler un d�chet autour du joueur)",
        "spray -> tir un projectile dans une direction au choix (1 power)",
        "strong spray -> tir un projectile dans une direction en faisant toute la ligne/colonne",
        "line spray -> pareil mais avec toute la ligne horizontale ou verticale",
        "cross spray -> pareil mais avec la ligne horizontal ET verticale",
        "combo spray -> pareil mais chaque item touch� REDECLENCHE une cross spray",
        "bombe (fais -1 � tous les objets sur l'�cran) : coute jsp combien de power",
        "propulseur (permet de Dash dans une direction jusqu'� rencontr� un mur",
        "stronger bombe -> fais plus de d�g�ts",
        "strong propulseur -> dash jusqu'� rencontrer un mur mais recycle tous les objets sur le chemin",
        "t�l�porteur (permet de se t�l�porter n'importe o�) ---> coute BCP de power"
    };

    private void Start()
    {
        upgradesManager = GameObject.FindGameObjectWithTag("Player").GetComponent<UpgradesManager>();
        list_slot.Add(upgrade_slot_recyclage);
        list_slot.Add(upgrade_slot_spray);
        list_slot.Add(upgrade_slot_bombe);
        list_slot.Add(upgrade_slot_dash);
        list_slot.Add(upgrade_slot_tp);
        list_lvl.Add(upgrade_lvl_1);
        list_lvl.Add(upgrade_lvl_2);
        list_lvl.Add(upgrade_lvl_3);
        list_lvl.Add(upgrade_lvl_4);
        list_lvl.Add(upgrade_lvl_5);
        list_objet_lvl.Add(objet_1_lvl);
        list_objet_lvl.Add(objet_2_lvl);
        list_objet_lvl.Add(objet_3_lvl);
        list_objet_lvl.Add(objet_4_lvl);
        list_objet_lvl.Add(objet_5_lvl);
        panel.SetActive(false);
        upgradesManager = GameObject.FindGameObjectWithTag("Player").GetComponent<UpgradesManager>();
    }


    private void Update()
    {
        if (chooseUpgradeState == "apparition")
        {
            panel.SetActive(true);
            if (upgrade_choose.transform.localScale.x < 11)
            {
                upgrade_choose.transform.localScale = new Vector3(
                    upgrade_choose.transform.localScale.x + 20 * Time.deltaTime,
                    upgrade_choose.transform.localScale.y + 20 * Time.deltaTime,
                    upgrade_choose.transform.localScale.z
                );
            }
            else
            {
                chooseUpgradeState = "choix";
            }
        }
        if (chooseUpgradeState == "choix")
        {
            if (upgrade_choice_1.GetComponent<Image>().color[3] == 0)
            {
                if (upgradesManager.gadgetLevel == 0 || upgradesManager.gadgetLevel == 2) { upgrade_choice_3.GetComponent<Image>().sprite = bombe; }
                if (upgradesManager.gadgetLevel == 1 || upgradesManager.gadgetLevel == 3) { upgrade_choice_3.GetComponent<Image>().sprite = dash; }
                if (upgradesManager.gadgetLevel == 4) { upgrade_choice_3.GetComponent<Image>().sprite = tp; }
            }
            if (upgrade_choice_3.GetComponent<Image>().color[3] <= 255)
            {
                upgrade_choice_1.GetComponent<Image>().color = new Color(255,255, 255, upgrade_choice_1.GetComponent<Image>().color[3]+ 20 * Time.deltaTime);
                upgrade_choice_2.GetComponent<Image>().color = new Color(255, 255, 255, upgrade_choice_2.GetComponent<Image>().color[3] + 20 * Time.deltaTime);
                upgrade_choice_3.GetComponent<Image>().color = new Color(255, 255, 255, upgrade_choice_3.GetComponent<Image>().color[3] + 20 * Time.deltaTime);
            }
        }
        if (chooseUpgradeState == "disparition")
        {
            upgrade_choice_1.GetComponent<Image>().color = new Color(255, 255, 255,0);
            upgrade_choice_2.GetComponent<Image>().color = new Color(255, 255, 255,0);
            upgrade_choice_3.GetComponent<Image>().color = new Color(255, 255, 255,0);
            info.text = " ";
            if (upgrade_choose.transform.localScale.x > 0)
            {
                upgrade_choose.transform.localScale = new Vector3(
                    upgrade_choose.transform.localScale.x - 20 * Time.deltaTime,
                    upgrade_choose.transform.localScale.y - 20 * Time.deltaTime,
                    upgrade_choose.transform.localScale.z
                );
            }
            else
            {
                upgrade_choose.transform.localScale = new Vector3(0,0,0);
                panel.SetActive(false);
                chooseUpgradeState = "";
            }
        }
    }

    public void chooseUpgrade()
    {
        if (!choix_1_impossible || !choix_2_impossible || !choix_3_impossible)
        {
            chooseUpgradeState = "apparition";
        }
    }
    public void choice1()
    {
        if (!choix_1_impossible)
        {
            chooseUpgradeState = "disparition";
            addUpgrade(1);
        }
    }
    public void choice2()
    {
        if (!choix_2_impossible)
        {
            chooseUpgradeState = "disparition";
            addUpgrade(2);
        }
    }
    public void choice3()
    {
        if (!choix_3_impossible)
        {
            chooseUpgradeState = "disparition";
            addUpgrade(3);
        }
    }


    public void addUpgrade(int nbUpgrade)
    {
        if (nbUpgrade == 1) // Recycle
        {
            list_objet_lvl[0].GetComponent<Image>().sprite = list_lvl[upgradesManager.recyclingLevel];
            list_objet_lvl[0].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            upgradesManager.ChangeLevel("Recycle");
            list_slot[0].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            choix_1_impossible = upgradesManager.recyclingLevel == 5;
        }
        else if (nbUpgrade == 2) // Spray
        {
            list_objet_lvl[1].GetComponent<Image>().sprite = list_lvl[upgradesManager.sprayLevel];
            list_objet_lvl[1].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            upgradesManager.ChangeLevel("Spray");
            list_slot[1].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            choix_2_impossible = upgradesManager.sprayLevel == 5;
        }
        else if (nbUpgrade == 3) // Gadget
        {
            upgradesManager.ChangeLevel("Gadget");
            choix_3_impossible = upgradesManager.gadgetLevel == 5;
            list_slot[2].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            if (upgradesManager.gadgetLevel == 1)
            {
                list_objet_lvl[2].GetComponent<Image>().sprite = list_lvl[0];
                list_objet_lvl[2].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                list_slot[2].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            if (upgradesManager.gadgetLevel == 2)
            {
                list_objet_lvl[3].GetComponent<Image>().sprite = list_lvl[0];
                list_objet_lvl[3].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                list_slot[3].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            if (upgradesManager.gadgetLevel == 3)
            {
                list_objet_lvl[2].GetComponent<Image>().sprite = list_lvl[1];
            }
            if (upgradesManager.gadgetLevel == 4)
            {
                list_objet_lvl[3].GetComponent<Image>().sprite = list_lvl[2];
            }
            if (upgradesManager.gadgetLevel == 5)
            {
                list_objet_lvl[4].GetComponent<Image>().sprite = list_lvl[0];
                list_objet_lvl[4].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                list_slot[4].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
        }
        if (choix_1_impossible) { upgrade_choice_1.GetComponent<Image>().sprite = croix; }
        if (choix_2_impossible) { upgrade_choice_2.GetComponent<Image>().sprite = croix; }
        if (choix_3_impossible) { upgrade_choice_3.GetComponent<Image>().sprite = croix; }

    }
    public void addScore(int addNb)
    {
        score += addNb;
        scoreText.text = "Score : " + score.ToString();
    }
    public void ChangePower(int power)
    {
        powerText.text = "Power Left : " + power.ToString();
    }
    public void upgrade1_info_montre()
    {
        if (upgradesManager.recyclingLevel != 0)
        {
            info.text = list_description[upgradesManager.recyclingLevel - 1];
        }
    }
    public void upgrade2_info_montre()
    {
        if (upgradesManager.sprayLevel != 0)
        {
            info.text = list_description[upgradesManager.sprayLevel + 4];
        }
    }
    public void upgrade3_info_montre()
    {
        if (upgradesManager.gadgetLevel >= 3)
        {
            info.text = list_description[12];
        }
        else if (upgradesManager.gadgetLevel >= 1)
        {
            info.text = list_description[10];
        }
    }
    public void upgrade4_info_montre()
    {
        if (upgradesManager.gadgetLevel >= 4)
        {
            info.text = list_description[13];
        }
        else if (upgradesManager .gadgetLevel >= 2)
        {
            info.text = list_description[11];
        }
    }
    public void upgrade5_info_montre()
    {
        if (upgradesManager.gadgetLevel == 5)
        {
            info.text = list_description[14];
        }
    }
    public void choice_1_info()
    {
        if (choix_1_impossible)
        {
            info.text = "This Upgrade is already maximized";
        }
        else
        {
            info.text = list_description[upgradesManager.recyclingLevel];
        }
    }
    public void choice_2_info()
    {
        if (choix_2_impossible)
        {
            info.text = "This Upgrade is already maximized";
        }
        else
        {
            info.text = list_description[upgradesManager.sprayLevel + 5];
        }
    }
    public void choice_3_info()
    {
        if (chooseUpgradeState == "choix")
        {
            if (choix_3_impossible)
            {
                info.text = "This Upgrade is already maximized";
            }
            else
            {
                info.text = list_description[upgradesManager.gadgetLevel + 10];
            }
        }
    }

    public void info_cache()
    {
        info.text = " ";
    }

    public void EraseDetrituInfo()
    {
        detrituInfo.text = "";
    }
    public void PrintDetrituRecycleInfo(Vector3Int[] pos)
    {
        int score = 0; int energy = 0; int pieces = 0;
        foreach (Vector3Int posInt in pos)
        {
            DetrituData data = MapController.instance.itemsGrid[posInt.x, posInt.y].Item1;
            score += data.score; 
            energy += data.energy; 
            if(data.type == "P")
                pieces++;
        }
        detrituInfo.text = $"Press \"Space\" to recycle.\nYou will gain {energy} power and {score} score"
        + (pieces==0 ? "" : $"\nand {pieces} piece");
        detrituSprite.gameObject.SetActive(true);
        detrituSprite.sprite = recycling;
    }
    public void PrintDetrituMouseInfo((DetrituData,int) detrituData, int distance)
    {
        DetrituData detritu = detrituData.Item1;
        Tile tile = detrituData.Item1.tiles[detrituData.Item3] as Tile;

        string infoText = "";
        if(detritu.type == "P")
            infoText = "Fragile piece that can be recycled to repair you and improve your abilities"
            + $"\nDistance : " + (distance>0 ? $"{distance}" : $"Too far (need {-distance} more moves)");
        else
        {
            infoText = $"{detritu.name}\nGive {detritu.energy} energy when recycled\nGive {detritu.score} score points when cleaned or recycled";
            infoText += $"\nIt needs {detrituData.Item2} cleanings to be fully cleaned";
        }

        string movesText = "";
        if(distance>0)
            movesText = $"{distance} moves are required to reach it";
        else if(distance<0)
            movesText = $"You need {-distance} more power to reach it";
        else
            movesText = "Press \"Space\" to recycle it";

        detrituInfo.text = infoText + "\n" + movesText;
<<<<<<< HEAD
        detrituSprite.gameObject.SetActive(true);
        detrituSprite.sprite = tile.sprite;
=======
        
>>>>>>> parent of 642bb6e (ahhh)
    }

    public void PrintMovements(int mov)
    {
        mvt.text = $"Movements : {mov}";
    }
}



