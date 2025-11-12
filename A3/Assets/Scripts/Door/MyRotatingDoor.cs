using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRotatingDoor : MonoBehaviour, ISlideDoor
{
    public enum DoorStates
    {
        Open,
        Close,
    }

    public DoorStates doorState;

    float HowLongWaitToOpenDoor = 1.5f;
    float TimeDoorOpen = 1.5f;
    public float DoorSpeed = 10;

    public float openAngle = 90.0f; // Angle to open the door by
    float smooth = 0.2f; // Smoothness of the door opening animation

    public bool DoorOpened = false;

    public Transform pivot;

    private Quaternion defaultRotation;
    private Quaternion openRotation;

    float elapsedTime_;

    // Start is called before the first frame update
    void Start()
    {
        // Save the default and open rotations of the door
        defaultRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // FSM State
        FSMEnumState();

        DoorTwo();
    }

    protected void FSMEnumState()
    {
        if (DoorOpened == true) //door is open
        {
            doorState = DoorStates.Open;
        }
        else
        {
            doorState = DoorStates.Close;
        }
    }

    public void DoorTwo()
    {
        if (DoorOpened == true) //door is open
        {
            if (doorState == DoorStates.Open)
            {
                //AudioManager.Instance.PlaySound(SoundID.DoorOpen);
                print("Open Door");
                float deltaTime = Time.deltaTime;
                elapsedTime_ += deltaTime;

                pivot.rotation = Quaternion.Lerp(openRotation, openRotation, smooth * Time.deltaTime);
                if (elapsedTime_ >= HowLongWaitToOpenDoor  /* Must wait x seconds to open door. */ + TimeDoorOpen /* x seconds it stay open. */)
                {
                    //close the door
                    DoorOpened = false;

                    elapsedTime_ = 0;
                    print("Door Closing");
                }
            }
        }
        else
        {
            if (doorState == DoorStates.Close)
            {
                //AudioManager.Instance.PlaySound(SoundID.DoorClose);
                pivot.rotation = Quaternion.Lerp(defaultRotation, defaultRotation, smooth * Time.deltaTime);
            }
        }
    }

    public void SlideDoorOpen(bool DoorOpen)
    {
        DoorOpened = DoorOpen;
    }
}
