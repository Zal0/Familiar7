using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public AudioClip[] music;
	public AudioSource source;
	public float timing;
	private int note;

	// Use this for initialization
	void Start () {
		StartCoroutine (Rhythm());
	}

	IEnumerator Rhythm () {
		while (true) {
			yield return new WaitForSeconds (timing);
			source.PlayOneShot (music[note % music.Length]);
			note++;
		}
	}
}
