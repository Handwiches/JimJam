using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

	//The stats will be ATK (Attack), DEF (Defense), SPD (Speed), MOD (Mood),
	//MRY (Memory), LCK (Luck), HP (Health Points), and MP (Memory Points).

	float attack = 25.0f; //Attack affects Jim's standard attack strength
	float defense = 15.0f; //Defense affects how much damage Jim takes when hurt
	float speed = 10.0f; //Speed affects dodge capability and if Jim attacks first, 
	string mood = "Unassigned"; //Mood affects how much Jim's mood determines his obedience
	float obedience = 100.0f; //always out of 100
	float memory = 25.0f; //Memory affects the strength/effectiveness of Jim's skills
	float luck = 10.0f; //Affects almost everything a slight degree, including how many pennies Jim can find
	float healthPoints = 100.0f; //affects how much health Jim has
	float memoryPoints = 10.0f; //Affects how many points Jim can expend on skill moves in combat.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
