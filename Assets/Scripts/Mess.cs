using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorTypes
{
    Tiled, Carpet
}

public class Mess : Interactable
{
    public AudioClip CleanMessEndSfx;
    public AudioClip CleaningSfx;
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
    public bool OnSide;
    public bool OnOtherSide;
    private SfxPlayer Sfx;
    
    // Start is called before the first frame update
    void Start()
    {
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		base.Start();
        if (TiledMultiplier == 0) TiledMultiplier = 1;
        if (CarpetMultiplier == 0) CarpetMultiplier = 1;
        if (bonusMultiplier == 0) bonusMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (OnSide)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 50, Color.yellow);
        }
        else if (OnOtherSide)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 50, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 50, Color.yellow);
        }

    }
    

    /// <summary>
    /// Called by player when interacting to start cleaning up this mess object.
    /// </summary>
    /// <param name="thisplayer"></param>
    public void CleanMess(PlayerInteract thisplayer)
    {
        RaycastHit hit;
       // print("ray");
        if (OnSide)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, WhatIsGround))
            {
                if (hit.transform.gameObject.CompareTag("Carpet"))
                {
                    print("Carpet");
                    cleanUpTime = cleanUpTime / CarpetMultiplier;
                    
                }
                if (hit.transform.gameObject.CompareTag("Tiled"))
                {
                    print("Tiled");
                    cleanUpTime = cleanUpTime / TiledMultiplier;
                }
            }
        }
        else if (OnOtherSide)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, WhatIsGround))
            {
                if (hit.transform.gameObject.CompareTag("Carpet"))
                {
                    print("Carpet");
                    cleanUpTime = cleanUpTime / CarpetMultiplier;

                }
                if (hit.transform.gameObject.CompareTag("Tiled"))
                {
                    print("Tiled");
                    cleanUpTime = cleanUpTime / TiledMultiplier;
                }
            }
        }
        else
        {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, WhatIsGround))
                {
                    
                if (hit.transform.gameObject.CompareTag("Carpet"))
                {
                    print("Carpet");
                }
                if (hit.transform.gameObject.CompareTag("Tiled"))
                {
                    print("Tiled");
                }
            }
        }
        
       


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
        Sfx.PlaySfx(CleaningSfx);
        if (player.currentTool != null)
        {
            Sfx.PlaySfx(player.currentTool.UseSfx);
        }
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
        player.FinishClean(messPoints); //reactivates player movement
        Sfx.PlaySfx(CleanMessEndSfx);
        
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
