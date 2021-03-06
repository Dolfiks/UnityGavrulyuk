﻿using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	public float speed = 0.1f;
	public GameObject deadFx;
	public int scoreRewarded = 200;

	private bool isStop = false;
	
	
	void FixedUpdate () {
		if (!isStop)
			transform.Translate (-speed, 0, 0);
	}

	void OnBecameInvisible() {
		Destroy (gameObject);	
	}

	public void Dead(){
		GameManager.Score += scoreRewarded;
		Instantiate (deadFx, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
			if (other.CompareTag ("Player")) {
				Dead ();
				other.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				other.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 300f));
			}
		}


	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player")) {
			isStop = true;
			GameManager.instance.GameOver ();
		}
	}
}
