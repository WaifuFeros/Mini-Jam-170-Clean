using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Collections.Generic;
public class affichageScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerText;

    public GameObject upgrade_slot_1;
    public GameObject upgrade_slot_2;
    public GameObject upgrade_slot_3;
    public GameObject upgrade_slot_4;

    public Sprite upgrade_texture_1;
    public Sprite upgrade_texture_2;
    public Sprite upgrade_texture_3;
    public Sprite upgrade_texture_4;
    public Sprite upgrade_texture_5;
    public Sprite upgrade_texture_6;

    private int score = 0;
    private int power = 0;
    private int indexSlot = 0;

    private List<GameObject> list_slot = new List<GameObject>();
    private List<Sprite> list_texture = new List<Sprite>();


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
        /*
        addUpgrade(4); 
        addUpgrade(5);
        addUpgrade(1);
        addUpgrade(2);
        addUpgrade(6);
        */
    }




    void addUpgrade(int nbUpgrade)
    {
        if (indexSlot < 4)
        {
            list_slot[indexSlot].GetComponent<Image>().sprite = list_texture[nbUpgrade-1];
            list_slot[indexSlot].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            indexSlot++;
        }
    }
    void addScore(int addNb)
    {
        score += addNb;
        scoreText.text = "Score : " + score.ToString();
    }
    void addPower(int addNb)
    {
        power += addNb;
        powerText.text = "Power Left : " + power.ToString();
    }
    void removePower(int removeNb)
    {
        power -= removeNb;
        powerText.text = "Power Left : " + power.ToString();
    }
}



