using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	public int playerNum;
	public float speed;
	Rigidbody rb;
    public AudioClip Footsteps;
    private SfxPlayer Sfx;
    private bool PlayingFootstep;


    // Start is called before the first frame update
    void Start()
    {
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		rb.velocity = new Vector3(Input.GetAxis("Horizontal" + playerNum),0,Input.GetAxis("Vertical" + playerNum)) * speed;

		if(rb.velocity.magnitude != 0) 
		{
			rb.rotation = Quaternion.LookRotation(rb.velocity);
            if (!PlayingFootstep)
            {
                StartCoroutine(FootstepCD());
            }
		}
			

    }

    IEnumerator FootstepCD()
    {
        Sfx.PlaySfx(Footsteps);
        PlayingFootstep = true;
        yield return new WaitForSeconds(2);
        PlayingFootstep = false;
    }
}
