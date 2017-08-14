using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Characters
{
    public object this[string propertyName]
    {
        get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
        set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
    }

    public GameObject Wang { get; set; }
    public GameObject Gott { get; set; }
    public GameObject Gott2 { get; set; }
    public GameObject Gott3 { get; set; }
    public GameObject ShenTe { get; set; }
    public GameObject Leng { get; set; }
    public GameObject MrWengo { get; set; }
    public GameObject Wache { get; set; }
    public GameObject FrauPeng { get; set; }
    public GameObject FrauPenyi { get; set; }
    public GameObject Fischer { get; set; }
}

public class TextHandler : MonoBehaviour
{
    private string colors =
@"Wang:#8000FF
Gott:#58FAF4
Gott2:#2EFEF7
Gott3:#01DFD7
ShenTe:#F781D8
Leng:#FF8000
Wache:#3B0B0B
MrWengo:#A9A9F5
FrauPeng:#FE2E64
FrauPenyi:#A4A4A4
Fischer:#0000FF
Bauer:#5F4C0B
Verkäufer:#F2F5A9
Verkäufer2:#F5F6CE
Jan:#82FA58";
    public int textStep = 0;
    public int maxTextStep = 0;
    private int distanceNeeded = 50;
    public Characters characters = new Characters();
    public bool XButtonState = false;
    public string[,] currentText;
    //public float distance = 100f;
    private Text UIText;
    private bool playIntro = true;
    public bool textLoaded = false;
    private GameObject[] UIComponents;
    private string _lastConversationPartner;

    string[] lines;

    //Texte

