using UnityEngine;
using System.Collections;

public class enemyStats : MonoBehaviour {

	//The stats will be ATK (Attack), DEF (Defense), SPD (Speed), MOD (Mood),
	//MRY (Memory), LCK (Luck), HP (Health Points), and MP (Memory Points).

	//Maybe enemy moods are affected by a playthrough?

	public string name = "Jim";

	public float attack = 25.0f; //Standard attack strength
	float baseAttack;
	public float defense = 10.0f; //Affects how much damage Jim takes when hurt
	float baseDefense;
	public float speed = 10.0f; //Speed affects dodge capability and if Jim attacks first, 
	float baseSpeed;
	public float obedience = 100.0f; //always out of 100
	public float memory = 25.0f; //Memory affects the strength/effectiveness of Jim's skills
	float baseMemory;
	public float luck = 10.0f; //Affects almost everything a slight degree, including how many pennies Jim can find
	float baseLuck;
	public float healthPoints = 100.0f; //affects how much health Jim has
	float baseHP;
	public float memoryPoints = 10.0f; //Affects how many points Jim can expend on skill moves in combat.

	// Use this for initialization
	void Start () {
		baseAttack = attack;
		baseDefense = defense;
		baseSpeed = speed;
		baseMemory = memory;
		baseLuck = luck;
		baseHP = healthPoints;
		//in case of modifiers
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
