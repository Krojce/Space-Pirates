using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public static float totalHP;

	public float currentHP;
	private PlayerController player;
	
	void Start(){
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>() as PlayerController;
		totalHP = player.GetHealth();
		currentHP = totalHP;
	}

	void Update(){
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>() as PlayerController;
		currentHP = player.GetHealth();
		transform.localScale = new Vector3 ((currentHP/totalHP)*2, 2, 1);
	}
	
	public void ResetHealth(){
		currentHP = totalHP;
	}
}
