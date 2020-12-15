using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TrackerAndController : MonoBehaviour
{

    public GameObject[] targetObjs;
    public ETrackedDeviceClass targetClasst = ETrackedDeviceClass.GenericTracker;
    //add
    public ETrackedDeviceClass targetClassc = ETrackedDeviceClass.Controller;
    //add
    public KeyCode resetDeviceIds = KeyCode.Tab;

    CVRSystem _vrSystem;
    List<int> _validDeviceIds = new List<int>();

    void Start()
    {
        var error = EVRInitError.None;
        _vrSystem = OpenVR.Init(ref error, EVRApplicationType.VRApplication_Other);

        if (error != EVRInitError.None) { Debug.LogWarning("Init error: " + error); }

        else
        {
            Debug.Log("init done");
            foreach (var item in targetObjs) { item.SetActive(false); }
            SetDeviceIds();
        }
    }

    void SetDeviceIds()
    {
        _validDeviceIds.Clear();
        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            var deviceClass = _vrSystem.GetTrackedDeviceClass(i);
            //tracker
            if (deviceClass != ETrackedDeviceClass.Invalid && deviceClass == targetClasst)
            {
                Debug.Log("OpenVR device at " + i + ": " + deviceClass);
                _validDeviceIds.Add((int)i);
                Debug.Log(targetObjs[_validDeviceIds.Count - 1]);
                targetObjs[_validDeviceIds.Count - 1].SetActive(true);
            }
            //controller
            if (deviceClass != ETrackedDeviceClass.Invalid && deviceClass == targetClassc)
            {
                Debug.Log("OpenVR device at " + i + ": " + deviceClass);
                _validDeviceIds.Add((int)i);
                Debug.Log(targetObjs[_validDeviceIds.Count - 1]);
                targetObjs[_validDeviceIds.Count - 1].SetActive(true);
            }
        }
    }

    void UpdateTrackedObj()
    {
        TrackedDevicePose_t[] allPoses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];

        _vrSystem.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0, allPoses);

        for (int i = 0; i < _validDeviceIds.Count; i++)
        {
            if (i < targetObjs.Length)
            {
                var pose = allPoses[_validDeviceIds[i]];
                var absTracking = pose.mDeviceToAbsoluteTracking;
                var mat = new SteamVR_Utils.RigidTransform(absTracking);
                targetObjs[i].transform.SetPositionAndRotation(mat.pos, mat.rot);
            }
        }
    }

    void Update()
    {
        UpdateTrackedObj();

        if(Input.GetKeyDown(resetDeviceIds)){
            SetDeviceIds();
        }
    }
}