    string[] gameText = { 
        //Intro | 0
@"Wang|Ich bin Marktverkäufer in der Hauptstadt von Sezuan
Wang|Mein Geschäft läuft beschaulich und
Wang|die Ernte war in letzter Zeit zum Vergessen
Wang|Es herrscht große Armut und ich
Wang|bleibe Woche für Woche ohne Verdienst
Wang|Unserer schönen Stadt können nur noch die Götter helfen
LoadNext|WangZuGott1", 
        //WangZuGott1 | 1
@"Gott|
Gott|Können dies die Götter sein?
Gott|Es scheint so hell. Dies müssen sie sein
Gott|Sie wirken so unglaublich groß
Gott|und sind so gut gekleidet
Gott|Gott: Werden wir hier erwartet? Ist dies die Hauptstadt?
Gott|Ja, seit langem wartet unsere Stadt auf eure Ankunft
Gott|Es ist unglaublich, dass ich
Gott|der Erste bin, der euch zu Gesicht bekommt
Gott|Gott: Wir benötigen eine Unterkunft für
Gott|Gott: die heutige Nacht? Hättest du eine für uns?
Gott|Jedes Haus sollte froh sein,
Gott|die Götter empfangen zu dürfen.
Gott|Sucht euch jenes aus,
Gott|welches euch am besten gefällt.
Gott|Gott: Uns ist es egal. Wir nehmen irgendeins.
Gott|Ich werde mich für euch auf die Suche machen
Gott|Gott: Vielen Dank
Gott|",
    //QUESTS
    //Dieb
    //Treffen auf Leng | 2
@"Leng|
Wang|Hallo Leng, was treibt dich hierher?
Leng|Leng:Eine lange Geschichte. Ich wohne jetzt da vorne und bin auf der Suche.
Wang|Nach was?
Leng|Leng:Nach einem Buch. Aber es ist im Tempel.
Wang|Dann geh doch in den Tempel und frag nach dem Buch.
Leng|Leng:Das kann ich doch nicht, das weißt du.
Wang|Stimmt, das bist du aber auch selber Schuld.
Leng|Leng:Jaa, aber ich brauche es sehr dringend. Kannst du mir helfen?
Wang|Da hab ich doch nur Ärger, wenn ich dir helfe.
Leng|Leng:Es springt eine gute Summe Geld für dich raus.
Wang|Wie viel?
Leng|Leng:Genug, vertrau mir.
Wang|Aber nur dieses eine Mal noch und ich hab noch eine Bitte.
Leng|Leng:ich höre.
Wang|Du lässt drei Freunde von mir bei dir übernachten.
Leng|Leng:Solang es sonst nichts ist, ich habe genug Platz.
Wang|Danke. Was soll ich denn tun?
Leng|Leng:Du gehst zum Tempel, an den Wachen vorbei und in den Tempel.
Leng|Leng:Dort findest du ein grünes Buch
Leng|Leng:“Der gute Mensch von Sezuan”. Dieses Buch bräuchte ich.
Wang|Also muss ich es stehlen?
Leng|Leng:Nur für kurze Zeit entwenden.
Wang|Das ist wirklich das letzte Mal, dass ich dir einen Gefallen tun.
Leng|Leng:Du weißt dir winkt eine Belohnung.
Wang|Ok, ich mach mich auf den Weg.
Leng|Leng:Pass auf dich auf.",
    //Option 1 Lüge | 3
    @"Wache|Wache:Wie kann ich dir helfen?
Wang|Ich wollte nur kurz in den Tempel, ich wollte ein Gebet sprechen.
Wache|Wache:Es ist gerade eher ungünstig. Es findet eine kleine Versammlung statt.
Wang|Es geht auch sehr schnell. Ich bin auch leise.
Wache|Wache:Versuch aber nicht aufzufallen, eigentlich soll ich keinen reinlassen.
Wang|Ich gebe keinen Ton von mir. Bin in 5 Minuten wieder raus.
Wache|Wache:Ok, beeil dich.",
    //Option 1 Ende | 4
@"Leng|Leng:Hast du es?
Wang|Jaa hier. Ging sogar recht schnell mit ein bisschen lügen.
Leng|Leng:Sehr gut.
Wang|Wo ist das versprochene Geld?
Leng|Leng:Du hast echt gedacht, ich meine das ernst mit dem Geld.
Wang|Du hattest dein Wort gegeben.
Leng|Leng:Jaa und wahrscheinlich noch eine Unterkunft.
Wang|Was soll das denn? Ich hätte verhaftet werden können. Das ist dein Dank.
Leng|Leng:Du hast echt gedacht, ich habe mich geändert.
Wang|Jaa.
Leng|Leng:Verschwinde jetzt.
Wang|Ich will mein Geld.
Leng|Leng:Hau jetzt ab, sonst steck ich dich in den Boden.
Wang|Du bist das Letzte.",
//Option 2 Wahrheit | 5
@"Wache|Wache:Wie kann ich dir helfen?
Wang|Ich hab eine Bitte. Kommen sie mit mir mit.
Wache|Wache:Was ist los?
Wang|Leng hat mich geschickt, ich soll ein Buch für ihn stehlen aus dem Tempel.
Wache|Wache:Leng? Ist der schon wieder aus dem Gefängnis.
Wang|Jaa, er fängt schon wieder an und wollte mich bestechen.
Wache|Wache:Wo ist er?
Wang|Ich führe sie hin?
Wache|Wache:Ich hol noch meinen Kollegen, dann folgen wir ihnen.",
//Option 2 Ende | 6
@"Leng|Leng:Wer sind die zwei?
Wang|Die nehmen dich jetzt mit.
Leng|Leng:Warum?
Wang|Ich hab ihnen alles erzählt.
Leng|Leng:Du Lügner, das stimmt alles gar nicht. Hören Sie nicht auf ihn.
Wache|Wache:Seien sie ruhig, wir wissen genau, was sie vorhatten. Sie bekommen das Buch nicht.
Leng|Leng:Ich brauche es aber.
Wache|Wache:Wir nehmen sie jetzt mit.
Leng|Leng:Das können sie nicht machen.
Wang|Können sie wohl.
Wang|Was wird aus dem Haus?
Wache|Wache:Wir müssen dies nach weiteren Sachen durchsuchen. Es bleibt erstmal geschlossen.
Wang|Okay. Schade. Ich hatte noch eine Unterkunft gesucht für die Götter.
Wache|Wache:Versuchen sie es doch mal bei Frau Shain, sie hat doch ein riesiges Haus.
Wang|Danke.",
//Szene Liebespaar | 7
@"MrWengo|
MrWengo|Mr. Wengo: Hallo Wang, was treibt dich hierher?
Wang|Ich suche eine Unterkunft für die drei Götter, die in die Stadt gekommen sind.
MrWengo|Mr. Wengo: Wie nett von dir. Vielleicht kann ich dir helfen.
Wang|Das wäre super.
MrWengo|Mr. Wengo: Du müsstest mir vorher aber helfen.
Wang|Gerne.
MrWengo|Mr. Wengo: Du kennst doch Frau Peng von gegenüber?
Wang|Jaa, die nette Frau Peng kenn ich.
MrWengo|Mr. Wengo: Ich habe ihr einen Liebesbrief geschrieben. Doch sie soll ihn nicht lesen.
Wang|Aber warum denn nicht?
MrWengo|Mr. Wengo: Es ist mir peinlich, sie soll ihn nicht sehen.
Wang|Aber hat sie ihn schon?
MrWengo|Mr. Wengo: Ich habe ihn heute Morgen in den Briefkasten geworfen.
Wang|Vielleicht ist er noch darin.
MrWengo|Mr. Wengo: Ich hoffe. Kannst du ihn vielleicht daraus holen?
Wang|Warum machen sie das nicht selber?
MrWengo|Mr. Wengo: Ich habe es schon versucht, meine Hände sind zu groß. Deine sind kleiner.
Wang|Ich kann eben mal schnell rüber gehen und nachschauen.
MrWengo|Mr. Wengo: Bitte lieber Wang.
Wang|Bis gleich.",
//Szene Liebespaar | 8
@"FrauPeng|Frau Peng: Was machen sie da?
Wang|Ähhm, ich bin auf der Suche nach einem Brief.
FrauPeng|Frau Peng: Was für ein Brief?
Wang|Herr Wengo schickt mich. Ich sollte ihm seinen Brief wiederholen.
FrauPeng|Frau Peng: Aber warum denn?
Wang|Der Brief war ihm peinlich und sie sollten ihn nicht lesen.
FrauPeng|Frau Peng: Aber er war doch so wunderschön.
Wang|Ach ehrlich??
FrauPeng|Frau Peng: Jaa so liebe Worte. Ich fand ihn auch schon immer toll.
Wang|Was sie nicht sagen.
FrauPeng|Frau Peng: Ich hatte mich auch nie getraut ihn anzusprechen.
Wang|Das ist doch super.
FrauPeng|Frau Peng: Jaa endlich ist es soweit.
Wang|Ich werde schnell zu ihm rüber gehen und es ihm sagen.
FrauPeng|Frau Peng: Mach das.
FrauPeng|",
//Szene Liebespaar | 9
@"MrWengo|
MrWengo|Mr. Wengo: Und, haben sie den Brief?
Wang|Nein, aber was viel besseres.
MrWengo|Mr. Wengo: Was ist denn los?
Wang|Sie hat den Brief gelesen.
MrWengo|Mr. Wengo: Oh nein.
Wang|Sie fand ihn wunderschön. Sie ist so froh, dass sie sich getraut haben.
MrWengo|Mr. Wengo: Wirklich? Das hat sie gesagt?
Wang|Jaa, sie hatte schon länger das gleiche Empfinden.
MrWengo|Mr. Wengo: Ich muss sie unbedingt sehen.
Wang|Das können sie gerne. Wie sieht es mit der Unterkunft aus?
MrWengo|Mr. Wengo: Ich will Frau Peng sehen, lassen sie mich mit ihrer Unterkunft in Ruhe.
Wang|Aber sie hatten gesagt, sie können mir helfen?
MrWengo|Mr. Wengo: Jaa, aber ich habe keine Zeit und auch keinen Platz.
Wang|Aber Mr. Wengo? Bitte.
MrWengo|Mr. Wengo: Du musst jetzt gehen, ich muss zu ihr.
Wang|Ach Mr. Wengo, sie waren meine Hoffnung.
MrWengo|Mr. Wengo: Geh jetzt bitte.
Wang|Aber? …",
//Quest Diamanten | 10
@"FrauPenyi|Frau Penyi: Ach mein süßer Wang, komm doch mal her.
Wang|Was gibt es Frau Penyi?
FrauPenyi|Frau Penyi: Ich kann kaum noch Laufen und ich wollte eigentlich zum Fischer.
Wang|Was wollten sie da?
FrauPenyi|Frau Penyi: Ich sollte nur schnell ein paar Diamanten abholen.
Wang|Das kann ich doch auch für sie eben erledigen.
FrauPenyi|Frau Penyi: Du bist so ein lieber Jung. Das wäre so lieb von dir.
Wang|Für sie doch immer gerne, Frau Penyi.
FrauPenyi|Frau Penyi: Du bekommst auch eine kleine Belohnung.
Wang|Das brauchen sie doch nicht.
FrauPenyi|Frau Penyi: Doch, so möchte ich es.",
//Quest Diamanten | 11
@"Fischer|Fischer: Ach Wang. Lange nicht gesehen.
Wang|Jaa, ich war länger nicht mehr hier oben bei Ihnen.
Fischer|Fischer: Wie kann ich dir denn helfen?
Wang|Ich soll die Diamanten von Frau Penyi abholen.
Fischer|Fischer: Das ist aber lieb von dir, aber ich muss dich leider enttäuschen.
Wang|Warum?
Fischer|Fischer: Ich habe die Diamanten bisher nicht besorgen können.
Wang|Jetzt bin ich hier umsonst hingekommen.
Fischer|Fischer: Ich hatte leider noch keine Zeit. Du kannst sie aber selber holen.
Wang|Wie? Wo finde ich die Diamanten denn?
Fischer|Fischer: Hinten bei den Bergen ist eine Höhle, dort findest du einige Diamanten.
Wang|In der kleinen Höhle dort hinten?
Fischer|Fischer: Jaa, genau.
Wang|Dann suche ich diese selber und bringe diese Frau Penyi.
Fischer|Fischer: Jaa, sie wollte einen kleineren Diamanten haben.
Wang|Ok, ich hoffe, dass ich diese finde.
Fischer|Fischer: Das wirst du schon.
Wang|Auf Wiedersehen.
Fischer|Fischer: Viel Glück.",
//Quest Diamanten | 12
@"FrauPenyi|Frau Penyi: Ahh, da ist ja mein kleiner. Hast du sie?
Wang|Jaa, ich habe diese sogar selber besorgt.
FrauPenyi|Frau Penyi: Wie denn?
Wang|Der Fischer hatte sie noch nicht und deswegen musste ich sie selbst suchen.
FrauPenyi|Frau Penyi: Das ist aber so lieb von dir.
Wang|Kein Problem, Frau Penyi. Ich hätte eine Frage.
FrauPenyi|Frau Penyi: Was gibt es denn lieber Wang?
Wang|Ich suche eine Unterkunft für die drei wahren Götter.
FrauPenyi|Frau Penyi: Sind sie nun endlich hier?
Wang|Jaa, sie sind unglaublich.
FrauPenyi|Frau Penyi: Es wäre mir eine Ehre diese willkommen zu heißen.
Wang|Sie sind meine Rettung. Ich werde sofort zu ihnen gehen.
FrauPenyi|Frau Penyi: Mach das. Ich freu mich. Endlich mal ein paar Gäste für meine Katzen.
Wang|Ich laufe eben zu ihnen und gebe ihnen Bescheid.
FrauPenyi|Frau Penyi: Mach das Wang, bis gleich.
Wang|Vielen lieben Dank. Ich bin gleich zurück.",
//Wang geht zu Göttern nach 3 Quests | 13
@"Gott|1. Gott: Und lieber Wang, bist du fündig geworden?
Wang|In vielen Häusern war kein Platz mehr,
Wang|doch ich bin fündig geworden.
Gott|1. Gott: Sehr gut. Wo kommen wir unter?
Wang|Bei Frau Penyi, der älteren Katzendame.
Gott2|2. Gott: Dort können wir nicht hin.
Gott2|2. Gott: Ich bin allergisch gegen Katzen.
Wang|Oh nein, das wusste ich nicht.
Gott|1. Gott: Ist auch nicht so schlimm.
Wang|Doch, ich ziehe wieder los und suche etwas neues.
Gott3|3. Gott: Das brauchst du nicht.
Gott2|2. Gott: Jetzt sag es ihm.
Wang|Was ist los?
Gott|1. Gott: Wir haben bereits eine Unterkunft 
Gott|1. Gott: bei einer jungen Dame.
Wang|Wie?
Gott|1. Gott: Sie kam vorbei und wir haben mit ihr gesprochen. 
Gott|1. Gott: Sie war so nett und bot uns eine Unterkunft an.
Gott|1. Gott: So eine freundliche Dame.
Wang|Wer ist es denn?
Gott|1. Gott: Shen Te heißt die junge Frau.
Gott|1. Gott: Sie wohnt gleich dahinten.
Wang|Achso. Sie wirklich eine nette Frau.
Wang|Sie hat immer gerne Besuch.
Gott|1. Gott: Wir hatten nur auf dich gewartet
Gott|1. Gott: und wollten mit dir zu ihr gehen.
Wang|Dann lasst uns rüber gehen damit ihr zur Ruhe kommen könnt.
Gott2|2. Gott: Ja, ich bin auch schon sehr erschöpft.
Gott3|3. Gott: Stell dich nicht so an.
Gott|1. Gott: Dann los.",
//Abgabe | 14
@"ShenTe|Shen Te: Ach da seid ihr ja. Hallo Wang.
Wang|Hallo Shen Te, lange nicht gesehen.
Wang|Ich bin froh, dass du die Götter aufnimmst.
ShenTe|Shen Te: Jaa es ist mir eine Ehre.
ShenTe|Shen Te: Ich freue mich ihnen Einzug zu gewähren.
Wang|Das ist sehr nett von dir.
ShenTe|Shen Te: Es ist schon alles hergerichtet, kommt doch rein.
Gott3|3. Gott: Schön hast du es hier.
Gott2|2. Gott: Wo dürfen wir denn schlafen?
ShenTe|Shen Te: Dort hinten, in dem großen Zimmer.
Gott|1. Gott: Wir sind dir sehr dankbar.
Wang|Ich auch. Ich hatte schon die Hoffnung aufgegeben.
ShenTe|Shen Te: Ich bin froh euch eine Freude zu machen.
Wang|Ich hoffe euch wird es bei Shen Te gefallen und ihr seid zufrieden.
Gott|1. Gott: Das wird alles passen. Du hast dich so sehr bemüht.
Wang|Ich bin einfach nur froh.
Gott|1. Gott: Wir haben gesehen wie viel du auf dich genommen hast.
ShenTe|Shen Te: Sie hatten mir erzählt, wo du überall gewesen bist.
Wang|Ach nicht der Rede wert. Für die Götter würde ich alles tun.
Gott|1. Gott: Das freut uns zu hören. Du bist ein Herzensmensch.
ShenTe|Shen Te: Das habe ich ihnen auch gesagt.
Wang|Jetzt werd ich aber verlegen.
Gott|1. Gott: Wir haben ein Geschenk für dich.
Wang|Für mich?
Gott|1. Gott: Jaa, komm her.
Wang|Danke. Was kann ich damit machen?
Gott|1. Gott: Geh raus und du wirst es sehen.
Gott|1. Gott: Es gibt dir eine neue Möglichkeit.
Wang|Für was?
Gott|1. Gott: Das ganze aus einem neuen Blickwinkel zu sehen.
Wang|Ich verstehe nicht so recht.
Gott|1. Gott: Du wirst nun endlich verstehen,
Gott|1. Gott: wie das Leben von anderen ist.
Wang|Aber warum?
Gott|1. Gott: Du hast es dir verdient. Probier es einfach draußen aus.
Wang|Das werde ich.
Gott|1. Gott: Wir bleiben hier bei Shen Te und ruhen uns aus. Geh ruhig.
ShenTe|Shen Te: Ich werde die Drei schon versorgen. Mach dir keine Sorgen.
Wang|Ich kann euch nicht genug danken.
Gott|1. Gott: Das brauchst du nicht. Wir sehen uns wieder.
ShenTe|Shen Te: Mach dich auf den Weg.
Wang|Wir sehen uns."
 };

