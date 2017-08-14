using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTeleporterText : MonoBehaviour
{

    private Text InfoText;
    private string _showText = "Gebäude betreten/verlassen";

    void Start()
    {
        InfoText = GameObject.Find("InfoText").GetComponent<Text>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Wenn Spieler in einem Teleporter-Trigger ist, zeige Text an
        InfoText.text = "";
        InfoText.text = _showText;
    }

    void OnTriggerExit(Collider other)
    {
        //Ansonsten entferne den Text
        InfoText.text = "";
    }
}
