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
    public GameObject upgrade_slot_1;
    public GameObject upgrade_slot_2;
    public GameObject upgrade_slot_3;
    public GameObject upgrade_slot_4;
    public GameObject upgrade_choose;
    public GameObject upgrade_choice_1;
    public GameObject upgrade_choice_2;
    public GameObject panel;

    public Sprite upgrade_texture_1;
    public Sprite upgrade_texture_2;
    public Sprite upgrade_texture_3;
    public Sprite upgrade_texture_4;
    public Sprite upgrade_texture_5;
    public Sprite upgrade_texture_6;

    private int score = 0;
    private int power = 0;
    private int indexSlot = 0;
    private int choice_1 = 0;
    private int choice_2 = 0;

    private string chooseUpgradeState = "";

    private List<GameObject> list_slot = new List<GameObject>();
    private List<Sprite> list_texture = new List<Sprite>();
    private List<string> list_description = new List<string>() {
        "Description objet 1",
        "Description objet 2",
        "Description objet 3",
        "Description objet 4",
        "Description objet 5",
        "Description objet 6",
    };
    private List<int> upgrade_obtenue = new List<int>() {0,0,0,0};  // 0 signifie qu'il n'y a pas d'upgrade

    private void Start()
    {
        
        list_slot.Add(upgrade_slot_1);
        list_slot.Add(upgrade_slot_2);
        list_slot.Add(upgrade_slot_3);
        list_slot.Add(upgrade_slot_4);
        list_texture.Add(upgrade_texture_1);
        list_texture.Add(upgrade_texture_2);
        list_texture.Add(upgrade_texture_3);
        list_texture.Add(upgrade_texture_4);
        list_texture.Add(upgrade_texture_5);
        list_texture.Add(upgrade_texture_6);
        
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
                List<int> list_choice = new List<int>() {1,2,3,4,5,6};
                foreach (int upgrade in upgrade_obtenue)
                {
                    if (upgrade != 0)
                    {
                        list_choice.Remove(upgrade);
                    }
                }
                choice_1 = list_choice[Random.Range(0,list_choice.Count)];
                list_choice.Remove(choice_1);
                choice_2 = list_choice[Random.Range(0, list_choice.Count)];
                upgrade_choice_1.GetComponent<Image>().sprite = list_texture[choice_1 - 1];
                upgrade_choice_2.GetComponent<Image>().sprite = list_texture[choice_2 - 1];

            }
            if (upgrade_choice_1.GetComponent<Image>().color[3] <= 255)
            {
                upgrade_choice_1.GetComponent<Image>().color = new Color(255,255, 255, upgrade_choice_1.GetComponent<Image>().color[3]+ 20 * Time.deltaTime);
                upgrade_choice_2.GetComponent<Image>().color = new Color(255, 255, 255, upgrade_choice_2.GetComponent<Image>().color[3] + 20 * Time.deltaTime);
            }
        }
        if (chooseUpgradeState == "disparition")
        {
            upgrade_choice_1.GetComponent<Image>().color = new Color(255, 255, 255,0);
            upgrade_choice_2.GetComponent<Image>().color = new Color(255, 255, 255,0);
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
        addUpgrade(choice_1);
    }
    public void choice2()
    {
        chooseUpgradeState = "disparition";
        addUpgrade(choice_2);
    }


    public void addUpgrade(int nbUpgrade)
    {
        if (upgrade_obtenue[0] == 0)
        {
            indexSlot = 0;
        }
        else if (upgrade_obtenue[1] == 0)
        {
            indexSlot = 1;
        }
        else if (upgrade_obtenue[2] == 0)
        {
            indexSlot = 2;
        }
        else if (upgrade_obtenue[3] == 0)
        {
            indexSlot = 3;
        }
        else 
        {
            indexSlot = 4;
        }

        if (indexSlot != 4)
        {
            upgrade_obtenue[indexSlot] = nbUpgrade;
            list_slot[indexSlot].GetComponent<Image>().sprite = list_texture[nbUpgrade-1];
            list_slot[indexSlot].GetComponent<Image>().color = new Color(255, 255, 255, 255);
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
    void removePower(int removeNb)
    {
        power -= removeNb;
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
            info.text = list_description[choice_1 - 1];
        }
    }
    public void choice_2_info()
    {
        if (chooseUpgradeState == "choix")
        {
            info.text = list_description[choice_2 - 1];
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
}



