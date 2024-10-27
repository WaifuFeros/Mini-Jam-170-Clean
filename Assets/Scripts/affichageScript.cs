using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class affichageScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI info;
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
    private List<int> upgrade_obtenue = new List<int>() {0,0,0,0,0};  // 0 signifie qu'il n'y a pas d'upgrade

    private void Start()
    {
        list_slot.Add(upgrade_slot_recyclage);
        list_slot.Add(upgrade_slot_spray);
        list_slot.Add(upgrade_slot_bombe);
        list_slot.Add(upgrade_slot_dash);
        list_slot.Add(upgrade_slot_tp);
        panel.SetActive(false);
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
                List<int> list_choice = new List<int>() {1,2,3};
                if (upgrade_obtenue[2] == 2)
                {
                    list_choice.Remove(1);
                }
                if (upgrade_obtenue[3] == 2)
                {
                    list_choice.Remove(2);
                }
                if (upgrade_obtenue[4] == 1)
                {
                    list_choice.Remove(3);
                }
                if (list_choice.Count == 0)
                {
                    choix_3_impossible = true;
                }
                if (!choix_3_impossible)
                {
                    choice_3 = list_choice[Random.Range(0, list_choice.Count)]+2;
                    if (choice_3 == 3) { upgrade_choice_3.GetComponent<Image>().sprite = bombe; }
                    if (choice_3 == 4) { upgrade_choice_3.GetComponent<Image>().sprite = dash; }
                    if (choice_3 == 5) { upgrade_choice_3.GetComponent<Image>().sprite = tp; }
                }
                

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
        chooseUpgradeState = "apparition";
    }

    public void choice1()
    {
        chooseUpgradeState = "disparition";
        addUpgrade(1);
    }
    public void choice2()
    {
        chooseUpgradeState = "disparition";
        addUpgrade(2);
    }
    public void choice3()
    {
        chooseUpgradeState = "disparition";
        addUpgrade(choice_3);
    }


    public void addUpgrade(int nbUpgrade)
    {
        if (nbUpgrade == 1 && nbUpgrade<5)
        {
            upgrade_obtenue[nbUpgrade - 1] += 1; 
            list_slot[0].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else if (nbUpgrade == 2 && nbUpgrade < 5)
        {
            upgrade_obtenue[nbUpgrade - 1] += 1;
            list_slot[1].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else if (nbUpgrade == 3 && nbUpgrade < 2)
        {
            upgrade_obtenue[nbUpgrade - 1] += 1;
            list_slot[2].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else if (nbUpgrade == 4 && nbUpgrade < 2)
        {
            upgrade_obtenue[nbUpgrade - 1] += 1;
            list_slot[3].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else if (nbUpgrade == 5 && nbUpgrade < 1)
        {
            upgrade_obtenue[nbUpgrade - 1] += 1;
            list_slot[4].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }

    }
    public void removeUpgrade(int slotUpgrade)
    {
        upgrade_obtenue[slotUpgrade - 1] = 0;
        list_slot[slotUpgrade - 1].GetComponent<Image>().color = new Color(255, 255, 255, 0);
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
        if (upgrade_obtenue[0] != 0)
        {
            info.text = list_description[upgrade_obtenue[0] - 1];
        }
    }
    public void upgrade2_info_montre()
    {
        if (upgrade_obtenue[1] != 0)
        {
            info.text = list_description[upgrade_obtenue[1] - 1];
        }
    }
    public void upgrade3_info_montre()
    {
        if (upgrade_obtenue[2] != 0)
        {
            info.text = list_description[upgrade_obtenue[2] - 1];
        }
    }
    public void upgrade4_info_montre()
    {
        if (upgrade_obtenue[3] != 0)
        {
            info.text = list_description[upgrade_obtenue[3] - 1];
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
    public void PrintDetrituMouseInfo((DetrituData,int) detrituData, int distance)
    {
        DetrituData detritu = detrituData.Item1;

        string infoText = "";
        if(detritu.type == "P")
            infoText = "Piece that can be recycled to repair you and improve your abilities";
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
        
    }

    public void PrintMovements(int mov)
    {
        mvt.text = $"Movements : {mov}";
    }
}



