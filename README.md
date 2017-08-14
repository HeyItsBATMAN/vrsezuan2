# VRSezuan - Eine Theaterinterpretation in Virtual Reality
## Kurs bei Jan Wieners an der Universität Köln

### 1. Sprint 03.05.2017-10.05.2017
- Unity Projekt importieren (Niclas Unity/ Kai Unreal)
- Unity und Unreal auf VR Features testen (Niclas Unity/ Kai Unreal)
- Interaktions Konzepte für Story (Jason und Bene)
- Welt begutachten auf was noch hinzugefügt werden kann (Thomas)
- Trello Tickets (Thomas)

### 2. Sprint 10.05.2017-17.05.2017
- Komplettes Projekt in Unity und Unreal laden und auf Smartphone kompilieren (Kai/Niclas)
- Performance Test am 17.05.2017 um Entscheidung für Technologie zu treffen (Unreal/Unity)
- Auf top-down Ansicht der Map die Interaktionspunkte und Storyelemente notieren (Bene/Jason)
- Bergübergänge und Details (Thomas)

### 3. Sprint 17.05.2017-24.05.2017
- Komplettes Projekt in Unity und Unreal laden und auf Smartphone kompilieren (Kai/Niclas)
	---> verlängert bis 20.05.2017
- Performance Test am 21.05.2017 um Entscheidung für Technologie zu treffen (Unreal/Unity) ---> verlängert
- Dialoge der Quests schreiben (Jason)
- Markup für Skripte der Questreihe und des ganzen Stücks (Bene)
- Items für Quests designen in MagicaVoxel (Thomas)

### 4. Sprint 24.05.2017-31.05.2017
- Freemode Dialoge schreiben/ Überarbeitung der Dialoge (Jason)
- Welt letzte Details einfügen (Bene/Thomas)
- Dieb und Fischer (Niclas)
- Ersten Part des Stücks scripten bis “Mainquest Start” (Kai)
- Unity updaten bzw. installieren und Git mit Unity einrichten (Alle)
- Android Emulator ausprobieren (Thomas) 

### 5. Sprint 31.05.2017.-07.06.2017
- Chunks bearbeiten für Unity Import (Skype Konferenz mit Tutorial von Kai)
- Präsentation erstellen (Jason)
- Controller informieren über Kompatibilität/Anschaffung
- Text einbauen und Stück scripten
- finale Dialoge (Jason)
- Figuren Entwicklung (Wang/Götter) (Niclas)
- Angel designen (Niclas)
- Inneneinrichtung (Jason/Niclas/Bene) Frame auf 16 = 1 Meter
- Daylight System in Unity testen (Thomas)

### 6. Sprint 21.06.2017-05.07.2017
- Text einbauen ins Projekt
- Questsystem
- Collision
- Items in die Welt einfügen
- Tempel Boden fixen


## Screenshots
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/EditorScreen.PNG "Unity Editor")
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/EditorUnityScriptSystem.PNG "Unity Script System und Game Handler")
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/Screenshot_2017-07-11-15-17-47.png "VR Intro")
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/Screenshot_2017-07-11-15-18-09.png "VR vor den Göttern")
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/Screenshot_2017-07-11-15-24-58.png "VR im Gespräch mit den Göttern")
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/Screenshot_2017-07-11-15-25-20.png "VR vor einem Haus")
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/Screenshot_2017-07-11-15-25-40.png "VR in dem Haus")
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/Screenshot_2017-07-11-15-26-09.png "VR auf dem Markt")
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/Screens/Screenshot_2017-07-11-15-26-27.png "VR vor dem Tempel")


# Finales Projekt für Abgabe

Das Repository enthält eine kompilierte Android Version, sowie einen Unity-Projektordner

## Möglichkeit 1:

Um das Spiel auf Android zu benutzen wird ein Android 6.0 Gerät, sowie ein Bluetooth Controller benötigt.
Die APK befindet sich in:

### vrsezuan2/SezuanUnity/VRSezuan.apk

## Möglichkeit 2:

## Hinweis: Projekt wurde mit Unity 5.6.1f gebaut, andere Versionen wurden nicht getestet

Um das Projekt mit Unity zu öffnen, einfach in der Unity-Projektauswahl auf "OPEN" und den *vrsezuan2* (Repository) Ordner auswählen
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/TutProjekt/projekt%20laden%201.png "Unity Projektmanager")
Der erste Start kann einige Minuten in Anspruch nehmen. Nach dem Öffnen ist eine leere Szene zu sehen. Um die Sezuan Szene zu öffnen in dem Dateibrowser von Unity in dem Ordner **Assets** einen Doppelklick auf die Szene **VRSezuan** tätigen
![alt text](https://raw.githubusercontent.com/HeyItsBATMAN/vrsezuan2/master/TutProjekt/projekt%20laden%202.png "Unity Szene öffnen")

Nachdem die Szene geladen wurde, kann man direkt Play drücken.
Um das Intro zu aktivieren, kann man in der Unity-Hierarchie das Objekt **"GameHandler"** auswählen und die Checkbox bei **Intro Played** rausnehmen

## Was gibt es zu entdecken?

Zwei von den Gebäuden kann man betreten: den Tempel und an der Seite vom Platz das größere Haus mit dem grünen Dach.
Außerdem ist abhängig davon wo man sich auf der Karte befindet ein anderes Theme zu hören. Das Theme das im Tempel läuft wurde von unserem Gruppenmitglied Thomas Schiffer komponiert und eingespielt.
Einige verschiedene Geräusche gibt es zu entdecken. Die größte Soundkulisse kann man beim Fischerhaus hören.

## Wie funktioniert das Ganze?

Die Technik (abgesehen von Unity) kann man sich im Programmcode angucken. Alle Script-Dateien befinden sich in:

### vrsezuan2/SezuanUnity/assers/Script/*

Die Scripts sind in C# geschrieben und alles was wichtig ist für die Funktionsweise wurde kommentiert (Kommentare fangen in einer Zeile mit // an)
