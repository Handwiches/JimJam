using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
	 Jim Navigation works like AI. There needs to be points of interest within an area that Jim can randomly grab and.
	 	play animations to accordingly.
	 	
 	Jim needs a set time without player input before he starts moving and deciding picking his own actions.

 */

public class jimControl2D : MonoBehaviour {

	//MOVEMENT Variables:
	public NavMeshAgent agent;
	public Animator anim;
	public SpriteRenderer graphicHolder;

	public float inactiveTimer = 0.0f;
	float initialInactiveTimerLimit = 8.0f;
	public float inactiveTimerLimit = 3.0f;
	public Vector2 inactiveTimerMinMax = new Vector2(2.0f, 5.0f);
	public bool sentient = false;
	public float walkRadius = 5.0f; //radius in which Jim will randomly walk

	public GameObject narratorBox;
	public Text narratorBoxText;
	public float typeTimer = 1.5f; //how long it takes for the text to type out
	GameObject[] interactableObjects;
	jimSentientIdles jimIdles; //Updated from RandomSuggestion RandomIdle
	public bool jimActive = false; //means Jim is sentient, or in the middle of something, the exitTrigger script is currently affecting this
	RaycastHit hit;
	Ray ray;

	bool trackingToObject = false; //check to see if Jim is still heading towards the player's intended interaction

	void Awake (){
		initialInactiveTimerLimit = inactiveTimerLimit;
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		jimIdles = GetComponent<jimSentientIdles> ();
		interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
		narratorBoxText = narratorBox.GetComponent<Text>();
		narratorBox.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, 0f); //should activate collision check every frame, when not moving
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//CLICK
		if (Input.GetMouseButtonDown(0) && jimActive == false) {
			trackingToObject = false; //resets
			jimActive = false;
			if (Physics.Raycast(ray, out hit, 100)) {
				agent.destination = hit.point;
			}
			if (hit.collider.gameObject.tag == "Interactable") {
				//agent.destination = hit.collider.GetComponent<interactableObject>().pointOfInterest.position;
				print ("hit interactable object");
				StartCoroutine (moveToObject (hit.collider));
			}
		}

		//HOVER
		if (Physics.Raycast(ray, out hit, 100)){
			if (hit.collider.gameObject.tag == "Interactable") {
				hit.collider.gameObject.SendMessage ("Hover");
			}
		}

		//INACTIVE JIM
		if (Input.anyKeyDown) {
			inactiveTimer = 0.0f;
			sentient = true;
			inactiveTimerLimit = initialInactiveTimerLimit;
		} else {
			inactiveTimer += 1.0f * Time.deltaTime; 
		}

		if (jimActive) {
			inactiveTimer = 0.0f;
		} else {
			narratorBox.SetActive (false);
		}

		if (sentient == true) {
			if (inactiveTimer >= inactiveTimerLimit) {
				inactiveTimer = 0.0f;
				sentient = true;
				RandomSuggestion ();
			}
		}
		if (sentient == false) {
			if (inactiveTimer >= inactiveTimerLimit) {
				inactiveTimer = 0.0f;
				RandomSuggestion ();
			}
		}



		//print ("Speed: " + agent.desiredVelocity);
		//MOVEMENT:
		
		if (agent.desiredVelocity != Vector3.zero) {
			anim.SetBool ("walking", true);
		}
		if (agent.desiredVelocity == Vector3.zero) {
			anim.SetBool ("walking", false);
		}
		if (agent.desiredVelocity.x < -1.0f) {
			graphicHolder.flipX = true; //print ("left");
		} 
		if (agent.desiredVelocity.x > 1.0f) {
			graphicHolder.flipX = false; //print ("right");
		}

}

	IEnumerator moveToObject(Collider other){
		jimActive = true;
		trackingToObject = true;
		interactableObject iObject = other.GetComponent<interactableObject>();
		agent.destination = iObject.pointOfInterest.position;
		//not at object yet, but still tracking
		while (agent.transform.position.x != iObject.pointOfInterest.position.x && 
			agent.transform.position.z != iObject.pointOfInterest.position.z && trackingToObject == true) {
			print ("tracking to object");
			yield return null;
		}
		//at object and still tracking
		if (trackingToObject == true) {
			StartCoroutine (Wait (4.5f));
			iObject.typePrompt (); 
			narratorBox.SetActive (true);
		}
	
		while (agent.transform.position.x == iObject.pointOfInterest.position.x &&
		       agent.transform.position.z == iObject.pointOfInterest.position.z && trackingToObject == true) {
			yield return null;
		}
		print ("cancelled interaction");
	}


	//RANDOM STUFF

	void RandomSuggestion (){
		jimActive = true;
		//Jim should be able to randomly decide between doing an Idle, Interact with a random object, and move somewhere
		inactiveTimerLimit = Random.Range (inactiveTimerMinMax.x, inactiveTimerMinMax.y);
		int suggest = Random.Range(0,3);
		if (suggest == 0) {
			RandomMove ();
			//jimIdles.RandomIdle();
			StartCoroutine (Wait (0.75f));
		}
		if (suggest == 1) {
			jimIdles.RandomIdle();
			StartCoroutine (Wait (0.1f));
		}
		if (suggest == 2) {
			//RandomInteraction ();
			StartCoroutine (RandomInteraction());
			//StartCoroutine (Wait (3.5f));
		}
	}

	IEnumerator RandomInteraction (){
		//print ("randoRunning");
		int objectNumber = Random.Range (0, interactableObjects.Length);
		interactableObject objectScript;
		if (interactableObjects [objectNumber].GetComponent<interactableObject> () != null) {
			objectScript = interactableObjects [objectNumber].GetComponent<interactableObject> ();
			agent.SetDestination (objectScript.pointOfInterest.position);
			while (agent.transform.position.x != objectScript.pointOfInterest.position.x && agent.transform.position.z != objectScript.pointOfInterest.position.z) {
				//print ("stuck");
				yield return null;
			}
			print ("find random item");
			narratorBox.SetActive (true);
			objectScript.sentientInteraction ();
		} else {
			print ("no interactableObject script found on: " + interactableObjects [objectNumber].name);
		}
		yield return new WaitForSeconds (4.5f);
		narratorBox.SetActive (false);
		jimActive = false;
	}


	public void RandomMove(){
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (randomDirection, out hit, walkRadius, 1);
		Vector3 finalPosition = hit.position;
		agent.SetDestination (finalPosition);
	}

	IEnumerator Wait(float waitTime){
		yield return new WaitForSeconds (waitTime);
		JimActive (false);
	}

	public void JimActive (bool active){
		jimActive = active;
	}
}