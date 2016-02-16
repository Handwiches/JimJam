using UnityEngine;
using System.Collections;

public class jimSentientIdles : MonoBehaviour {

	//notified from JimControl2D
	jimControl2D jimControl;

	AnimationClip[] idles; 

	// Use this for initialization
	void Start () {
		jimControl = transform.GetComponent<jimControl2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RandomIdle(){
		//play random idle animation
		//have a list of narrator
		//jimControl.anim.Play(idles[0]);
		int randomNo = Random.Range(1,3);
		jimControl.anim.SetInteger("idleIndex", randomNo);
		print ("anim: " + randomNo);
		StartCoroutine (Wait ());
		//jimControl.anim.SetInteger("idleIndex", 0);

	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (1.5f);
		jimControl.anim.SetInteger("idleIndex", 0);
	}
}
