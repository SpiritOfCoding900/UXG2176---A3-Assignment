using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody rb_;
    private float moveSpeed = -5f;
    public bool isJumping = true;
    private float TurnRate = -180;

    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");//get the up/down or w/s key input, return a value from -1 to 1
        float horizontal = -Input.GetAxis("Horizontal");//get the left/right or a/d key input, return a value from -1 to 1

        //this is inside FixedUpdate, so we use Time.fixedDeltaTime
        float deltaTime = Time.fixedDeltaTime;


        //calculate the amount of rotation from the player horizontal axis, we are rotating on the y-axis
        Quaternion rotationDelta = Quaternion.Euler(0, TurnRate * horizontal * deltaTime, 0);


        //rb_.rotation is the current rotation of the rigidbody, we use * operator to combine the current rotation and the rotationDelta
        Quaternion newRotation = rb_.rotation * rotationDelta;


        rb_.rotation = newRotation;

        //we are calculating the forward direction of player, or the blueArrow;
        Vector3 forward = rb_.rotation * Vector3.forward;


        //calculate the amount of movement from the player vertical axis
        Vector3 moveDelta = forward * moveSpeed * -vertical * deltaTime;


        //we use + operator to add both vector3 together
        Vector3 newPos = rb_.position + moveDelta;


        //using = operator, we are changing the rigidbody position to newPos
        rb_.position = newPos;


        if (Input.GetButtonDown("Jump") && isJumping)
        {
            rb_.AddForce(new Vector3(0, 250, 0), ForceMode.Impulse);
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    }
}
