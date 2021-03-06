﻿using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	public Transform eyes;
	public Transform dropMonsterPoint;
	public GameObject[] Monsters;
	public float minAttackTime = 1f;
	public float maxAttackTime = 3f;
	public float offsetPlayer = 6f;


	private PlayerController player;
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		StartCoroutine (Attack (Random.Range (minAttackTime, maxAttackTime)));
	}
	
	
	void Update () {
		//Глаза следят за игроком
		Vector3 dir = player.transform.position - eyes.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		eyes.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	void FixedUpdate(){
		float x = Mathf.Lerp (transform.position.x, player.transform.position.x + offsetPlayer,0.3f);
		transform.position = new Vector3 (x, transform.position.y, transform.position.z);
	}

	IEnumerator Attack(float time){
		yield return new WaitForSeconds (time);
		if (GameManager.CurrentState == GameManager.GameState.Playing) {
			GameObject monster = Monsters [Random.Range (0, Monsters.Length)];
			Instantiate (monster, dropMonsterPoint.position, Quaternion.identity);

			StartCoroutine (Attack (Random.Range (minAttackTime, maxAttackTime)));
		}
	}
}
