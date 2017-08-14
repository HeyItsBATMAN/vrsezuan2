using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour {
	public float turnSpeed = 50f;
	void Update () {
		//Rotiert Questitems
		transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
	}
}
