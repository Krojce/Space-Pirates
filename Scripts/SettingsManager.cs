using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	public static SettingsManager instance = null;
	private GameObject menu; 
	private static bool isShowing = false;
	public Slider volumeSlider;
	private static float volumeValue = 1;

	void Start(){
		menu = GameObject.FindGameObjectWithTag("Settings") as GameObject;
		if (menu) {
			menu.SetActive (isShowing);
		}
		volumeSlider.value = volumeValue;
	}

	void Update() {
		if (Input.GetKeyDown("escape") || Input.GetKeyDown(KeyCode.P)) {
			isShowing = !isShowing;
			if (menu) {
				menu.SetActive (isShowing);
			}
		}
		if (isShowing) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
		VolumeController ();
	}

	public void VolumeController(){
		AudioListener.volume = volumeSlider.value;
		volumeValue = volumeSlider.value;
	}

	public void ResumeButton(){
		isShowing = !isShowing;
		Time.timeScale = 1;
		if (menu) {
			menu.SetActive (isShowing);
		}
	}
}
