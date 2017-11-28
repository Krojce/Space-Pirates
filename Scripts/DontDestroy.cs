using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
	static DontDestroy instance = null;
	// Use this for initialization
	void Start () {
		if (instance != null && instance != this) {
			Destroy (transform.gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(transform.gameObject);
		}
	}
}
