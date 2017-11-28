using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {

	public void Hit(){
		Destroy (gameObject);
	}
}
