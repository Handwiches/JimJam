using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Soundtrack : MonoBehaviour {
	//Soundtrack on a level by level basis
	public AudioSource mainMusic;
	public AudioClip[] clips = new AudioClip[2];
	public string[] levels = new string[2];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded(int level) {
		//if (level == 13)
			//print("Woohoo");
		for (int i = 0; i < clips.Length; i++){
			if (Application.loadedLevelName == levels[i]){
				mainMusic.clip = clips[i];
				mainMusic.Play ();
			}
		}
	}
}
