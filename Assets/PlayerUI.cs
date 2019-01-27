using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Image ToolImage;
    public Sprite Mop;
    public Sprite Vacuum;
    public Sprite Mess;
    public Sprite bucket;
    public Sprite vase;
    public int PlayerNumber;
    //public Sprite Empty;
    // Start is called before the first frame update
    void Awake()
    {
        bool assigned = false;
        ToolImage = transform.GetChild(4).GetComponent<Image>();
        EquipItem(ToolName.None);
        transform.GetChild(1).GetComponent<Text>().text = "Player " + PlayerNumber;
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].playerNum == PlayerNumber)
            {
                players[i].GetComponent<PlayerInteract>().MyUI = this;
                assigned = true;
                return;
            }
            
        }

        if (!assigned)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Actiaved(bool IsCleaner)
    {
        //if (IsCleaner)
        //{
        //    transform.GetChild(0).GetComponent<Image>().color = Color.blue;
        //}
        //else
        //{
        //    transform.GetChild(0).GetComponent<Image>().color = Color.red;
        //}

    }

    public void EquipItem(ToolName tool)
    {
        ToolImage.color = Color.white;
        switch (tool)
        {
            case (ToolName.None):
                Color newcolor = ToolImage.color;
                newcolor.a = 0;
                ToolImage.color = newcolor;
                ToolImage.sprite = null;
                break;
            case (ToolName.Mess):
                ToolImage.sprite = Mess;
                break;
            case (ToolName.Mop):
                ToolImage.sprite = Mop;
                break;
            case (ToolName.Vaccuum):
                ToolImage.sprite = Vacuum;
                break;
            case (ToolName.vase):
                ToolImage.sprite = vase;
                break;
            case (ToolName.bucket):
                ToolImage.sprite = bucket;
                break;
        };
    }
}
