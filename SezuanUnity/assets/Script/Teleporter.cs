using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	private bool XButtonState = false;
	private GameObject player;
	private GameObject[] teleporters;

	GameObject gameHandler;
    ControllerInput mainScript;


	void Start () {
		player = GameObject.Find("Wang");
        //Lade existierende Teleporter in ein Array
		teleporters = new GameObject[4];
		teleporters[0] = GameObject.Find("HausInnenraumPorter");
		teleporters[1] = GameObject.Find("PortToHausInnenraum");
        teleporters[2] = GameObject.Find("TempelPorter");
        teleporters[3] = GameObject.Find("PortToTempel");

		gameHandler = GameObject.Find("GameHandler");
        mainScript = gameHandler.GetComponent<ControllerInput>();
    }

	void Update () {
		if (XButtonState != Input.GetButton("XButton"))
        {
            if (!XButtonState)
            {
                //Loope durch Liste an Teleportern
                foreach (GameObject item in teleporters)
                {
                    //Wenn Spieler kollidiert mit Teleporter-Box
                    if (player.GetComponent<SphereCollider>().bounds.Intersects(item.GetComponent<BoxCollider>().bounds))
                    {
                        string name = item.ToString().Split(' ')[0];
                        //Porte den Spieler rein bzw. raus
                        if (name.Contains("PortTo"))
                        {
                            player.transform.position = GameObject.Find(name.Replace("PortTo", "") + "Porter").transform.position;
                            break;
                        }
                        else
                        {
                            player.transform.position = GameObject.Find("PortTo" + name.Replace("Porter", "")).transform.position;
                            break;
                        }
                    }
                }
            }
            XButtonState = !XButtonState;
        }
	}
}
