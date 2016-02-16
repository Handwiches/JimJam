using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuScript : MonoBehaviour {

	public bool gameStarted = false;//Game not started -> main menu
			//Game started -> hide main menu buttons, reveal extra in options menu
	//public AudioMixer masterMixer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel (string levelToLoad){
		Application.LoadLevel (levelToLoad);
	}

	public void OpenWebsite (string siteToOpen){
		Application.OpenURL (siteToOpen);
	}

	public void Quit (){
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}
