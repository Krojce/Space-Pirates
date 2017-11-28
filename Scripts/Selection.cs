using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Selection : MonoBehaviour {
	public GameObject[] images;
	private int selectionIndex = 0;
	private float highScore;
	public GameObject lockImage;
	private int firstHighScore = 50000;
	private int secondHighScore = 100000;
	private Text error;
	// Use this for initialization
	void Start () {
		images[selectionIndex].SetActive (true);
		error =  GameObject.Find("Text").GetComponent<Text>();
	}

	void Update(){
		highScore = PlayerPrefs.GetFloat("Highscore");
		if (highScore < firstHighScore) {
			if (selectionIndex == 1 || selectionIndex == 2) {
				lockImage.SetActive (true);
			}else{
				lockImage.SetActive (false);
			}
		} else if (highScore < secondHighScore) {
			if (selectionIndex == 2) {
				lockImage.SetActive (true);
			}else{
				lockImage.SetActive (false);
			}
		} else {
			lockImage.SetActive (false);
		}
	}

	public void SelectUp (){
		images [selectionIndex].SetActive (false);
		selectionIndex += 1;
		if (selectionIndex > 2) {
			selectionIndex = 0;
		}
		images [selectionIndex].SetActive (true);
	}

	public void SelectDown (){
		images [selectionIndex].SetActive (false);
		selectionIndex -= 1;
		if (selectionIndex < 0) {
			selectionIndex = 2;
		}
		images [selectionIndex].SetActive (true);
	}

	public void Select(){
		if (selectionIndex == 1 & highScore < firstHighScore) {
			error.text = "You need more than " + firstHighScore + " highscore!";
		} else if(selectionIndex == 2 & highScore < secondHighScore){
			error.text = "You need more than " + secondHighScore + " highscore!";
		}else {
			PlayerPrefs.SetInt ("ShipSelection", selectionIndex);
			Application.LoadLevel ("Level 1");
		}
	}
}
