using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolName
{
    None, Mop, Vaccuum, Mess
}

[RequireComponent(typeof(Rigidbody))]
public class CleaningTool : Interactable
{
    public PlayerInteract player;
    public ToolName toolName;

    Rigidbody rb;
    FixedJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachToPlayer(PlayerInteract activatingPlayer)
    {
        player = activatingPlayer;
        activatingPlayer.currentTool = this;
        rb.position = player.transform.GetChild(0).position;
        //rb.rotation = Quaternion.Euler(rb.rotation.x , player.transform.rotation.y, rb.rotation.x);
        StartCoroutine(MakeJoint());
       

    }

    public void DetachFromPlayer(PlayerInteract activatingPlayer)
    {
        player.currentTool = null;
        player = null;

        if(joint != null)
        {
            Destroy(joint);
        }

        rb.velocity = new Vector3(0,0,0);

    }

    IEnumerator MakeJoint()
    {
        yield return new WaitForSeconds(0.05f);
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = player.GetComponent<Rigidbody>();
    }
}
