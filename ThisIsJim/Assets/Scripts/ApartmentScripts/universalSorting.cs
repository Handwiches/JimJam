using UnityEngine;
using System.Collections;

public class universalSorting : MonoBehaviour {

	private float xPosition; //move down as Z moves up
	private float zPosition;

	public float yOffset = 0.0f;
	public float zOffset = 0.0f;

	public Transform target; //if there is a target, the x and zPositions will be based on it

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			xPosition = transform.position.x;
			zPosition = transform.position.z;
			transform.position = new Vector3 (xPosition, -zPosition/5 + yOffset, zPosition + zOffset);
		} else {
			xPosition = target.position.x;
			zPosition = target.position.z;
			transform.position = new Vector3 (xPosition, -zPosition/5 + yOffset, zPosition + zOffset);
		}
	}
}
