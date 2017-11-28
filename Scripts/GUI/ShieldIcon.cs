using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldIcon : MonoBehaviour {
	public Sprite[] shieldIcons;

	private PlayerController player;
	private int levelShield;

	void Start(){
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>() as PlayerController;
	}

	void Update(){
		levelShield = player.LevelShield ();
		this.GetComponent<Image>().sprite = shieldIcons [levelShield];
	}
}
