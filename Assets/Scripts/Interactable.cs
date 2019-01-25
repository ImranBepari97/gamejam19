using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

	public Material normal;
	public Material glowing;
	private Renderer MyRender;
    // Start is called before the first frame update
    public void Start()
    {
		MyRender = GetComponent<Renderer>();
		normal = MyRender.material;
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
		MyRender.material = glowing;
	}

	/// <summary>
	/// resets the object when player leaves range
	/// </summary>
	public void PlayerOutofRange() 
	{
		MyRender.material = normal;
	}

}
