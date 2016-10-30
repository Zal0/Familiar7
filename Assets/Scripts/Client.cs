using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {

	public float waitTime = 4.0f;
	[HideInInspector]public float remainigTime;
	private Grid grid;
	private bool destroyed;
	public PercentBar bar;

	// Use this for initialization
	void Start () {
		remainigTime = waitTime;
		grid = GameObject.FindObjectOfType< Grid > ();
		destroyed = false;
	}
	
	// Update is called once per frame
	void Update () {
		remainigTime -= Time.deltaTime;
		bar.percent = Mathf.Clamp01(remainigTime / waitTime);
		if (remainigTime < 0.0f  && !destroyed) {
			Destroy (gameObject, 2.0f);
			gameObject.AddComponent< Blinker > ();
			destroyed = true;
		}
	}

	void OnDestroy() {
		GameObject table = transform.parent.parent.gameObject;
		grid.busy_tables.Remove (table);
		grid.free_tables.Add (table);
	}
}
