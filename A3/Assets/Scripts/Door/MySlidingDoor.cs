using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySlidingDoor : MonoBehaviour, ISlideDoor
{
    public enum DoorStates
    {
        Open,
        Close,
    }

    public DoorStates doorState;

    float HowLongWaitToOpenDoor = 0.5f;
    float TimeDoorOpen = 0.5f;
    public float DoorSpeed = 10;
    public bool DoorOpened = false;

    public Transform DoorOpen;
    public Transform DoorClose;

    float elapsedTime_;

    // Start is called before the first frame update
    void Start()
    {
        /* Where you are. */
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
            if(doorState == DoorStates.Open)
            {
                //AudioManager.Instance.PlaySound(SoundID.DoorSlideOpen);
                print("Open Door");
                float deltaTime = Time.deltaTime;
                elapsedTime_ += deltaTime;

                transform.position = Vector3.MoveTowards(transform.position/* Where you are. */, DoorOpen.position/* Where you want to go. */, DoorSpeed/* Number of Speed Declared. */ * deltaTime/* Time.deltaTime. */); //animated
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
                //AudioManager.Instance.PlaySound(SoundID.DoorSlideOpen);
                transform.position = Vector3.MoveTowards(transform.position, DoorClose.position, DoorSpeed * Time.deltaTime); //animated
            }
        }
    }

    public void SlideDoorOpen(bool DoorOpen)
    {
        DoorOpened = DoorOpen;
    }
}
