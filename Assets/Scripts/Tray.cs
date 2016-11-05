using UnityEngine;
using System.Collections;

public class Tray : MonoBehaviour {

	public bool dismissed = false;
	public float accel_y = 0.0f;

	public void Dismiss(bool animated) {
		if (animated) {
			dismissed = true;
			accel_y = 0.1f;
		} else {
			Destroy (gameObject, 2.0f);
			gameObject.AddComponent< Blinker > ();
		}
	}

	void Update () {
		if (dismissed) {
			accel_y -= 1.0f * Time.deltaTime;
			transform.Translate (Vector3.up * accel_y, Space.World);
			transform.Translate (Vector3.forward * Time.deltaTime * 6.0f);
		} if (transform.position.y < 0.0f) {
			transform.position = new Vector3 (transform.position.x, 0.0f, transform.position.z);
			Destroy (gameObject, 2.0f);
			gameObject.AddComponent< Blinker > ();
			GetComponent< AudioSource > ().Play ();
			enabled = false;
		}
	}
}
