using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAutoAdjust : MonoBehaviour {
	public List<GameObject> targets;

	//public Vector3 offset;
	Vector3 desiredPosition;
	Vector3 moveVelocity;

	public Camera mainCamera;


	public float cameraEdge, cameraMinimumSize;
	float cameraMoveSpeed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Move the camera to middle of all teh targets
		desiredPosition = GetAverageTargetPosition ();
		transform.position = Vector3.SmoothDamp(transform.position, GetAverageTargetPosition(), ref moveVelocity, 0.2f);

		//Zoom in and out fit everything in
		float requiredSize = RequiredSize();
		mainCamera.orthographicSize = Mathf.SmoothDamp (mainCamera.orthographicSize, requiredSize, ref cameraMoveSpeed, 0.2f);
	}


	Vector3 GetAverageTargetPosition() {
		//get the average player position
		Vector3 cameraCenter = Vector3.zero;
		foreach(GameObject target in targets){
			cameraCenter += target.transform.position;
		}

		Vector3 trueCenter = cameraCenter / targets.Count;
		//dont move on y
		trueCenter.y = transform.position.y;

		return trueCenter;
	}

	float RequiredSize() {
		//where the desired camera position is, relative to where the camera is now
		Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);
		float size = 0f;
		foreach (GameObject gameobj in targets) {
			//get the relative position of the target
			Vector3 positionRelativeToCamera = transform.InverseTransformPoint(gameobj.transform.position);
			//get the distance between where the camera wants to be and the targer
			Vector3 desiredPosToTarget = positionRelativeToCamera - desiredLocalPos;
			//set the size so that the bounds will be in range of the object
			size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
			size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / mainCamera.aspect);
		}
		//add the edge
		size += cameraEdge;
		//make sure its actually big enough
		size = Mathf.Max (size, cameraMinimumSize);

		return size;
	}
}
