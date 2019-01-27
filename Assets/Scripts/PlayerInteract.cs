using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInteractObject; //what object are we interacting with
    private PlayerMovement playerMovement; //to lock movement
    private bool isCleaning; //to avoid interacting multiple times on same object
    // Start is called before the first frame update
    public CleaningTool currentTool;
    public bool cleaningPlayer;
    public PlayerUI MyUI;
    private FloatingNumbers FloatNums;
    //public int PlayerNumber;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (MyUI != null)
        {
            MyUI.Actiaved(cleaningPlayer);
        }
        FloatNums = GameObject.Find("FloatNumberCanvas").GetComponent<FloatingNumbers>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCleaning)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        if (Input.GetButtonDown("Use" + playerMovement.playerNum) && !isCleaning) //will need to change "Fire1" dependant on player number
        {
            if (currentInteractObject == null)
            {
                return;
            }
            if (cleaningPlayer && currentInteractObject.CompareTag("MessPlayer"))
            {
                return;
            }
            if (!cleaningPlayer && currentInteractObject.CompareTag("CleanPlayer"))
            {
                return;
            }

            if (currentInteractObject != null && currentInteractObject.GetComponent<Mess>()) //if interacting object within range
            {
                if (currentInteractObject.GetComponent<Mess>().IsBeingCleaned)
                {
                    currentInteractObject = null;
                    return;

                }
                playerMovement.enabled = false; //deactivate movement
                isCleaning = true;
                currentInteractObject.GetComponent<Mess>().CleanMess(this); //let object know its cleaning
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //zero out velocity
            }
        }

		//pick up an item like a mop
		if(Input.GetButtonDown("Pickup" + playerMovement.playerNum) && !isCleaning) 
		{
			if (currentInteractObject != null && currentTool == null) //if interacting object within range
			{
				if(currentInteractObject.GetComponent<CleaningTool>())
                {
                    currentInteractObject.GetComponent<CleaningTool>().AttachToPlayer(this);
                }
			} else if(currentTool != null)
            {
                currentTool.DetachFromPlayer(this);
                currentTool = null;

            }
            if (MyUI != null)
            {
                if (currentTool == null)
                {
                    MyUI.EquipItem(ToolName.None);
                    return;
                }
                MyUI.EquipItem(currentTool.toolName);
            }
        }
    }

    /// <summary>
    /// Called by an interact object when it has finished being cleaned
    /// </summary>
    public void FinishClean(int points)
    {
        FloatNums.RequestNumber(points, this.gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(points, this.gameObject);
        currentInteractObject.SetActive(false); //hide object. This line may change when object is coded more
        if (currentTool != null)
        {
        if (currentTool.toolName == ToolName.Mess)
            {
                MyUI.EquipItem(ToolName.None);
            }
        }
        currentInteractObject = null; //clear current interact object to detect new one
        playerMovement.enabled = true; //enable movement
        isCleaning = false; //player is not cleaning up anymore
    }

    private void OnTriggerStay(Collider other) //using on stay rather than enter to get other objects in range when one is finished interacting with
    {

        //if (cleaningPlayer && other.gameObject.CompareTag("MessPlayer"))
        //{
        //    return;
        //}
        //if (!cleaningPlayer && other.gameObject.CompareTag("CleanPlayer"))
        //{
        //    return;
        //}
		Interactable inter;
		if (inter = other.gameObject.GetComponent<Interactable>()) {
            if (currentInteractObject == null)
            {
                //if youre a cleaning tool and in use, DONT GLOW
                CleaningTool tool;
                if (tool = other.gameObject.GetComponent<CleaningTool>())
                {
                    if(tool.player != null)
                    {
                        return;
                    }
                }
                else
                {
                  if (cleaningPlayer && other.gameObject.CompareTag("MessPlayer"))
                    {
                        return;
                    }
                    if (!cleaningPlayer && other.gameObject.CompareTag("CleanPlayer"))
                    {
                        return;
                    }
                }
                
                currentInteractObject = other.gameObject;
                inter.PlayerInRange();
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject == currentInteractObject && !isCleaning) //if no longer in range of object 
        {
            currentInteractObject.GetComponent<Interactable>().PlayerOutofRange(); //reset object interact settings
            currentInteractObject = null; //clear current object
        }
    }

    public void HoldingTool()
    {
        currentInteractObject.GetComponent<Interactable>().PlayerOutofRange(); //reset object interact settings
        currentInteractObject = null;
    }

}
