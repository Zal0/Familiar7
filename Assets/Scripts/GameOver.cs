﻿using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public UnityEngine.UI.Text scoreText;
	public UnityEngine.UI.Text topText;

	public GameObject topWaitersLabel;
	public GameObject retryButton;
	public GameObject inputField;

	private void SendScoreAndShowAll(string name) {
		topWaitersLabel.gameObject.SetActive (true);
		retryButton.gameObject.SetActive (true);
		inputField.gameObject.SetActive (false);
		if (name == null) {
			//Gamejolt user
			GameJolt.API.Scores.Add (Game.score, Game.score + (Game.score == 1 ? " client" : " clients"), 208559, "", 
				(bool success) => {
					ShowScores ();
				}
			);
		} else {
			//Gues user
			GameJolt.API.Scores.Add (Game.score, Game.score + (Game.score == 1 ? " client" : " clients"), name, 208559, "", 
				(bool success) => {
					ShowScores ();
				}
			);
		}
	}

	// Use this for initialization
	void Start () {
		scoreText.text = "" + Game.score;

		if (Game.score > 0) {
			if (GameJolt.API.Manager.Instance.CurrentUser != null) {
				SendScoreAndShowAll (null);
			} else {
				topWaitersLabel.gameObject.SetActive (false);
				retryButton.gameObject.SetActive (false);
				inputField.gameObject.SetActive (true);
			}
		} else {
			topWaitersLabel.gameObject.SetActive (true);
			retryButton.gameObject.SetActive (true);
			inputField.gameObject.SetActive (false);
			ShowScores ();
		}
	}

	public void OnNameEntered() {
		SendScoreAndShowAll (inputField.GetComponent< UnityEngine.UI.InputField >().text);
	}

	void ShowScores() {
		GameJolt.API.Scores.Get (
			_scores => {
				if(_scores != null) {
					for(int i = 0; i < 5 && i < _scores.Length; ++i) {
						topText.text += (i + 1) + "." + " " + _scores[i].PlayerName;
						int nDots = 15 - _scores[i].PlayerName.Length;
						for(int n = 0; n < nDots; ++ n) {
							topText.text += ".";
						}
						topText.text += "" + _scores[i].Value + "\n";
					}
				}
			}
		);
	}
	
	public void OnRetry() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}
}
