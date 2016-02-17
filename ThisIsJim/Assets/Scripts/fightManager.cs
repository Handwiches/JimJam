using UnityEngine;
using System.Collections;

public class fightManager : MonoBehaviour {

	GameObject[] goodGuys;
	GameObject[] badGuys;
	public playerStats[] pStats;
	public enemyStats[] eStats;

	enum turnState{neutral, player, enemy};
	public turnState turn = turnState.neutral;

	// Use this for initialization
	void  Start () {
		goodGuys = GameObject.FindGameObjectsWithTag ("GoodGuy");
		badGuys = GameObject.FindGameObjectsWithTag ("BadGuy");
		for (int i = 0; i < (goodGuys.Length + badGuys.Length); i++) {
			if (goodGuys [i] != null) {
				pStats[i] = goodGuys[i].GetComponent<playerStats> ();
			}
			if (badGuys [i] != null) {
				eStats[i] = goodGuys[i].GetComponent<enemyStats> ();
			}
		}

		//if player 1 is slower than enemy 1, it is not the player's turn
		if (StatPlusLuck (pStats [0].speed, pStats [0].luck) < StatPlusLuck (eStats [0].speed, eStats [0].luck)) {
			turn = turnState.enemy;
		} else {
			turn = turnState.player;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerAttack (int attackerNumber, int defenderNumber){
		playerStats attacker = pStats [attackerNumber];
		enemyStats defender = eStats [defenderNumber];

		//float attackStrength = attacker.attack;
		float attackStrength = StatPlusLuck(attacker.attack, attacker.luck);
		float defenseStrength = StatPlusLuck (defender.defense, defender.luck);

		float damageDifference = (attackStrength - defenseStrength);
		defender.healthPoints -= damageDifference;
	}

	public void EnemyAttack (int attackerNumber){
		enemyStats attacker = eStats [attackerNumber];
		playerStats defender = pStats [Random.Range (0, goodGuys.Length)];

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
