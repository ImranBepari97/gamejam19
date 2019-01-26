using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : Interactable
{
    public float CleanUpTime;
    public int MessPoints;
    public ToolName RecieveBonusFrom;
    public float BonusMultiplier;
    public bool IsDestructable;    
    private PlayerInteract player;
    private GameManager GameManager;
    
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
        
        if (thisplayer.currentTool == RecieveBonusFrom && RecieveBonusFrom != ToolName.None)
        {
            CleanUpTime = CleanUpTime / BonusMultiplier;
          
        }
        //Player UI clean progress bar
        StartCoroutine(BeingCleaned());
        print("StartClean");
        player = thisplayer;
        thisplayer.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        thisplayer.gameObject.transform.GetChild(1).GetComponent<castbar>().Activated(CleanUpTime);
    }

    /// <summary>
    /// wait timer for cleaning. Will create / use a small timebar or similar if takes time.
    /// </summary>
    /// <returns></returns>
    IEnumerator BeingCleaned()
    {
        //lock player movement
        yield return new WaitForSeconds(CleanUpTime);
        print("EndClean");
        if (IsDestructable)
        {
            SpawnMoreMess();
        }
        else
        {
            GameManager.AddCleanScore(MessPoints);
        }
        player.FinishClean(); //reactivates player movement
        //add points to global score
        //stop clean progress bar
        
    }

    public void SpawnMoreMess()
    {
        transform.parent.GetChild(1).gameObject.SetActive(true);
        GameManager.AddMessScore(MessPoints);
    }
}
