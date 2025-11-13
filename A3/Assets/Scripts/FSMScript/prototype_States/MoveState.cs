using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveState : IPrototypeState
{
    public float mouseSensitivity = 1000f;
    float xRotation = 0f;
    float yRotation = 0f;

    public MoveState(FSMProtoType machine) : base(machine)
    {

    }

    public override PrototypeStateType Type => PrototypeStateType.Navigate;

    public override void OnStateUpdate()
    {
        if (UIManager.Instance.HasOpened) return;

        Shoot();
        MouseLook();
        WalkingWASD();
    }

    //protected void CellosWay()
    //{
    //    if (Input.GetButton("Fire1"))
    //    {
    //        if (UIManager.Instance.HasOpened) return;

    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        if (Physics.Raycast(ray, out RaycastHit hit))
    //        {
    //            Machine.Agent.SetDestination(hit.point);
    //        }
    //    }
    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        if (UIManager.Instance.HasOpened) return;

    //        Machine.ChangeState(PrototypeStateType.Attack);
    //    }
    //}

    protected void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseX;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -35f, 35f);

        Machine.transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
    }

    protected void WalkingWASD()
    {
        if (UIManager.Instance.HasOpened) return;

        //FSMProtoType.horizontalInput = Input.GetAxisRaw("Horizontal");
        //FSMProtoType.verticalInput = Input.GetAxisRaw("Vertical");

        //// Calculate Movement Direction
        //Vector3 forward = Machine.playerBody.forward;
        //forward.y = 0;
        //forward.Normalize();
        //Machine.moveDirection = forward * FSMProtoType.verticalInput + Machine.playerBody.right * FSMProtoType.horizontalInput;

        //Machine._rb.AddForce(Machine.moveDirection.normalized * Machine.moveSpeed * 10f, ForceMode.Force);

        // Instant input, no smoothing
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Get forward/right from the player body, but stay flat on ground
        Vector3 forward = Machine.playerBody.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = Machine.playerBody.right;
        right.y = 0f;
        right.Normalize();

        // Raw movement direction from input
        Vector3 inputDir = forward * vertical + right * horizontal;

        // Keep existing vertical (Y) velocity for gravity/jump
        Vector3 vel = Machine._rb.linearVelocity;

        if (inputDir.sqrMagnitude > 0f)
        {
            // Normalize so diagonal isn't faster, then apply moveSpeed
            inputDir.Normalize();
            Vector3 desiredHorizontalVel = inputDir * Machine.moveSpeed;

            // SNAP to desired velocity on XZ
            Machine._rb.linearVelocity = new Vector3(
                desiredHorizontalVel.x,
                vel.y,
                desiredHorizontalVel.z
            );
        }
        else
        {
            // No input? Instantly stop horizontal movement
            Machine._rb.linearVelocity = new Vector3(
                0f,
                vel.y,
                0f
            );
        }
    }

    protected void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (UIManager.Instance.HasOpened) return;

            Machine.ChangeState(PrototypeStateType.Attack);
        }
    }
}
