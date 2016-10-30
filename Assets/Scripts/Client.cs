using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {

	public float waitTime = 4.0f;
	[HideInInspector]public float remainigTime;
	private Grid grid;
	public bool destroyed;
	public PercentBar bar;

	// Use this for initialization
	void Start () {
		remainigTime = waitTime;
		grid = GameObject.FindObjectOfType< Grid > ();
		destroyed = false;
	}

	void Dismiss(bool served) {
		Destroy (gameObject, 2.0f);
		gameObject.AddComponent< Blinker > ();
		destroyed = true;	

		GetComponentInChildren< MeshRenderer > ().material.color = served ? Color.green : Color.red;
	}

	// Update is called once per frame
	void Update () {
		if (!destroyed) {
			remainigTime -= Time.deltaTime;
			bar.percent = Mathf.Clamp01 (remainigTime / waitTime);
		}
		if (remainigTime <= 0.0f  && !destroyed) {
			Dismiss (false);
		}
	}

	public void ServeDrinks() {
		Dismiss (true);
	}

	void OnDestroy() {
		GameObject table = transform.parent.parent.gameObject;
		grid.busy_tables.Remove (table);
		grid.free_tables.Add (table);
	}
}
