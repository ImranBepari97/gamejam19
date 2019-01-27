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
    private bool attached;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attached)
        {
            transform.position = player.transform.GetChild(0).position;
        }
        
    }

    public void AttachToPlayer(PlayerInteract activatingPlayer)
    {
        player = activatingPlayer;
        activatingPlayer.currentTool = this;
        attached = true;
      //  rb.position = player.transform.GetChild(0).position;
        //rb.rotation = Quaternion.Euler(rb.rotation.x , player.transform.rotation.y, rb.rotation.x);
       // StartCoroutine(MakeJoint());
        foreach (BoxCollider box in GetComponents<BoxCollider>()) box.enabled = false;
        activatingPlayer.HoldingTool();

    }

    public void DetachFromPlayer(PlayerInteract activatingPlayer)
    {
        player.currentTool = null;
        player = null;
        attached = false;
        if(joint != null)
        {
            Destroy(joint);
        }
        foreach (BoxCollider box in GetComponents<BoxCollider>()) box.enabled = true;
        rb.velocity = new Vector3(0,0,0);

    }

    IEnumerator MakeJoint()
    {
        yield return new WaitForSeconds(0.05f);
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = player.GetComponent<Rigidbody>();
    }
}
