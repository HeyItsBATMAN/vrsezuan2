using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCanvasToCam : MonoBehaviour {

	Camera rightcam;
	Canvas canvas;

	void Start () {
		//Wenn das Spiel in VR ausgeführt wird, existieren 2 Kameras als "Augen".
		//In dieser Funktion wird der User-Interface Canvas and die "Augen" befestigt
		StartCoroutine(AttachCam());
	}

	IEnumerator AttachCam() {
		yield return new WaitForSeconds(0.1f);
		try
		{
			rightcam = GameObject.Find("Main Camera Right").GetComponent<Camera>();
			canvas = GameObject.Find("CameraCanvas").GetComponent<Canvas>();
			canvas.worldCamera = rightcam;
		}
		catch (System.Exception)
		{
			Debug.Log("Keine VR Kamera gefunden");
		}
	}
}
