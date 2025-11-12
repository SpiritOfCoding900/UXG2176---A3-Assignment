using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMovingPlatform : MonoBehaviour, ISwitchPlatform
{
    public static MyMovingPlatform Instance;

    public Transform target1; // First target position
    public Transform target2; // Second target position
    //public float speed = 1.0f; // Platform speed

    private Transform currentTarget; // Current target position

    public bool platformIsRight = false;


    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = target1; // Set current target to target1
    }

    // Update is called once per frame
    void Update()
    {
        TheMovingPlatform();
    }

    protected void TheMovingPlatform()
    {
        if (platformIsRight == false)
        {
            currentTarget = target1;
            transform.position = currentTarget.position;
        }

        if (platformIsRight == true)
        {
            currentTarget = target2;
            transform.position = currentTarget.position;
        }
    }

    public void SwitchPlatform(bool WhichSide)
    {
        platformIsRight = WhichSide;
    }
}
