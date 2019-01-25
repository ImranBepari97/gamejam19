using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public int playerNum;
	public float speed;
	Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		rb.velocity = new Vector3(Input.GetAxis("Horizontal" + playerNum),0,Input.GetAxis("Vertical" + playerNum)) * speed;

		if(rb.velocity.magnitude != 0) 
		{
			rb.rotation = Quaternion.LookRotation(rb.velocity);
		}
			

    }
}
