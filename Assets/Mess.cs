using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : MonoBehaviour
{
    public float CleanUpTime;
    public float MessPoints;
    public Material glowing;
    public Material normal;
    private Renderer MyRender;
    private PlayerInteract player;
    
    // Start is called before the first frame update
    void Start()
    {
        MyRender = GetComponent<Renderer>();
        normal = MyRender.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Sets material of object to show is in range of player. Called by playerinteract script.
    /// </summary>
    public void PlayerInRange()
    {
        MyRender.material = glowing;
    }

    /// <summary>
    /// resets the object when player leaves range
    /// </summary>
    public void PlayerOutofRange() 
    {
        MyRender.material = normal;
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
