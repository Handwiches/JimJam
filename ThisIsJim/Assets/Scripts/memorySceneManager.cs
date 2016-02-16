using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class memorySceneManager : MonoBehaviour {

	public float totalTime = 5.0f; //time until loading to a fight, or back to the apartment
	public MovieTexture movTexture;

	// Use this for initialization
	void Start () {
		StartCoroutine (EndScene ());
		transform.GetComponent<Renderer>().material.mainTexture = movTexture;
		movTexture.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator EndScene(){
		yield return new WaitForSeconds (totalTime);
		//load to fight or apartment
		SceneManager.LoadScene("JimApartment_Main");
	}
}
