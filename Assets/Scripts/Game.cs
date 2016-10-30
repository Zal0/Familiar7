using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public static int score = 0;
	float reviews = 1.0f;
	public PercentBar reviewsBar;
	public UnityEngine.UI.Text scoreText;

	void Start() {
		score = 0;
	}

	public void ClientDone(bool happy) {
		if(happy) {
			reviews = Mathf.Clamp01 (reviews + 0.2f);
			score ++;
		} else {
			reviews = Mathf.Clamp01 (reviews - 0.2f);
			if (reviews <= 0.01f) {
				UnityEngine.SceneManagement.SceneManager.LoadScene ("GameOver");
			}
		}
	}

	void Update() {
		reviewsBar.percent = reviews;
		scoreText.text = "" + score;
	}
}
