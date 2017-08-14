using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInput : MonoBehaviour
{

    float horizontal;
    float vertical;
    float rhorizontal;
    float rvertical;
    float movement;
    Quaternion direction;
    public float speed = 1;
    GameObject player;
    GameObject mainCamera;

    Vector3 rotationvector = new Vector3(1, 0, 1);

    private Text debugText;

    private Quaternion locRot;
    GameObject gameHandler;
    mainGame mainScript;

    void Start()
    {
        player = GameObject.Find("Wang");
        mainCamera = GameObject.Find("Main Camera");
        locRot = new Quaternion(0, 0.707f, 0, 0.707f);
        debugText = GameObject.Find("VRRotationText").GetComponent<Text>();

        gameHandler = GameObject.Find("GameHandler");
        mainScript = gameHandler.GetComponent<mainGame>();
    }

    void Update()
    {
        if (mainScript.introPlayed && !mainScript.playerLocked)
        {
            //Da das Spiel in VR laufen sollte, wird hier die Position des VR Headsets gelesen und damit die Position berechnet
#if UNITY_ANDROID
            locRot = UnityEngine.VR.InputTracking.GetLocalRotation(UnityEngine.VR.VRNode.CenterEye);
#endif

            //Lese Achsen des Controllers aus, um den Spieler zu bewegen
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");    

            direction = locRot;
            movement = Time.deltaTime * speed;

            //Falls das Spiel im Unity Editor läuft, wird Tastatur und Maussteuerung erlaubt
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.W)) {
                vertical = 1;
            }
            if (Input.GetKey(KeyCode.S)) {
                vertical = -1;
            }
            if (Input.GetKey(KeyCode.A)) {
                horizontal = -1;
            }
            if (Input.GetKey(KeyCode.D)) {
                horizontal = 1;
            }
            direction = mainCamera.transform.rotation;
            mainCamera.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0));
            float z = mainCamera.transform.eulerAngles.z;
            mainCamera.transform.Rotate(0, 0, -z);
#endif

            //Behandle die Fälle: Vorwärts, Rückwärts, Links, Rechts
            //Hier wird die Rotation des VR-Headsets abhängig von der Achsenbewegung des Controllers genommen
            //Dadurch bewegt sich der Spieler z.B. mit vorwärts immer in die Richtung, in die er guckt
            if (horizontal > 0)
            {
                player.transform.position += direction * Vector3.right * movement;
            }
            if (horizontal < 0)
            {
                player.transform.position += direction * Vector3.left * movement;
            }
            if (vertical > 0)
            {
                player.transform.position += direction * Vector3.forward * movement;
            }
            if (vertical < 0)
            {
                player.transform.position += direction * Vector3.back * movement;
            }
        }
    }
}
