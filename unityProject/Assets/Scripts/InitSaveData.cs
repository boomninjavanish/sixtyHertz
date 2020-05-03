using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSaveData : MonoBehaviour
{

    // for saving the OSC data
    public OSC osc;
    private UDPPacketIO uDPPacketIO;



    // Start is called before the first frame update
    void Start()
    {
        // init gameobject that accesses the OSC script vars
        GameObject oscScript = GameObject.Find("OSC");

        // make the save data

        /*         ipAdddressListen          */
        if (PlayerPrefs.HasKey("ipAddressListen"))
        {
            // store value into OSC script
            oscScript.GetComponent<OSC>().outIP = PlayerPrefs.GetString("ipAddressListen");
            
        } else
        {
            // init value and make playerpref
            oscScript.GetComponent<OSC>().outIP = "127.0.0.1";
            PlayerPrefs.SetString("ipAddressListen", oscScript.GetComponent<OSC>().outIP);
        }

        /*         ipPortListen          */
        if (PlayerPrefs.HasKey("ipPortListen"))
        {
            // store value into OSC script
            //oscScript.GetComponent<OSC>().inPort = PlayerPrefs.GetInt("ipPortListen");
            //osc.inPort = PlayerPrefs.GetInt("ipPortListen");
            
        }
        else
        {
            // init value and make playerpref
            oscScript.GetComponent<OSC>().inPort = 9999;
            PlayerPrefs.SetInt("ipPortListen", oscScript.GetComponent<OSC>().inPort);
        }

        /*         ipPortTransmit          */
        if (PlayerPrefs.HasKey("ipPortTransmit"))
        {
            // store value into OSC script
            oscScript.GetComponent<OSC>().outPort = PlayerPrefs.GetInt("ipPortTransmit");
        }
        else
        {
            // init value and make playerpref
            oscScript.GetComponent<OSC>().outPort = 8888;
            PlayerPrefs.SetInt("ipPortTransmit", oscScript.GetComponent<OSC>().outPort);
        }

        /*         headResponseTime          */
        if (!PlayerPrefs.HasKey("headResponseTime"))
        {
            // store default value into playerpref
            PlayerPrefs.SetFloat("headResponseTime", 10f);
        }

        /*         headGazeTime          */
        if (!PlayerPrefs.HasKey("headGazeTime"))
        {
            // store default value into playerpref
            PlayerPrefs.SetFloat("headGazeTime", 0.5f);
        }

        /*         sceneGazeTime          */
        if (!PlayerPrefs.HasKey("sceneGazeTime"))
        {
            // store default value into playerpref
            PlayerPrefs.SetFloat("sceneGazeTime", 3f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
