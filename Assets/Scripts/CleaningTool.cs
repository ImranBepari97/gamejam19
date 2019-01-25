using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolName
{
    None, Mop, Vaccuum
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
        print("SET TOOL");
        activatingPlayer.currentTool = toolName;
        print("SET TOOL1");
        rb.position = player.transform.GetChild(0).position;
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = player.GetComponent<Rigidbody>();

    }

    public void DetachFromPlayer(PlayerInteract activatingPlayer)
    {
        player.currentTool = ToolName.None;
        player = null;

        if(joint != null)
        {
            Destroy(joint);
        }
        
    }
}
