using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Tilemaps;
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

    public Sprite bombe;
    public Sprite dash;
    public Sprite tp;
    public Sprite croix;
    public Image detrituSprite;


    private int score = 0;
    private int power = 0;
    private int indexSlot = 0;
    private int choice_3 = 0;

    bool choix_1_impossible = false;
    bool choix_2_impossible = false;
    bool choix_3_impossible = false;

    private string chooseUpgradeState = "";

    private List<GameObject> list_slot = new List<GameObject>();
    private List<string> list_description = new List<string>() {
        "Description objet 1",
        "Description objet 2",
        "Description objet 3",
        "Description objet 4",
        "Description objet 5",
        "Description objet 6",
        "Description objet 7",
        "Description objet 8",
        "Description objet 9",
        "Description objet 10",
        "Description objet 11",
        "Description objet 12",
        "Description objet 13",
        "Description objet 14",
        "Description objet 15"
    };

    private void Start()
    {
        upgradesManager = GameObject.FindGameObjectWithTag("Player").GetComponent<UpgradesManager>();
        list_slot.Add(upgrade_slot_recyclage);
        list_slot.Add(upgrade_slot_spray);
        list_slot.Add(upgrade_slot_bombe);
        list_slot.Add(upgrade_slot_dash);
        list_slot.Add(upgrade_slot_tp);
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
            upgradesManager.ChangeLevel("Recycle");
            list_slot[0].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            choix_1_impossible = upgradesManager.recyclingLevel == 5;
        }
        else if (nbUpgrade == 2) // Spray
        {
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
                list_slot[2].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            if (upgradesManager.gadgetLevel == 2)
            {
                list_slot[3].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            if (upgradesManager.gadgetLevel == 5)
            {
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
        scoreText.text = $"Score : \n{score}\nBest : \nbestscore";
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
            info.text = list_description[upgradesManager.sprayLevel - 1];
        }
    }
    public void upgrade3_info_montre()
    {
        if (upgradesManager.gadgetLevel != 0)
        {
            info.text = list_description[upgradesManager.gadgetLevel - 1];
        }
    }
    public void upgrade4_info_montre()
    {
        if (upgradesManager.gadgetLevel != 0)
        {
            info.text = list_description[upgradesManager.gadgetLevel - 1];
        }
    }
    public void choice_1_info()
    {
        if (chooseUpgradeState == "choix")
        {
            info.text = list_description[1 - 1];
        }
    }
    public void choice_2_info()
    {
        if (chooseUpgradeState == "choix")
        {
            info.text = list_description[1 - 1];
        }
    }

    public void info_cache()
    {
        info.text = " ";
    }

    public void EraseDetrituInfo()
    {
        detrituInfo.text = "";
        detrituSprite.gameObject.SetActive(false);
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
        + (pieces==0 ? "" : $"and {pieces} pieces");
    }
    public void PrintDetrituMouseInfo((DetrituData,int,int) detrituData, int distance)
    {
        DetrituData detritu = detrituData.Item1;
        Tile tile = detritu.tiles[detrituData.Item3] as Tile;

        string infoText = "";
        if(detritu.type == "P")
            infoText = "Piece that can be recycled to repair you and improve your abilities";
        else
        {
            infoText = 
            $"Score points : {detritu.score}"
            + $"\nEnergy gain : {detritu.energy}"
            + $"\nDistance : " + (distance>0 ? $"{distance}" : $"Too far (need {-distance} more moves)")
            + $"\nNeeds {detrituData.Item2} more cleaning" + (detrituData.Item2==1 ? "" : "s");
        }

        detrituInfo.text = infoText;
        detrituSprite.gameObject.SetActive(true);
        detrituSprite.sprite = tile.sprite;
    }

    public void PrintMovements(int mov)
    {
        mvt.text = $"Actions taken :\n{mov}";
    }
}



