using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetupMenuLoadData : MonoBehaviour
{
    public InputField inputFieldIpAddress, inputFieldPortListen, inputFieldPortTransmit;
    public Slider sliderHeadResponseTime, sliderHeadGazeTime, sliderSceneGazeTime;
    public Text textHeadResponseTimeValue, textHeadGazeTimeValue, textSceneGazeTimeValue;

    private string ipAddress;
    private int portListen, portTransmit;
    private float headGazeTime, headResponseTime, sceneGazeTime;

    // Start is called before the first frame update
    void Start()
    {
        // load all values to input fields from PlayerPrefs
        ipAddress = PlayerPrefs.GetString("ipAddressListen");
        inputFieldIpAddress.text = ipAddress;

        portListen = PlayerPrefs.GetInt("ipPortListen");
        inputFieldPortListen.text = portListen.ToString();

        portTransmit = PlayerPrefs.GetInt("ipPortTransmit");
        inputFieldPortTransmit.text = portTransmit.ToString();

        // use to round slider vals to two decimal places
        float mult = Mathf.Pow(10.0f, 2.0f);

        headResponseTime = PlayerPrefs.GetFloat("headResponseTime");
        sliderHeadResponseTime.value = headResponseTime;        
        textHeadResponseTimeValue.text = (Mathf.Round(headResponseTime * mult) / mult).ToString();

        headGazeTime = PlayerPrefs.GetFloat("headGazeTime");
        sliderHeadGazeTime.value = headGazeTime;
        textHeadGazeTimeValue.text = (Mathf.Round(headGazeTime * mult) / mult).ToString();

        sceneGazeTime = PlayerPrefs.GetFloat("sceneGazeTime");
        sliderSceneGazeTime.value = sceneGazeTime;
        textSceneGazeTimeValue.text = (Mathf.Round(sceneGazeTime * mult) / mult).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveValues()
    {
        // save all values to PlayerPrefs
        ipAddress = inputFieldIpAddress.text;
        PlayerPrefs.SetString("ipAddressListen", ipAddress);

        portListen = int.Parse(inputFieldPortListen.text);
        PlayerPrefs.SetInt("ipPortListen", portListen);

        portTransmit = int.Parse(inputFieldPortTransmit.text);
        PlayerPrefs.SetInt("ipPortTransmit", portTransmit);

        headResponseTime = sliderHeadResponseTime.value;
        PlayerPrefs.SetFloat("headResponseTime", headResponseTime);

        headGazeTime = sliderHeadGazeTime.value;
        PlayerPrefs.SetFloat("headGazeTime", headGazeTime);

        sceneGazeTime = sliderSceneGazeTime.value;
        PlayerPrefs.SetFloat("sceneGazeTime", sceneGazeTime);

        // exit to MainScene
        ExitToMainScene();
    }

    public void ExitToMainScene()
    {
        SceneManager.LoadSceneAsync("mainScene");
        
        // only unload the scene when mainScene has been loaded
        if (SceneManager.GetSceneByName("mainScene").isLoaded)
            SceneManager.UnloadSceneAsync("menuScene");

    }
    
    // quit to desktop
    public void QuitToDesktop()
    {

        SaveValues(); 
        Application.Quit();
    }

}