    void Start()
    {
        currentText = null;
        UIComponents = new GameObject[2];

        //Lade die Charaktere

        characters["Wang"] = GameObject.Find("Wang");
        characters["Gott"] = GameObject.Find("Gott");
        characters["Gott2"] = GameObject.Find("Gott2");
        characters["Gott3"] = GameObject.Find("Gott3");
        characters["Leng"] = GameObject.Find("Leng");
        characters["Wache"] = GameObject.Find("Wache");
        characters["MrWengo"] = GameObject.Find("MrWengo");
        characters["FrauPeng"] = GameObject.Find("FrauPeng");
        characters["FrauPenyi"] = GameObject.Find("FrauPenyi");
        characters["Fischer"] = GameObject.Find("Fischer");
        characters["ShenTe"] = GameObject.Find("ShenTe");
        _lastConversationPartner = "";

        //Lade den Canvas, auf dem der Text gezeigt wird

        UIText = GameObject.Find("UIText").GetComponent<Text>();
        UIComponents[0] = GameObject.Find("UIText");
        UIComponents[1] = GameObject.Find("TextBoxBackground");
        if (!GameObject.Find("GameHandler").GetComponent<mainGame>().introPlayed)
        {
            //loadText(0);
            // StartCoroutine(AutoPlay());
        }
        else
        {
            playIntro = false;
            // loadText(1);
        }
    }

