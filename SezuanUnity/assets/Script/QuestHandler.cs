using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Questklasse

public class Quest
{
    public GameObject _Player;
    public GameObject[] objectives;
    public int objectiveStep = 0;
    public string questName;
    public int[] textNumber;
    public int textStep = 0;
    public bool completed = false;
    public bool active = false;
    public bool isInReachOfObjective = false;
    public QuestHandler _qstHandler = GameObject.Find("GameHandler").GetComponent<QuestHandler>();

    //Die Questklasse wird mit einer Liste an Objectives, einer Nummer für den hardcoded Text und dem Namen der Quest initialisiert
    public Quest(string[] _obj, int[] _tN, string _name)
    {
        //Finde Spieler
        _Player = GameObject.Find("Wang");

        //Finde die GameObjects der Objectives
        List<GameObject> _objList = new List<GameObject>();
        foreach (string objective in _obj)
        {
            _objList.Add(GameObject.Find(objective));
        }
        objectives = _objList.ToArray();

        //Hardcoded Text (aus der TextHandler.cs)
        textNumber = _tN;

        //Questname
        questName = _name;
    }

    public void startQuest()
    {
        // Überprüfe Voraussetzungen
        bool _reqscompleted = this.checkReqs();

        // Wenn erfüllt, starte Quest
        if (_reqscompleted)
        {
            _qstHandler._textHandler.UnloadText();
            Debug.Log("Started Quest " + this.questName);
            _qstHandler.activeQuest = this;
            this.active = true;
            this.advanceQuest();
        }
    }

    public void stopQuest()
    {
        // Beende Quest
        if (this.questName == "Dieb") {
            GameObject.Find("Wache").transform.position = new Vector3(-254.2f, 13.87f, -374.2f);
        }
        this.completed = true;
        this.active = false;
        _qstHandler.activeQuest = null;
        Debug.Log("Finished Quest " + this.questName);
    }

