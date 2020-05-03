using UnityEngine;
using Tobii.Gaming;

public class SceneController : MonoBehaviour
{
    // public objects
    public OSC osc;
    public TextMesh sceneText;
    public Material selectedMaterial;
    public Material deselectedMaterial;
    public Material noTobiiMaterial;

    // used to lerp between materials
    new Renderer renderer;
    
    // eye tracker
    private GazeAware gazeAwareComponent;

    // use timer to track how long we are looking in seconds
    private float gazeTimeElasped = 0.0f;
    private float gazeTimeStarted = 0.0f;
    private const float GAZE_TIME = 2.0f;
    private bool gazeStarted = false;
    private bool messageSent = false;

    // track the scene which we are in
    private int sceneNumber;

    // Start is called before the first frame update
    void Start()
    {
        // setup the rederer for lerping between colors
        renderer = GetComponent<Renderer>();

        // setup the scene changing object to be gaze aware
        gazeAwareComponent = GetComponent<GazeAware>();

        // receive the OSC messages
        osc.SetAddressHandler("/scene", OnReceive);


    }

    // Update is called once per frame
    void Update()
    {
        // See if we are gazing on this object
        if (gazeAwareComponent.HasGazeFocus)
        {
            // set the time started to the current time when the gaze has begun
            if (gazeStarted == false)
            {
                gazeTimeStarted = Time.time;
                gazeStarted = true;
            }

            // start changing the color of the head
            if (gazeTimeElasped < GAZE_TIME)
            {
                // set the elapsed time
                gazeTimeElasped = Time.time - gazeTimeStarted;

                // lerp the material color to show that it is about to be selected
                float lerpTime = Mathf.PingPong(gazeTimeElasped, GAZE_TIME) / GAZE_TIME;
                renderer.material.Lerp(deselectedMaterial, selectedMaterial, lerpTime);
            }

            if (gazeTimeElasped > GAZE_TIME)
            {
                // send scene request via OSC
                if (messageSent == false)
                {
                    // send scene request to Max
                    // Max will send back a scene number to confirm the change has taken place
                    OscMessage sceneRequest = new OscMessage();
                    sceneRequest.address = "/sceneRequest";
                    sceneRequest.values.Add("bang");
                    osc.Send(sceneRequest);
                    messageSent = true;
                }
            }

        }
        else
        {
            // change back to deselected material depending on if eye tracker is connected
            if (TobiiAPI.IsConnected)
            {
                renderer.material = deselectedMaterial;
            }
            else
            {
                renderer.material = noTobiiMaterial;
            }

            // reset the gazeStarted flag, gazeTimeElapsed, and messageSent flag
            gazeStarted = false;
            gazeTimeElasped = 0.0f;
            messageSent = false;
        }
    }


    // stub that is called when receiving an OSC /scene message
    void OnReceive(OscMessage message)
    {
        // get the scene number
        sceneNumber = message.GetInt(0);

        // update the scene number on the text
        sceneText.text = sceneNumber.ToString();

    }
}
