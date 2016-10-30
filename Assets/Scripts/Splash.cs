using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	public void OnPlay() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}
}
