using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	float score = 0;
	float reviews = 1.0f;
	public PercentBar reviewsBar;
	public UnityEngine.UI.Text scoreText;

	public void ClientDone(bool happy) {
		if(happy) {
			reviews = Mathf.Clamp01 (reviews + 0.2f);
			score ++;
		} else {
			reviews = Mathf.Clamp01 (reviews - 0.2f);
			if (reviews == 0.0f) {
				//TODO: Game Over
			}
		}
	}

	void Update() {
		reviewsBar.percent = reviews;
		scoreText.text = "" + score;
	}
}
