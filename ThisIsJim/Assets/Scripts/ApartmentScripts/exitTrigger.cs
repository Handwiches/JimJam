using UnityEngine;
using System.Collections;

public class exitTrigger : MonoBehaviour {

	public Transform pointOfExit;
	//public string levelToLoad = "JimApartment_LR";
	// Use this for initialization
	public roomTrigger roomTrig;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other){
		
		if(other.tag == "Player"){
			//print ("RAN");
			other.GetComponent<NavMeshAgent>().enabled = false;
			//Application.LoadLevel (levelToLoad);
			other.transform.position = pointOfExit.position;
			other.GetComponent<NavMeshAgent>().enabled = true;

			other.GetComponent<jimControl2D>().jimActive = false; //for now, makes sure Jim doesn't get stuck when moving from one room
																//to the living room
			//problem is, Jim 's navmesh is getting confused when he enters a new room
			roomTrig.SendMessage ("enterNewRoom");
		}
	}
}
