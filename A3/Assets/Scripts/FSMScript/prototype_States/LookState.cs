using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookState : IPrototypeState
{
    public float mouseSensitivity = 100f;

    public LookState(FSMProtoType machine) : base(machine)
    {
    }

    public override PrototypeStateType Type => PrototypeStateType.Looking;

    public override void OnStateUpdate()
    {
        MouseLook();
    }

    protected void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Machine.playerBody.Rotate(Vector3.up * mouseX);
    }
}
