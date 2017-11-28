using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoubleFireScript : MonoBehaviour {
	public Sprite[] shieldIcons;
	private PlayerController player;
	private int powerUpCount;
	private bool isActive;
	
	void Start(){
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>() as PlayerController;
	}
	
	void Update(){
		powerUpCount = (int)player.DoubleFireSeconds ();
		isActive = player.DoubleFireActive ();
		if (isActive){
			this.GetComponent<Image> ().sprite = shieldIcons [10 - powerUpCount];
		}
		if (!isActive) {
			this.GetComponent<Image> ().sprite = shieldIcons [0];
		}
	}
}
