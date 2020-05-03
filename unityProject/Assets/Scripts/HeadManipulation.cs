using UnityEngine;
using Tobii.Gaming;

public class HeadManipulation : MonoBehaviour
{
    // used to check if objects is being gazed upon
    private GazeAware gazeAwareComponent;

    // numbers to track how long user is gazing
    // object will become fully selected after timer has elapsed
    private float gazeTime;
    private float gazeTimeElasped = 0.0f;
    private float gazeTimeStarted = 0.0f;
    private bool gazeStarted = false;

    // used to lerp between materials
    new Renderer renderer;

    // map to the the head that this script attaches to
    public GameObject head;

    // change to how responsive the movement to be
    public float responsiveness;

    // materials to use for object when selected and deselected
    public Material selectedMaterial, deselectedMaterial, noTobiiMaterial;    

    // Start is called before the first frame update
    void Start()
    {
        // setup the rederer for lerping between colors
        renderer = GetComponent<Renderer>();
        
        // get gaze aware objects 
        gazeAwareComponent = GetComponent<GazeAware>();

        // set the gaze time and responsiveness to the vars in playerprefs
        gazeTime = PlayerPrefs.GetFloat("headGazeTime");
        responsiveness = PlayerPrefs.GetFloat("headResponseTime");

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
            if (gazeTimeElasped < gazeTime)
            {
                // set the elapsed time
                gazeTimeElasped = Time.time - gazeTimeStarted;

                // lerp the material color to show that it is about to be selected
                float lerpTime = Mathf.PingPong(gazeTimeElasped, gazeTime) / gazeTime;
                renderer.material.Lerp(deselectedMaterial, selectedMaterial, lerpTime);
            }

            if (gazeTimeElasped > gazeTime)
            {
                // take the head pose information from the user
                var headPose = TobiiAPI.GetHeadPose();

                // translate the head pose to a position for rotation of head
                if (headPose.IsRecent())
                {
                    // filter out x and z axis from obeject movement
                    Quaternion headPoseNoXZaxis = Quaternion.Euler(0, headPose.Rotation.eulerAngles.y, 0);

                    // invert axes from head position
                    headPoseNoXZaxis = Quaternion.Inverse(headPoseNoXZaxis);

                    // move the head based on y axis movement
                    head.transform.rotation = Quaternion.Lerp(head.transform.rotation, headPoseNoXZaxis, Time.unscaledDeltaTime * responsiveness);
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

            // reset the gazeStarted flag & gazeTimeElapsed
            gazeStarted = false;
            gazeTimeElasped = 0.0f;
        }


    }
}