    public void loadText(int inputFile)
    {
        //Funktion um den hardcoded Text zu laden
        distanceNeeded = 50;
        lines = null;
        lines = gameText[inputFile].Split('\n');
        textStep = 0;
        maxTextStep = lines.GetLength(0) - 1;
        textLoaded = true;
    }

    public IEnumerator AutoPlay()
    {
        //Funtkion die im Intro den Text von alleine ablaufen lässt
        while (textStep < lines.GetLength(0))
        {
            yield return new WaitForSeconds(3);
            if (lines[textStep + 1].Contains("LoadNext"))
            {
                // Debug.Log(lines[lines.GetLength(0) - 1].Split('|')[1]);
                // loadText(1);
                textStep = 0;
                playIntro = false;
                lines = null;
                UIComponents[0].GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
                // UIComponents[1].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                StopCoroutine(AutoPlay());
                break;
            }
            else
            {
                textStep++;
            }
        }
    }

    public IEnumerator UnloadText()
    {
        //Text entladen. Hier wird der playerLocked Status wider auf false gesetzt, damit der Spieler sich nach einem Gespräch wieder bewegen kann, aber nicht während dem Gespräch
        yield return new WaitForSeconds(1);
        GameObject.Find("GameHandler").GetComponent<mainGame>().playerLocked = false;
        textLoaded = false;
    }

