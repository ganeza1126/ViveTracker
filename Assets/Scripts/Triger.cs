using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class Triger : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(trackedObj);
//            var device = SteamVR_Controller.Input((int)trackedObj.index);
//		
//            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
//            {
//                Debug.Log("GetPressDown Trigger");		
//            }
    }
}
