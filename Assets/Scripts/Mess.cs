using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : Interactable
{
    public float CleanUpTime;
    public float MessPoints;
    
    
    private PlayerInteract player;
    
    // Start is called before the first frame update
    void Start()
    {
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
        //Player UI clean progress bar
        StartCoroutine(BeingCleaned());
        print("StartClean");
        player = thisplayer;

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
        player.FinishClean(); //reactivates player movement
        //add points to global score
        //stop clean progress bar
        
    }
}
