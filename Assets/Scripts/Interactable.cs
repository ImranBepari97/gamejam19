using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

	public Material normal;
	public Material glowing;
	private Renderer myRender;
    // Start is called before the first frame update
    public void Start()
    {
		myRender = GetComponent<Renderer>();
		normal = myRender.material;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

	/// <summary>
	/// Sets material of object to show is in range of player. Called by playerinteract script.
	/// </summary>
	public void PlayerInRange()
	{
		myRender.material = glowing;
	}

	/// <summary>
	/// resets the object when player leaves range
	/// </summary>
	public void PlayerOutofRange() 
	{
		myRender.material = normal;
	}

}
