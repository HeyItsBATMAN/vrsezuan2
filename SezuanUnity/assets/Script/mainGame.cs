using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainGame : MonoBehaviour {

	public GameObject player;
	public bool introPlayed = false;
    public bool playerLocked = false;
    double[,] introSeq;
	int introStep = 0;
	Vector3 startpos;
	float mTimer = 0;
	int speed = 10;
	bool isRunning = false;
    private bool isPlaying = false;
    private bool XButtonState = false;

	void Start () {
		player = GameObject.Find("/Wang");
		
		//Die Positionen, die im Intro gebraucht werden
		introSeq = new double[5,4] {
			{1,-413.4,14.19,10.06},
			{1,-370.4,14.19,10.06},
			{1,-278.7,14.19,10.1},
			{1,-258.8,14.19,4.91},
			{1,-236.8,13.20,-20}
		};

		calcDistance();

		//Wenn Intro schon abgespielt wurde, setze Spieler direkt vor die Götter, sonst an die Introposition
        if (!introPlayed)
        {
            player.transform.position = new Vector3((float)introSeq[0, 1], (float)introSeq[0, 2], (float)introSeq[0, 3]);
        }
		else {
			player.transform.position = new Vector3(-236.8f,14.53f,-25.67f);
		}
        startpos = player.transform.position;		
	}

	void calcDistance() {
		//Rechne die Distanz zwischen den Schritten im Intro
		//Grund: damit die Bewegung im Intro Linear ist
		for (var i = 1; i < introSeq.GetLength(0); i++) {
			Vector3 vecP = new Vector3((float)introSeq[i-1,1],(float)introSeq[i-1,2],(float)introSeq[i-1,3]);
			Vector3 vecC = new Vector3((float)introSeq[i,1],(float)introSeq[i,2],(float)introSeq[i,3]);
			float dist = Vector3.Distance(vecP,vecC);
			introSeq[i,0] = dist/(float)speed;
		}
	}
	
	void playSequence(string _name ,int _step, double[,] _seq) {
		//Der Spieler wird zu einer neuen Position bewegt, abgängig vom Schritt im Intro
		Vector3 newpos = new Vector3((float)_seq[_step,1],(float)_seq[_step,2],(float)_seq[_step,3]);		
		player.transform.position = Vector3.Lerp(startpos,newpos,mTimer/((float)_seq[_step,0]));
		
		//Startet eine Coroutine, die die Sequenz blockiert, solange der Spieler sich noch zu einer neuen Position bewegt
		if (!isRunning) {
			StartCoroutine(setStartPos((float)_seq[_step,0]));
		}
	}

	IEnumerator setStartPos(float time) {
		isRunning = true;
		yield return new WaitForSeconds(time);
		startpos = player.transform.position;
		mTimer = 0;
		introStep++;
		isRunning = false;
	}

	IEnumerator resetSeq() {
		//Debug Funktion zum zurücksetzen
		yield return new WaitForSeconds(5);
		player.transform.position = new Vector3((float)introSeq[0,1],(float)introSeq[0,2],(float)introSeq[0,3]);
		introStep = 0;
	}
	
	void Update () {
		mTimer += Time.deltaTime;

		//Wenn Intro nicht läuft und nicht gestartet wurde, zeige Interaktionsmöglichkeit
		if(!isPlaying && !introPlayed) {
			GameObject.Find("InfoText").GetComponent<Text>().text = "Drücke X um zu beginnen";
            if (Input.GetButton("XButton"))
            {
                isPlaying = true;
				GameObject.Find("GameHandler").GetComponent<TextHandler>().loadText(0);
                StartCoroutine(GameObject.Find("GameHandler").GetComponent<TextHandler>().AutoPlay());
				GameObject.Find("InfoText").GetComponent<Text>().text = "";
                playerLocked = true;
                player.GetComponent<Rigidbody>().useGravity = false;
            }
        }

		//Schalte Gravitation ein, wenn Intro übersprungen wurde (über die IntroPlayer Variable)
		if(!isPlaying && introPlayed) {
			player.GetComponent<Rigidbody>().useGravity = true;
		}

		//Spielt das Intro ab und beendet das Intro, wenn beim letzten Introschritt
        if (!introPlayed) {
			if (introStep < introSeq.GetLength(0) && isPlaying)
            {                
				playSequence("Intro", introStep, introSeq);				
            }
			else if (introStep == introSeq.GetLength(0)) {
				introPlayed = true;
                playerLocked = false;
				player.GetComponent<Rigidbody>().useGravity = true;
            }			
			//StartCoroutine(resetSeq());
		}
	}
}
