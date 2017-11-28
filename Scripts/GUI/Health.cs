using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public static float totalHP;
	
	private Text myText;
	public float currentHP;
	private PlayerController player;
	
	void Start(){
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>() as PlayerController;
		myText = GameObject.Find ("Health").GetComponent<Text> ();
		totalHP = player.GetHealth();
		currentHP = totalHP;
		myText.text = currentHP.ToString ();
	}
	
	void Update(){
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>() as PlayerController;
		currentHP = player.GetHealth();
		myText.text = currentHP.ToString ();
	}
	
	public void ResetHealth(){
		currentHP = totalHP;
		myText.text = currentHP.ToString ();
	}
}