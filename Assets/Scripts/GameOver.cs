using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public UnityEngine.UI.Text scoreText;
	public UnityEngine.UI.Text topText;

	// Use this for initialization
	void Start () {
		scoreText.text = "" + Game.score;

		if (Game.score > 0) {
			if (GameJolt.API.Manager.Instance.CurrentUser != null) {
				GameJolt.API.Scores.Add (Game.score, Game.score + (Game.score == 1 ? " client" : " clients"), 208559, "", 
					(bool success) => {
						Debug.Log (string.Format ("Score Add {0}.", success ? "Successful" : "Failed"));
						ShowScores ();
					}
				);
			} 
		} else {
			ShowScores ();
		}
	}

	void ShowScores() {
		GameJolt.API.Scores.Get (
			_scores => {
				for(int i = 0; i < 5 && i < _scores.Length; ++i) {
					topText.text += (i + 1) + "." + " " + _scores[i].PlayerName;
					int nDots = 15 - _scores[i].PlayerName.Length;
					for(int n = 0; n < nDots; ++ n) {
						topText.text += ".";
					}
					topText.text += "" + _scores[i].Value + "\n";
				}
			}
		);
	}
	
	public void OnRetry() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}
}
