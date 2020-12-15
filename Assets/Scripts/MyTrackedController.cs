using UnityEngine;

namespace VIVEHelper
{
    public class MyTrackedController : MonoBehaviour
    {
        public SteamVR_TrackedObject Controller;
        private SteamVR_Controller.Device device;

        protected Vector2 TouchPadValue => device.GetAxis();
        protected float TriggerValue => device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

        // Use this for initialization
        private void Start()
        {
//            if (Controller)
//                device = SteamVR_Controller.Input((int)Controller.index);
                device = SteamVR_Controller.Input(1);
            Debug.Log("########################" + device);
            

        }

        // Update is called once per frame
        protected void Update()
        {
            if (device != null)
            {
                ControllerFunction(device);
                Debug.Log("umakuitteruyo");
            }
//            else if (Controller)
//                device = SteamVR_Controller.Input((int)Controller.index);
        }

        protected void Pulse(ushort pulse)
        {
            device.TriggerHapticPulse(pulse);
        }

        private void ControllerFunction(SteamVR_Controller.Device device)
        {
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                TriggerTouchDown();
                //Debug.Log("トリガーを浅く引いた");
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                TriggerPressDown();
                //Debug.Log("トリガーを深く引いた");
            }
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                TriggeTouchUp();
                //Debug.Log("トリガーを離した");
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                TouchpadPressDown();
                //Debug.Log("タッチパッドをクリックした");
            }
            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                TouchpadPress();
                //Debug.Log("タッチパッドをクリックしている");
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                TouchpadPressUp();

                //Debug.Log("タッチパッドをクリックして離した");
            }
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                TouchpadTouchDown();

                Debug.Log("タッチパッドに触った");
            }
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                TouchpadTouchUp();

                //Debug.Log("タッチパッドを離した");
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
            {
                ApplicationMenuPressDown();
                //Debug.Log("メニューボタンをクリックした");
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                GripPressDown();
                //Debug.Log("グリップボタンをクリックした");
            }

            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                TriggerTouch();
                //Debug.Log("トリガーを浅く引いている");
            }
            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                TriggerPress();
                //Debug.Log("トリガーを深く引いている");
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


        protected virtual void TriggerPressDown()
        {
            return;
        }


        protected virtual void TriggerTouchDown()
        {
            return;
        }
    }
}
/*
using UnityEngine;
using System.Collections;

public class MyTrackedController : MonoBehaviour
{ 
    SteamVR_TrackedController trackedController;

    void Start () {
        trackedController = gameObject.GetComponent<SteamVR_TrackedController> ();

        if (trackedController == null) {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController> ();
        }
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!" + trackedController);
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaa" + );

        trackedController.MenuButtonClicked += new ClickedEventHandler (DoMenuButtonClicked);
        trackedController.MenuButtonUnclicked += new ClickedEventHandler (DoMenuButtonUnClicked);
        trackedController.TriggerClicked += new ClickedEventHandler (DoTriggerClicked);
        trackedController.TriggerUnclicked += new ClickedEventHandler (DoTriggerUnclicked);
        trackedController.SteamClicked += new ClickedEventHandler (DoSteamClicked);
        trackedController.PadClicked += new ClickedEventHandler (DoPadClicked);
        trackedController.PadUnclicked += new ClickedEventHandler (DoPadClicked);
        trackedController.PadTouched += new ClickedEventHandler (DoPadTouched);
        trackedController.PadUntouched += new ClickedEventHandler (DoPadTouched);
        trackedController.Gripped += new ClickedEventHandler (DoGripped);
        trackedController.Ungripped += new ClickedEventHandler (DoUngripped);
    }

    public void DoMenuButtonClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoMenuButtonClicked");
    }

    public void DoMenuButtonUnClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoMenuButtonUnClicked");
    }

    public void DoTriggerClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoTriggerClicked");
    }

    public void DoTriggerUnclicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoTriggerUnclicked");
    }

    public void DoSteamClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoSteamClicked");
    }

    public void DoPadClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoPadClicked");
    }

    public void DoPadUnclicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoPadUnclicked");
    }

    public void DoPadTouched(object sender, ClickedEventArgs e) {
        Debug.Log ("DoPadTouched");
    }

    public void DoPadUntouched(object sender, ClickedEventArgs e) {
        Debug.Log ("DoPadUntouched");
    }

    public void DoGripped(object sender, ClickedEventArgs e) {
        Debug.Log ("DoGripped");
    }

    public void DoUngripped(object sender, ClickedEventArgs e) {
        Debug.Log ("DoUngripped");
    }
}
*/
