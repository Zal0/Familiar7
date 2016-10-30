using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	string[] grid = {
		"---------",
		".........",
		".x.x.x.x.",
		".........",
		".x.x.x.x.",
		".........",
		".x.x.x.x.",
		".........",
		".x.x.x.x.",
		"........."
	};

	public GameObject[,] tables;
	public List< GameObject > free_tables = new List< GameObject >();
	public List< GameObject > busy_tables = new List< GameObject >();

	public float cell_size = 4;
	public GameObject tablePrefab;

	public GameObject trayPrefab;
	[HideInInspector]public Transform tray;
	[HideInInspector]public int tray_x;

	void Start() {
		int x_divs = grid [0].Length;
		int y_divs = grid.Length;
		tables = new GameObject[x_divs, y_divs];

		for (int x = 0; x < x_divs; ++x) {
			for (int y = 0; y < y_divs; ++y) {
				if (GetTileAt (x, y) == 'x') {
					tables[x, y] = GameObject.Instantiate (tablePrefab, GetPos(x, y), Quaternion.identity, transform) as GameObject;
					free_tables.Add (tables[x, y]);
				}
			}
		}

		GenerateTray ();
	}

	public void GenerateTray() {
		tray = (GameObject.Instantiate (trayPrefab) as GameObject).transform;
		tray_x = Random.Range (0, grid [0].Length);
		tray.position = GetPos (tray_x, 0) + Vector3.up * 0.704f;
	}

	public char GetTileAt(int x, int y) {
		if (x < 0 || x >= grid [0].Length || y < 0 || y >= grid.Length) {
			return '-';
		} else {
			return grid[y][x];
		}
	}

	public Vector3 GetPos(int x, int y) {
		int x_divs = grid [0].Length;
		int y_divs = grid.Length;
		float width = (cell_size * x_divs);
		float depth = (cell_size * y_divs);
		float cell_w = width / x_divs;
		float cell_h = depth / y_divs;

		float start_x = transform.position.x - width / 2.0f;
		float start_z = transform.position.z - depth / 2.0f;

		return new Vector3 (start_x + x * cell_w + cell_w * 0.5f, 0.0f, start_z + y * cell_w + cell_w * 0.5f);
	}

	public void OnDrawGizmos() {
		int x_divs = grid [0].Length;
		int y_divs = grid.Length;
		float width = (cell_size * x_divs);
		float depth = (cell_size * y_divs);
		float cell_w = width / x_divs;
		float cell_h = depth / y_divs;

		float start_x = transform.position.x - width / 2.0f;
		float start_z = transform.position.z - depth / 2.0f;

		for (int i = 0; i <= y_divs; ++i) {
			Gizmos.DrawLine (new Vector3 (start_x, 0.0f, start_z + cell_h * i), new Vector3 (start_x + width, 0.0f, start_z + cell_h * i));
		}
		for (int i = 0; i <= x_divs; ++i) {
			Gizmos.DrawLine (new Vector3 (start_x + cell_w * i, 0.0f, start_z), new Vector3 (start_x + cell_w * i, 0.0f, start_z + depth));
		}

		for (int x = 0; x < x_divs; ++x) {
			for (int y = 0; y < y_divs; ++y) {
				char tile = GetTileAt (x, y);
				switch (tile) {
					case '-':
						Gizmos.color = Color.red;
						break;

					case '.':
						Gizmos.color = Color.green;
						break;

					case 'x':
						Gizmos.color = Color.yellow;
						break;
				}
				Gizmos.DrawWireSphere (GetPos(x, y), 0.1f);
			}
		}
	}
}
