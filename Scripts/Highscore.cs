using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscore : MonoBehaviour {
	private static float highScore = 0;
	private Text myText;
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		highScore = PlayerPrefs.GetFloat ("Highscore");
		myText.text = highScore.ToString ();
	}
}
