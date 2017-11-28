using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	public static int score = 0;
	private Text myText;
	public static int highScore = 0;

	void Start(){
		myText = GetComponent<Text>();
		myText.text = score.ToString ();
	}

	public void Score(int points){
		score += points;
		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetFloat("Highscore", highScore);  
		}
		myText.text = score.ToString ();
	}

	public void Reset(){
		score = 0;
		myText.text = score.ToString ();
	}

	public int GetScore(){
		return score;
	}
}
