using UnityEngine;
using System.Collections;

public class ClientSpawner : MonoBehaviour {

	public GameObject clientPrefab;
	public Grid grid;

	public float respawnTime = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine (Respawn());
	}
	
	IEnumerator Respawn () {
		while (true) {
			yield return new WaitForSeconds (respawnTime);

			if (grid.free_tables.Count != 0) {
				int idx = Random.Range (0, grid.free_tables.Count);
				GameObject table = grid.free_tables [idx];
				grid.free_tables.RemoveAt (idx);
				grid.busy_tables.Add (table);

				Transform pivot = table.transform.Find ("ClientPivot");
				GameObject.Instantiate (clientPrefab, pivot.position, pivot.rotation, pivot);

				respawnTime -= respawnTime / 5.0f;
				if (respawnTime < 2.0f)
					respawnTime = 2.0f;
			}
		}
	}
}
