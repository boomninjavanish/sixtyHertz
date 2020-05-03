using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadOSC : MonoBehaviour
{
    // setup open sound control 
    public OSC osc;

    void Update()
    {
        // start OSC message object
        OscMessage message = new OscMessage
        {
            // set the osc addresses to the head's name
            address = "/" + this.name
        };

        // send the y rotation value via OSC
        //convert to int because Max had difficulty with precise floats
        message.values.Add((int) (this.transform.rotation.y * 10000));
        osc.Send(message);
        
    }
}
