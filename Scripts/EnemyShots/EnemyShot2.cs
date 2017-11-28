using UnityEngine;
using System.Collections;

public class EnemyShot2 : MonoBehaviour {

	public float damage;
	
	public void Hit(){
		Destroy (gameObject);
	}
	
	public float GetDamage(){
		return damage;
	}
}