    void Update()
    {
        //Wenn Text geladen ist
        if (lines != null)
        {
            //Finde Spieler/Wang und Partner
            GameObject _currentChar = (GameObject)characters["Wang"];
            GameObject _otherChar;

            //In der Textzeile steht immer vor dem | Symbol, mit wem Wang spricht
            //Hier wird behandelt, ob Wang z.B. mit sich selbst spricht (im Intro)
            try
            {
                 _otherChar = (GameObject)characters[lines[textStep].Split('|')[0]];
            }
            catch {
                _otherChar = (GameObject)characters["Wang"];
            }

            //Setze Partner auf Wang und überschreibt Wang, falls er nicht mit sich selbst spricht            
            string _conversationPartner = "Wang";
            if (lines[textStep].Contains(":"))
            {
                _conversationPartner = lines[textStep].Split('|')[1].Split(':')[0];
            }

            //Ändert die Farbe der Text-Outline, wenn die sprechende Person wechselt
            if (_conversationPartner != _lastConversationPartner)
            {
                foreach (string line in colors.Split('\n'))
                {
                    if (line.Split(':')[0].Contains(_conversationPartner))
                    {
                        //Parse HexColor zu RGBA
                        string _hexcolor = line.Split(':')[1].Substring(1);
                        UIComponents[0].GetComponent<Outline>().effectColor =
                            new Color(
                                (float)(int.Parse(_hexcolor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber)),
                                (float)(int.Parse(_hexcolor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber)),
                                (float)(int.Parse(_hexcolor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)),
                                UIComponents[0].GetComponent<Text>().color.a);
                        break;
                    }
                }
                _lastConversationPartner = _conversationPartner;
            }


            //Zeige Text nur an, wenn Spieler in Reichweite von Gesprächspartner
            //Notiz: Eigentlich eine übriggebliebene Funktion, die nützlich war, als playerLocked noch nicht verwendet wurde
            //Hat den Alphawert RGBA erhöht, wenn Spieler in der Nähe (also Text -> sichtbar) und reduziert, wenn Spieler nicht in der Nähe
            if (!playIntro && textLoaded)
            {
                if (Vector3.Distance(_currentChar.transform.position, _otherChar.transform.position) < distanceNeeded)
                {
                    if (XButtonState != Input.GetButton("XButton") && !playIntro)
                    {
                        if (!XButtonState)
                        {
                            if (textStep < maxTextStep)
                            {
                                GameObject.Find("GameHandler").GetComponent<mainGame>().playerLocked = true;
                                textStep += 1;
                            }
                            else
                            {
                                StartCoroutine(UnloadText());
                                textLoaded = false;
                            }
                        }

                        XButtonState = !XButtonState;
                    }

                    if (UIComponents[0].GetComponent<Text>().color.a < 1f)
                    {
                        UIComponents[0].GetComponent<Text>().color = new Color(1f, 1f, 1f, UIComponents[0].GetComponent<Text>().color.a + Time.deltaTime);
                    }
                }
                else
                {
                    if (UIComponents[0].GetComponent<Text>().color.a > 0f)
                    {
                        UIComponents[0].GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
                    }
                }
            }

            //Wenn noch Text übrig, zeige den aktuellen Text auf dem Canvas an
            //Ansonsten, wenn der Text fertig gelesen wurde, unlocke den Spieler
            if (textStep < maxTextStep)
            {
                UIText.text = lines[textStep].Split('|')[1];
            }
            else
            {
                GameObject.Find("GameHandler").GetComponent<mainGame>().playerLocked = false;
                distanceNeeded = 0;
            }
        }
    }
}