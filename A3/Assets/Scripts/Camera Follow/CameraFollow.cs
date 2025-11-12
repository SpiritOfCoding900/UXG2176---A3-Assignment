using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Cainos.PixelArtTopDown_Basic
{
    //let camera follow target
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float lerpSpeed = 1.0f;
        public float mouseSensitivity = 200f;
        public float verticalClamp = 35f;

        private Vector3 offset;
        private Vector3 targetPos;
        private float verticalRotation = 0f;

        private void Start()
        {
            findPlayerTarget(GameManager.Instance.CurrentPlayer.transform);
            offset = transform.position - target.position;
        }


        public void findPlayerTarget(Transform playerLocation)
        {
            target = playerLocation;
        }


        private void Update()
        {
            if (UIManager.Instance.HasOpened) return;
            if (target == null) return;

            // --- Mouse Y controls vertical rotation ---
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);

            // Base camera position
            Vector3 desiredPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, lerpSpeed * Time.deltaTime);

            // Apply vertical tilt only
            Quaternion verticalTilt = Quaternion.Euler(verticalRotation, target.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, verticalTilt, Time.deltaTime * lerpSpeed);
        }
    }
}