    public bool testPlayerObjectiveDistance()
    {
        if (this.objectives.Length > 0)
        {
            //Wenn noch Objectives erfüllt werden müssen
            if (this.objectiveStep < this.objectives.Length)
            {
                //Wenn Distanz vom Spieler zum aktuellen Objective kleiner als ein Grenzwert
                if (Vector3.Distance(_Player.transform.position, this.objectives[this.objectiveStep].transform.position) < 30f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool checkReqs()
    {
        //Jede Quest hat ein paar Voraussetzungen, z.B. darf der Spieler erst die anderen Bewohner um Hilfe bitten, nach dem die Götter Wang darum bitten
        switch (this.questName)
        {
            case "MeetGoetter":
            //Überprüfe ob Intro fertig
                return (GameObject.Find("GameHandler").GetComponent<mainGame>().introPlayed) ? true : false;
            case "Liebesbrief":
            //Überprüfe ob mit Göttern gesprochen wurde
                return (_qstHandler.MeetGoetter.completed) ? true : false;
            case "Diamant":
                return (_qstHandler.MeetGoetter.completed) ? true : false;
            case "Dieb":
                return (_qstHandler.MeetGoetter.completed) ? true : false;
            case "Abgabe":
            //Überprüfe ob die 3 vorherigen Quests abgeschlossen wurden
                return (_qstHandler.Liebesbrief.completed && _qstHandler.Dieb.completed && _qstHandler.Diamant.completed) ? true : false;
            default:
                return false;
        }
    }

    public void advanceQuest()
    {
        //advanceQuest lädt den nächsten Schritt in einer Quest, angefangen mit dem Questtext
        Debug.Log("Objectives: " + (this.objectiveStep + 1) + "/" + this.objectives.Length + " - " + this.objectives[this.objectiveStep].ToString());
        //Überprüft ob der Spieler gelockt ist, da der Spieler nur gelockt ist wenn er in einem Gespräch ist, oder im Intro
        //Grund: Um sicherzustellen, dass man nicht in einem Gespräch schon ein anderes Gespräch lädt
        if (!_qstHandler._gameHandler.GetComponent<mainGame>().playerLocked) {
            this.loadQuestText();
        }        

        //Um die Performance zu schonen, wird nur in einem Interval überprüft, ob der Spieler in Objective-Reichweite ist
        //Daher wird der Boolean hier direkt auf False gesetzt, damit man nicht an dem gleichen Objective mehrmals die Funktion advanceQuest triggern kann
        this.isInReachOfObjective = false;
        
        //Ruft Funktion auf, die die Missionsmarker setzt
        this.placeMarkers();

        if (this.objectiveStep < this.objectives.Length - 1)
        {
            //Liebesbrief Quest muss an der Stelle, an der man mit dem Brief interagiert künstlich einen Schritt nach vorne gesetzt werden
            if (this.questName == "Liebesbrief" && this.objectiveStep == 1)
            {
                this.objectiveStep++;
                this.advanceQuest();
                return;
            }
            //Gleiches bei der Diamant Quest beim aufhebem vom Diamanten
            if (this.questName == "Diamant" && this.objectiveStep == 2)
            {
                this.objectiveStep++;
                return;
            }

            //Die Diebesquest hat 2 Enden, die hier überprüft werden
            if (this.questName == "Dieb") {
                Debug.Log("Dieb Step: " + this.objectiveStep);
                GameObject.Find("ChoiceInfo").GetComponent<Text>().text = "";
                if (this.objectiveStep == 0) {
                    this.objectiveStep = 1;
                    return;
                }
                if (this.objectiveStep == 1) {                   
                    switch (_qstHandler.choice) {
                        case "wahr":
                            this.objectiveStep = 3;
                            this.textStep = 3;
                            _qstHandler._textHandler.UnloadText();
                            this.loadQuestText();
                            GameObject.Find("ChoiceInfo").GetComponent<Text>().text = "Wahrheit";
                            return;
                        case "luge":
                            this.objectiveStep = 2;
                            this.textStep = 1;
                            _qstHandler._textHandler.UnloadText();
                            this.loadQuestText();
                            GameObject.Find("ChoiceInfo").GetComponent<Text>().text = "Lüge";
                            return;
                        default: return;
                    }
                }
                if (this.objectiveStep == 2) {
                    this.objectives[this.objectiveStep].transform.position = new Vector3(this.objectives[this.objectiveStep].transform.position.x,this.objectives[this.objectiveStep].transform.position.y - 20,this.objectives[this.objectiveStep].transform.position.z);
                    this.objectiveStep++;
                    this.advanceQuest();                    
                    return;
                }
                if (this.objectiveStep >= 3 && _qstHandler.choice == "wahr") {
                    GameObject.Find("Wache").transform.position = new Vector3(-365.49f,14.83f,-96.7f);
                    return;
                }
            }
            this.objectiveStep++;            
        }
        else
        {
            //Wenn keine Objectives mehr übrig sind, beende Quest
            this.stopQuest();
        }
    }

    public void loadQuestText()
    {
        //Entlade alten Text
        _qstHandler._textHandler.UnloadText();
        Debug.Log("Loading Text: " + this.textNumber[this.textStep]);
        //Lade den Text der Quest, falls Objective ein Gesprächspartner ist und falls Spieler in Reichweite
        if (this.objectiveIsCharacter() && this.testPlayerObjectiveDistance())
        {
            GameObject.Find("GameHandler").GetComponent<mainGame>().playerLocked = true;
            _qstHandler._textHandler.loadText(this.textNumber[this.textStep]);
            //Setze Text einen Schritt nach vorn
            if (this.textStep < this.textNumber.Length - 1)
            {
                this.textStep++;
            }
        }
    }

    public bool objectiveIsCharacter()
    {
        //Überprüft ob Objective ein Item ist
        string[] items = { "Liebesbrief", "Diamant", "Getreidesaatgut", "VRBrille", "Buch" };
        bool isItem = false;
        foreach (string item in items)
        {
            Debug.Log("Checking Objective: " + this.objectives[this.objectiveStep]);
            if (this.objectives[this.objectiveStep].ToString().Split(' ')[0].Contains(item))
            {
                //Bei Übereinstimmung, dass Objective ein Item ist, breche Loop ab
                isItem = true;
                break;
            }
        }
        if (isItem)
        {
            //Da Objective ein Item -> kein Charakter -> false
            return false;
        }
        else
        {
            return true;
        }
    }

    public void placeMarkers() {
        //Orangene Marker für Items und grüne für Charakter, daher zuerst überprüfen ob Objective ein Charakter bzw. Item
        if (objectiveIsCharacter()) {
            _qstHandler.Questmarker[0].transform.position = new Vector3(this.objectives[this.objectiveStep].transform.position.x, (this.objectives[this.objectiveStep].transform.position.y + 15), this.objectives[this.objectiveStep].transform.position.z);
            if (this.questName == "Dieb" && this.objectiveStep == 0) {
                _qstHandler.Questmarker[1].transform.position = new Vector3(this.objectives[1].transform.position.x, (this.objectives[1].transform.position.y + 15), this.objectives[1].transform.position.z);
            }
        }

        //Spezielle Behandlung für die Items
        switch (this.questName) {
            case "Liebesbrief":
                if (this.objectiveStep < 2)
                {
                    GameObject _item = this.objectives[1];
                    _qstHandler.Questmarker[3].transform.position = new Vector3(_item.transform.position.x, (_item.transform.position.y + 15), _item.transform.position.z);
                }
                break;
            case "Dieb":
                if (this.objectiveStep < 3)
                {
                    GameObject _item = this.objectives[2];
                    _qstHandler.Questmarker[3].transform.position = new Vector3(_item.transform.position.x, (_item.transform.position.y + 15), _item.transform.position.z);
                }
                break;
            case "Diamant":
                if (this.objectiveStep < 3)
                {
                    GameObject _item = this.objectives[2];
                    _qstHandler.Questmarker[3].transform.position = new Vector3(_item.transform.position.x, (_item.transform.position.y + 15), _item.transform.position.z);
                }
                break;
            default: break;
        }
    }
}

public class QuestHandler : MonoBehaviour
{
    public Quest potentialQuest = null;
    public Quest activeQuest;
    public GameObject _gameHandler;
    public TextHandler _textHandler;
    public Quest MeetGoetter;
    public Quest Liebesbrief;
    public Quest Dieb;
    public Quest Diamant;
    public Quest Abgabe;
    public List<Quest> QuestList = new List<Quest>();
    public GameObject[] Questmarker;
    private bool XButtonState = false;
    public string choice = null;
    void Start()
    {
        //Die Marker werden nicht als GameObjects erstellt und zerstört, sondern sind in der Welt geladen und werden lediglich verschoben
        //Hier werden die Marker in ein Array geladen
        Questmarker = new GameObject[4];
        Questmarker[0] = GameObject.Find("Marker1");
        Questmarker[1] = GameObject.Find("Marker2");
        Questmarker[2] = GameObject.Find("Marker3");
        Questmarker[3] = GameObject.Find("ItemMarker");

        //Initialisierung der Quests
        MeetGoetter = new Quest(
            new string[] { "Gott" },
            new int[] { 1 },
            "MeetGoetter"
        );
        Liebesbrief = new Quest(
            new string[] { "MrWengo", "/Items/Liebesbrief", "FrauPeng", "MrWengo" },
            new int[] { 7, 8, 9 },
            "Liebesbrief"
        );
        Dieb = new Quest(
            new string[] { "Leng", "Wache", "/Items/Buch", "Leng"},
            new int[] { 2, 3, 4, 5, 6 },
            "Dieb"
        );
        Diamant = new Quest(
            new string[] { "FrauPenyi", "Fischer", "/Items/Diamant", "FrauPenyi" },
            new int[] { 10, 11, 12 },
            "Diamant"
        );
        Abgabe = new Quest(
            new string[] { "Gott", "ShenTe" },
            new int[] { 13, 14 },
            "Abgabe"
        );
        QuestList.Add(MeetGoetter);
        QuestList.Add(Liebesbrief);
        QuestList.Add(Dieb);
        QuestList.Add(Diamant);
        QuestList.Add(Abgabe);

        _gameHandler = GameObject.Find("GameHandler");
        _textHandler = _gameHandler.GetComponent<TextHandler>();

        //Die Diebesquest hat irgendwo einen Fehler und ist daher deaktiviert
        Dieb.stopQuest();

        //Starte die Coroutine, welche die Questobjectives überprüft
        StartCoroutine(CheckForQuestObjectives());

        //Starte die Coroutine, welche überprüft ob eine startbare Quest in Reichweite ist
        StartCoroutine(CheckForPotentialQuests());
    }

    private IEnumerator CheckForQuestObjectives()
    {
        //Loope die Routine
        while (true)
        {
            //Wenn activeQuest nicht NULL, also wenn es eine aktive Quest gibt
            if (activeQuest != null)
            {
                switch (activeQuest.testPlayerObjectiveDistance())
                {
                    case true:
                        if (activeQuest.objectiveIsCharacter())
                        {
                            //Zeigt den Text für die Diebesquest an
                            if (activeQuest.questName == "Dieb" && activeQuest.objectiveStep <= 1) {
                                GameObject.Find("ChoiceInfo").GetComponent<Text>().text = "X für Wahrheit - Kreis für Lüge";
                            }
                            else
                            {
                                GameObject.Find("QuestInfo").GetComponent<Text>().text = "";
                            }
                        }
                        else
                        {
                            //Wenn ein Item in der Nähe ist, zeige Interaktionstext an
                            GameObject.Find("QuestInfo").GetComponent<Text>().text = "Item aufheben";
                        }
                        activeQuest.isInReachOfObjective = true;
                        break;
                    default:
                        //Ansonsten wird das Textfeld geleert
                        GameObject.Find("QuestInfo").GetComponent<Text>().text = "";
                        activeQuest.isInReachOfObjective = false;
                        break;
                }
            }
            else
            {
                GameObject.Find("QuestInfo").GetComponent<Text>().text = "";
            }
            //Überprüfe alle 200 Millisekunden
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator CheckForPotentialQuests()
    {
        while (true)
        {
            bool questFound = false;
            int counter = 0;
            //Wenn keine Quest aktiv ist, suche nach Quests
            if (activeQuest == null)
            {
                //Setze Marker zurück
                foreach (GameObject _marker in Questmarker)
                {
                    _marker.transform.position = new Vector3(0, 0, 0);
                }

                //Loopt durch die Questliste
                foreach (Quest _qst in QuestList)
                {
                    //Wenn Quest nicht aktive, nicht fertig und die Voraussetzungen erfüllt sind
                    if (!_qst.active && !_qst.completed && _qst.checkReqs())
                    {
                        //Setze Questmarker über Gesprächspartner
                        Questmarker[counter].transform.position = new Vector3(_qst.objectives[0].transform.position.x, (_qst.objectives[0].transform.position.y + 15), _qst.objectives[0].transform.position.z);
                        counter++;

                        //Falls der Spieler nah genug dran ist, um zu reden, zeige Interaktionsmöglichkeit
                        if (_qst.testPlayerObjectiveDistance())
                        {
                            potentialQuest = _qst;
                            GameObject.Find("QuestInfo").GetComponent<Text>().text = "Mit Person reden";
                            questFound = true;
                        }
                    }
                }
            }

            //Leere das Textfeld
            if (!questFound)
            {
                GameObject.Find("QuestInfo").GetComponent<Text>().text = "";
                potentialQuest = null;
            }

            //Überprüfe alle 200 Millisekunden
            yield return new WaitForSeconds(0.2f);
        }
    }
    void Update()
    {
        //Spezielle Case für die Diebquest. Überprüft die weitere Interaktion
        if (activeQuest != null) {
            if (activeQuest.questName == "Dieb" && activeQuest.objectiveStep == 1 && activeQuest.isInReachOfObjective)
            {
                if (Input.GetButton("XButton"))
                {
                    choice = "wahr";
                    activeQuest.advanceQuest();                 
                    return;
                }
                else if (Input.GetButton("SquareButton"))
                {
                    choice = "luge";
                    activeQuest.advanceQuest();
                    return;
                }
            }
        }        

        //Überprüfe Interaktionsknopf
        if (XButtonState != Input.GetButton("XButton"))
        {
            if (!XButtonState)
            {
                if (potentialQuest != null)
                {   
                    //Überprüfe potentielle Quest und starte wenn möglich
                    if (!potentialQuest.active && !potentialQuest.completed && potentialQuest.checkReqs())
                    {
                        potentialQuest.startQuest();
                        GameObject.Find("QuestInfo").GetComponent<Text>().text = "";
                        return;
                    }
                }
                if (activeQuest != null)
                {
                    if (activeQuest.isInReachOfObjective)
                    {
                        //Wenn es eine aktive Quest gibt und in Objectivereichweite, wird Quest fortgesetzt
                        if (_textHandler.textStep == _textHandler.maxTextStep)
                        {
                            activeQuest.advanceQuest();
                            return;
                        }
                    }
                }
            }
        }
    }
}
