using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {
	//ATTRIBUTE
	private static int index = 0;
	//GAMEOBJECTS
	public GameObject[] playerPrefab;
	private GameObject player;
	
	void Awake () {
		index = PlayerPrefs.GetInt ("ShipSelection");
		player = Instantiate (playerPrefab[index], transform.position, Quaternion.identity) as GameObject;
		player.transform.parent = this.transform;
	}


	void Update () {

	}
}