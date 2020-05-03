using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class CubeManipulation : MonoBehaviour
{
    // used to check if objects is being gazed upon
    private GazeAware gazeAwareComponent;

    // map to the the cube that this script attaches to
    public GameObject cube;

    // change to how responsive the movement to be
    public float responsiveness = 10f;

    // materials to use for object when selected and deselected
    public Material selectedMaterial;
    public Material deselectedMaterial;
    public Material noTobiiMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // get gaze aware objects 
        gazeAwareComponent = GetComponent<GazeAware>();
    }

    // Update is called once per frame
    void Update()
    {
        // See if we are gazing on this object
        if (gazeAwareComponent.HasGazeFocus)
        {
            // change the material to show that it is selected
            cube.GetComponent<Renderer>().material = selectedMaterial;

            // take the head pose information from the user
            var headPose = TobiiAPI.GetHeadPose();

            // translate the head pose to a position for rotation of cube
            if (headPose.IsRecent())
            {
               cube.transform.rotation = Quaternion.Lerp(cube.transform.rotation, headPose.Rotation, Time.unscaledDeltaTime * responsiveness);
            }
        }
        else
        {
            // change back to deselected material depending on if eye tracker is connected
            if (TobiiAPI.IsConnected)
            {
                cube.GetComponent<Renderer>().material = deselectedMaterial;
            }
            else
            {
                cube.GetComponent<Renderer>().material = noTobiiMaterial;
            }
        }


    }
}