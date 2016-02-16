using UnityEngine;
using System.Collections;

public class cameraMain : MonoBehaviour {

	public Transform target;
	public Vector2 xMinMax = new Vector2 (-5, 5);
	public Vector2 zMinMax = new Vector2 (-5, 5);

	public float dampTime = 0.15f;


	// Use this for initialization
	void Start () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 destination = Vector3.Lerp (transform.position, target.position, Time.deltaTime);
		Vector3 newPosition;
		newPosition = new Vector3 (destination.x, transform.position.y, destination.z);
		Vector3 finalPosition;
		finalPosition.x = Mathf.Clamp (newPosition.x, xMinMax.x, xMinMax.y);
		finalPosition.z = Mathf.Clamp (newPosition.z, zMinMax.x, zMinMax.y);

		transform.position = new Vector3 (finalPosition.x, transform.position.y, finalPosition.z);
	}
}
