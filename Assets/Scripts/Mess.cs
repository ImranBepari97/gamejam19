using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorTypes
{
    Tiled, Carpet
}

public class Mess : Interactable
{
    public float cleanUpTime;
    public int messPoints;
    public ToolName recieveBonusFrom;
    public float bonusMultiplier;
    public bool isDestructable;    
    private PlayerInteract player;
    private GameManager GameManager;
    public LayerMask WhatIsGround;
    public float TiledMultiplier;
    public float CarpetMultiplier;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    /// <summary>
    /// Called by player when interacting to start cleaning up this mess object.
    /// </summary>
    /// <param name="thisplayer"></param>
    public void CleanMess(PlayerInteract thisplayer)
    {
       // var hit = Physics.Raycast(transform.position, Vector3.down, WhatIsGround);
       


        if (thisplayer.currentTool != null)
        {
            if ((thisplayer.currentTool.toolName == recieveBonusFrom) && (recieveBonusFrom != ToolName.None))
            {
                if (bonusMultiplier == 0)
                {
                    bonusMultiplier = 2;
                }
                cleanUpTime = cleanUpTime / bonusMultiplier;

            }
        }
        //Player UI clean progress bar
        StartCoroutine(BeingCleaned());
        print("StartClean");
        player = thisplayer;
        thisplayer.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        thisplayer.gameObject.transform.GetChild(1).GetComponent<Castbar>().Activated(cleanUpTime);
    }

    /// <summary>
    /// wait timer for cleaning. Will create / use a small timebar or similar if takes time.
    /// </summary>
    /// <returns></returns>
    IEnumerator BeingCleaned()
    {
        //lock player movement
        yield return new WaitForSeconds(cleanUpTime);
        print("EndClean");
        if (isDestructable)
        {
            SpawnMoreMess();
        }
        else
        {
            GameManager.AddCleanScore(messPoints);
        }
        player.FinishClean(); //reactivates player movement
        //add points to global score
        //stop clean progress bar
        
    }

    public void SpawnMoreMess()
    {
        transform.parent.GetChild(1).gameObject.SetActive(true);
        transform.parent.GetChild(1).transform.position = new Vector3(transform.position.x, transform.parent.GetChild(1).transform.position.y, transform.position.z);
        GameManager.AddMessScore(messPoints);
    }
}
