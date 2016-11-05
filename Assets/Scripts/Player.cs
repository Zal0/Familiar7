using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Grid grid;
	public int grid_x, grid_y;
	public Transform trayPivot;
	public Music music;

	private Transform tray;

	public GameObject normal;
	public GameObject sneezing;
	private float sneezingRemainingTime;

	public AudioSource fx;

	// Use this for initialization
	void Start () {
		grid_x = 0;
		grid_y = 1;

		transform.position = grid.GetPos (grid_x, grid_y);

		music.OnSneeze += OnSneeze;
		sneezingRemainingTime = 0;
	}

	void DismissTray() {
		if (tray != null) {
			Destroy (tray.gameObject, 2.0f);
			tray.gameObject.AddComponent< Blinker > ();
			tray.parent = null;
			tray = null;
			grid.GenerateTray ();
		}
	}

	void OnSneeze() {
		if (tray != null && tray.parent == trayPivot) {
			tray.GetComponent< AudioSource > ().Play ();
			DismissTray ();
		}
		sneezingRemainingTime = music.timing;
		normal.SetActive (false);
		sneezing.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (sneezingRemainingTime > 0.0f) {
			sneezingRemainingTime -= Time.deltaTime;
			if (sneezingRemainingTime <= 0.0f) {
				normal.SetActive (true);
				sneezing.SetActive (false);
			}
		} else {

			int progress_x = 0;
			int progress_y = 0;
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				progress_y = 1;
				transform.rotation = Quaternion.LookRotation (Vector3.forward);
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				progress_y = -1;
				transform.rotation = Quaternion.LookRotation (Vector3.back);

				if (tray == null && grid_x == grid.tray_x && grid_y == 1) {
					PickNewTray ();
				}
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				progress_x = -1;
				transform.rotation = Quaternion.LookRotation (Vector3.left);
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				progress_x = 1;
				transform.rotation = Quaternion.LookRotation (Vector3.right);
			}

			if ((progress_x != 0 || progress_y != -0)) {
				char tile = grid.GetTileAt (grid_x + progress_x, grid_y + progress_y);
				if (tile == '.') {
					grid_x += progress_x;
					grid_y += progress_y;
					transform.position = grid.GetPos (grid_x, grid_y);
					if (tray != null && tray.parent != trayPivot) {
						GetTray ();
					}
					fx.Play ();
				} else if (tile == 'x' && tray != null) {
					Vector3 pos = grid.GetPos (grid_x + progress_x, grid_y + progress_y) + Vector3.up * 0.704f; 
					ReleaseTray (pos);

					Client client = grid.tables [grid_x + progress_x, grid_y + progress_y].GetComponentInChildren< Client > ();
					if (client != null && !client.destroyed) {
						DismissTray ();
						client.ServeDrinks ();
					}
				}
			}
		}
	}

	void PickNewTray() {
		tray = grid.tray;
		GetTray ();
	}

	void GetTray() {
		tray.parent = trayPivot;
		tray.localPosition = Vector3.zero;
		tray.localRotation = Quaternion.identity;
	}

	void ReleaseTray(Vector3 pos) {
		if (tray != null) {
			tray.parent = null;
			tray.position = pos;
		}
	}
}
