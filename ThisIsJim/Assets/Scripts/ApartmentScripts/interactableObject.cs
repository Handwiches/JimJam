using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//these will need to cue up memories (scenes), later

public class interactableObject : MonoBehaviour {

	public Animator anim;
	public Transform pointOfInterest; //point jim walks to upon being clicked

	public string[] sentientSentences;
	public float typeSpeed = 0.1f;
	public AudioClip typeSound; //frequency is every other character for now
	public GameObject narratorBox;

	public string[] memoryScenes;

	public string playerPrompt = "Jim want sum fuck?";
	public GameObject promptObject;
	public jimControl2D player;

	// Use this for initialization
	void Start () {
		anim = transform.GetComponent <Animator>();
		//narratorBox = GameObject.FindGameObjectWithTag ("TextBox");
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<jimControl2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hover(){ //mouse is hovering from jimControl.cs
		//print("works");
		//anim.SetBool ("Hover", true);
	}

	public void Clicked(){
		print("CLICKED");
	}

	public void OnMouseOver(){
		//print (gameObject.name);
		if (anim != null) {
			anim.SetBool ("Hover", true);
		}
	}

	public  void OnMouseExit(){
		if (anim != null) {
			anim.SetBool ("Hover", false);
		}
	}

	public void typePrompt (){
		textTyper.TypeText(narratorBox, playerPrompt, typeSpeed, typeSound);
		promptObject.GetComponent<memoryPrompt>().iObject = gameObject.GetComponent<interactableObject>();
		promptObject.SetActive (true);
	}

	public void Answer(bool answer){
		if ( memoryScenes[0] != null && answer == true){
			SceneManager.LoadScene(memoryScenes[0]);
		}else{
			promptObject.SetActive (false);
			narratorBox.SetActive (false);
		}
	}

	public void sentientInteraction (){
		//sentientSentences [Random.Range (0, sentientSentences.Length)];
		textTyper.TypeText(narratorBox, sentientSentences [Random.Range (0, sentientSentences.Length)], typeSpeed, typeSound);
	}
}
