using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject CurrentInteractObject; //what object are we interacting with
    private PlayerMovement playermovement; //to lock movement
    private bool IsCleaning; //to avoid interacting multiple times on same object
    // Start is called before the first frame update
    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !IsCleaning) //will need to change "Fire1" dependant on player number
        {
            if (CurrentInteractObject != null) //if interacting object within range
            {
                playermovement.enabled = false; //deactivate movement
                IsCleaning = true;
                CurrentInteractObject.GetComponent<Mess>().CleanMess(this); //let object know its cleaning
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //zero out velocity
            }
        }
    }

    /// <summary>
    /// Called by an interact object when it has finished being cleaned
    /// </summary>
    public void FinishClean()
    {
        CurrentInteractObject.SetActive(false); //hide object. This line may change when object is coded more
        CurrentInteractObject = null; //clear current interact object to detect new one
        playermovement.enabled = true; //enable movement
        IsCleaning = false; //player is not cleaning up anymore
    }


    private void OnTriggerStay(Collider other) //using on stay rather than enter to get other objects in range when one is finished interacting with
    {
        if (other.gameObject.CompareTag("Interactable")) 
        {
            if (CurrentInteractObject == null)
            {
                CurrentInteractObject = other.gameObject;
                CurrentInteractObject.GetComponent<Mess>().PlayerInRange();
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject == CurrentInteractObject) //if no longer in range of object 
        {
            CurrentInteractObject.GetComponent<Mess>().PlayerOutofRange(); //reset object interact settings
            CurrentInteractObject = null; //clear current object
        }
    }


}
