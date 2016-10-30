using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public AudioClip[] music;
	public AudioSource source;
	public float timing;
	private int note;

	public event System.Action OnSneeze;

	// Use this for initialization
	void Start () {
		StartCoroutine (Rhythm());
	}

	IEnumerator Rhythm () {
		while (true) {
			yield return new WaitForSeconds (timing);
			source.PlayOneShot (music[note % music.Length]);
			if ((note % music.Length) == music.Length - 1 && OnSneeze != null) {
				OnSneeze ();
			}

			note++;
		}
	}
}
