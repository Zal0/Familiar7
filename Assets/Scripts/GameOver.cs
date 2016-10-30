using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public UnityEngine.UI.Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText.text = "" + Game.score;
	}
	
	public void OnRetry() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}
}
