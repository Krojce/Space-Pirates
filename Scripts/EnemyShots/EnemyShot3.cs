using UnityEngine;
using System.Collections;

public class EnemyShot3 : MonoBehaviour {

	public float damage;
	public float smooth = 1f;
	private Quaternion targetRotation;

	void Start(){
		targetRotation = transform.rotation;
	}
	
	public void Hit(){
		Destroy (gameObject);
	}
	
	public float GetDamage(){
		return damage;
	}

	void Update() {
		targetRotation *=  Quaternion.AngleAxis(5, Vector3.forward);
		transform.rotation= Quaternion.Lerp (transform.rotation, targetRotation , 10 * smooth * Time.deltaTime); 
	}
}
