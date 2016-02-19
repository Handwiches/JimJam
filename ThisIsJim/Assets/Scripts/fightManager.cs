using UnityEngine;
using System.Collections;
using System.Collections.Generic; //if using lists

public class fightManager : MonoBehaviour {

	public GameObject[] characters;
	public List<characterStats> cStats = new List<characterStats>();			//in order of highest speed to lowest
	public List<GameObject> fighters = new List<GameObject>();					//in order of highest speed to lowest
	public List<characterStats> goodGuysStats = new List<characterStats>();

	public enum turnState{neutral, player, enemy};
	public turnState turn;

	// Use this for initialization
	void  Start () {
		characters = GameObject.FindGameObjectsWithTag ("Fighter");
		for (int i = 0; i < characters.Length; i++) {
			cStats.Add (characters[i].GetComponent<characterStats> ()); //gets character stats reference
			fighters.Add (characters [i]); //adds gameObjects under Fighter tag to fighters list
			if (cStats [i].enemy == false) {
				goodGuysStats.Add (cStats [i]); //adds player characters stats to a list
			}
		}

		if (fighters.Count > 0){
			fighters.Sort (delegate(GameObject b, GameObject a) {
				return (a.GetComponent<characterStats> ().speed).CompareTo (b.GetComponent<characterStats> ().speed);
				});
			cStats.Sort (delegate(characterStats d, characterStats c) {
				return (c.speed).CompareTo (d.speed);
			});
		}



		//if player 1 is slower than enemy 1, it is not the player's turn
		//if (StatPlusLuck (eStats [0].speed, pStats [0].luck) < StatPlusLuck (eStats [0].speed, eStats [0].luck)) {
			//turn = turnState.player;
	}
	
	// Update is called once per frame
	void Update () {
		//Psuedo code of what needs to happen:
		/*
		 * 
		 * check who has highest speed of the group
		 * 
		 * if player and player is not dead, then
		 * playerStats[0].ShowOptions(); //shown on a timer? on-screen too?, IEnumerator
		 * UI button executes action
		 * 
		 * if enemy and enemy is not dead, then
		 * enemyStats[0].RandomOptionSelect();
		 * 
		 * check for next lowest speed of group, if null, pick highest speed of group
		 * 
		 * */

		//note about the playerStats,
		//if I save them at the end of every fight scene or skills edit
		// in the options menu, they can just be loaded at the beginning of another scene,
		//no need to worry aboout bringing the Jim prefab from the apartment into the fight scenes
	}

	public void StartTurn (){
		//if (pStats [0].speed < eStats [0].speed)

	}

	public void PlayerAttack (int attackerNumber, int defenderNumber){
		characterStats attacker = cStats [attackerNumber];
		characterStats defender = cStats [defenderNumber];

		//float attackStrength = attacker.attack;
		float attackStrength = StatPlusLuck(attacker.attack, attacker.luck);
		float defenseStrength = StatPlusLuck (defender.defense, defender.luck);

		float damageDifference = (attackStrength - defenseStrength);
		defender.healthPoints -= damageDifference;
	}

	public void EnemyAttack (int attackerNumber){
		characterStats attacker = cStats [attackerNumber];
		characterStats defender = goodGuysStats [Random.Range (0, goodGuysStats.Count)];

		float attackStrength = StatPlusLuck(attacker.attack, attacker.luck);
		float defenseStrength = StatPlusLuck (defender.defense, defender.luck);

		float damageDifference = (attackStrength - defenseStrength);
		defender.healthPoints -= damageDifference;
	}

	float StatPlusLuck (float stat, float luck){
		float rolled = Random.Range (0.0f, 100.0f);
		if (rolled < luck) {
			print ("lucky roll");
			float newAttackStrength = (stat * 1.25f);
			return newAttackStrength;
		} else {
			print ("UNlucky roll");
			return stat;
		}
	}
}
