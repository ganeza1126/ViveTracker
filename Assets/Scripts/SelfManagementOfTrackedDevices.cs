using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SelfManagementOfTrackedDevices : MonoBehaviour
{
    public GameObject[] targetObjs;
    public ETrackedDeviceClass targetClass = ETrackedDeviceClass.GenericTracker;
    public KeyCode resetDeviceIds = KeyCode.Tab;

    CVRSystem _vrSystem;
    List<int> _validDeviceIds = new List<int>();

    //add
    //controllerの情報
    public SteamVR_Controller.Device ctrl0;
    ///???
    protected Vector2 TouchPadValue => ctrl0.GetAxis();
    protected float TriggerValue => ctrl0.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

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
            Debug.Log("!!!!!device class is " + deviceClass + i);
            if (deviceClass != ETrackedDeviceClass.Invalid && deviceClass == targetClass)
            {
                Debug.Log("OpenVR device at " + i + ": " + deviceClass);
                _validDeviceIds.Add((int)i);
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
                //ここから位置トラック
                var pose = allPoses[_validDeviceIds[i]];
                var absTracking = pose.mDeviceToAbsoluteTracking;
                var mat = new SteamVR_Utils.RigidTransform(absTracking);
                targetObjs[i].transform.SetPositionAndRotation(mat.pos, mat.rot);

                //add
                //コントローラーのとき、ボタンの情報を取得
                var deviceClass = _vrSystem.GetTrackedDeviceClass((uint)_validDeviceIds[i]);
                if(deviceClass == ETrackedDeviceClass.Controller)
                {
                    if(ctrl0 == null)
                        ctrl0 = SteamVR_Controller.Input(_validDeviceIds[i]);
                    if(ctrl0 != null) {
                        ControllerFunction(ctrl0);
                    }
                }

            }
        }


    }


    void Update()
    {
        UpdateTrackedObj();

        if(Input.GetKeyDown(resetDeviceIds)){
            SetDeviceIds();
        }

        //終了と開始を振動で伝える
        if ((Input.GetKeyDown(KeyCode.F)|| Input.GetKeyDown(KeyCode.S)) && ctrl0!=null)
        {
            Pulse(2000, ctrl0);
        }
    }

//   protected void Pulse(ushort pulse, SteamVR_Controller.Device device)
   public void Pulse(ushort pulse, SteamVR_Controller.Device device)
   {
       device.TriggerHapticPulse(pulse);
   }

   private void ControllerFunction(SteamVR_Controller.Device device)
   {
       if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
       {
           TriggerTouchDown();
//           Debug.Log("トリガーを浅く引いた");
       }
       if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
       {
           TriggerPressDown(device);
//           Debug.Log("トリガーを深く引いた");
       }
       if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
       {
           TriggeTouchUp();
//           Debug.Log("トリガーを離した");
       }
       if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
       {
           TouchpadPressDown();
//           Debug.Log("タッチパッドをクリックした");
       }
       if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
       {
           TouchpadPress();
//           Debug.Log("タッチパッドをクリックしている");
       }
       if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
       {
           TouchpadPressUp();
//           Debug.Log("タッチパッドをクリックして離した");
       }
       if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
       {
           TouchpadTouchDown();
//           Debug.Log("タッチパッドに触った");
       }
       if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
       {
           TouchpadTouchUp();
//           Debug.Log("タッチパッドを離した");
       }
       if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
       {
           ApplicationMenuPressDown();
//           Debug.Log("メニューボタンをクリックした");
       }
       if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
       {
           GripPressDown();
//           Debug.Log("グリップボタンをクリックした");
       }

       if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
       {
           TriggerTouch();
//           Debug.Log("トリガーを浅く引いている");
       }
       if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
       {
           TriggerPress();
//           Debug.Log("トリガーを深く引いている");
       }
       if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
       {
           TouchPadTouch();
           //Debug.Log("タッチパッドに触っている");
       }
   }


   protected virtual void TouchPadTouch()
   {
       return;
   }

   protected virtual void TouchpadPressDown()
   {
       return;

   }
   protected virtual void TriggerPress()
   {
       return;
   }


   protected virtual void TriggerTouch()
   {
       return;
   }

   protected virtual void GripPressDown()
   {
       return;
   }

   protected virtual void ApplicationMenuPressDown()
   {
       return;
   }


   protected virtual void TouchpadTouchUp()
   {
       return;
   }


   protected virtual void TouchpadPressUp()
   {
       return;
   }


   protected virtual void TouchpadPress()
   {
       return;
   }


   protected virtual void TouchpadTouchDown()
   {
       return;
   }


   protected virtual void TriggeTouchUp()
   {
       return;
   }


   protected virtual void TriggerPressDown(SteamVR_Controller.Device device)
   {
//        Pulse(3200, device);
       return;
   }


   protected virtual void TriggerTouchDown()
   {
       return;
   }
}