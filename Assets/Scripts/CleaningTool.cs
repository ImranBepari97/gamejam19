using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolName
{
    None, Mop, Vaccuum, Mess, vase, bucket
}

[RequireComponent(typeof(Rigidbody))]
public class CleaningTool : Interactable
{
    public PlayerInteract player;
    public ToolName toolName;
    public AudioClip PickUpSfx;
    public AudioClip DropSfx;
    public AudioClip UseSfx;
    private SfxPlayer Sfx;
    Rigidbody rb;
    FixedJoint joint;
    private bool attached;

    // Start is called before the first frame update
    void Start()
    {
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
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
        Sfx.PlaySfx(PickUpSfx);
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
        Sfx.PlaySfx(DropSfx);
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
