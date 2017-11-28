﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyLVL3 : MonoBehaviour {
	//GAMEOBJECTS
	public GameObject shot;
	public GameObject[] dropItems;
	public GameObject remains;
	public GameObject particlesBoom;
	//SHOTS
	private static float shotsPerSecond = 0.3f;
	private static float projectileSpeed = 6f;
	//ATTRIBUTES
	private float health = 500;
	private int scoreValue = 800;
	//AUDIO
	public AudioClip fireSound;
	public AudioClip deathSound;
	public AudioClip hitSound;
	//SCRIPTS
	private ScoreKeeper scoreKeeper;
	//POWERUPS
	private static float powerUpSpeed = 0.01f;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent(typeof(Projectile)) as Projectile;
		if (missile) {
			AudioSource.PlayClipAtPoint (hitSound, transform.position);
			health = health - missile.GetDamage();
			missile.Hit();
			if(health<0){
				AudioSource.PlayClipAtPoint (deathSound, transform.position);
				scoreKeeper.Score(scoreValue);
				if(Random.value <= 0.6f){
					DropItem(0);
				}
				if(Random.value <= 0.6f){
					DropItem(1);
				}
				if(Random.value <= 0.25f){
					DropItem(2);
				}
				Instantiate(remains, transform.position, transform.rotation);
				Instantiate( particlesBoom, transform.position, transform.rotation);
				Destroy (gameObject);
			}
		}
	}

	void Fire(){
		GameObject beam = Instantiate(shot, this.transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0,-projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void DropItem(int dropNumber){
		GameObject dropItem = Instantiate (dropItems[dropNumber], this.transform.position, Quaternion.identity) as GameObject;
		dropItem.rigidbody2D.velocity = Direction() * powerUpSpeed;
	}

	void Update(){
		float probabilityProjectile = shotsPerSecond * Time.deltaTime;
		if (Random.value < probabilityProjectile) {
			Fire ();
		}
	}

	Vector2 Direction(){
		int randomX = Random.Range(-100,100);
		int randomY = -180;
		Vector3 direction = new Vector2(randomX, randomY);
		return direction;
	}

}