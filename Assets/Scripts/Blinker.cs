using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour {

	public float timing = 0.15f;

	void Start () {
		StartCoroutine (Blink());
	}

	IEnumerator Blink () {
		while (true) {
			yield return new WaitForSeconds (timing);
			foreach (MeshRenderer m in GetComponentsInChildren< MeshRenderer >()) {
				m.enabled = !m.enabled;
			}
		}
	}
}
