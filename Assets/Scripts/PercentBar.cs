using UnityEngine;
using System.Collections;

public class PercentBar : MonoBehaviour {

	public Transform pivot;
	public float percent;
	private float oldPercent;
	
	// Update is called once per frame
	void Update () {
		if (percent != oldPercent) {
			pivot.localScale = new Vector3(percent, 1.0f, 1.0f);
			percent = oldPercent;
		}
	}
}
