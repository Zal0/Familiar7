using UnityEngine;
using System.Collections;

public class SplashSneeze : MonoBehaviour {

	public float timing = 1.0f;
	public GameObject normal;
	public GameObject sneeze;

	void Start () {
		StartCoroutine (Blink());
	}

	IEnumerator Blink () {
		while (true) {
			yield return new WaitForSeconds (timing);
			normal.SetActive (false);
			sneeze.SetActive (true);

			yield return new WaitForSeconds (0.5f);
			normal.SetActive (true);
			sneeze.SetActive (false);
		}
	}
}
