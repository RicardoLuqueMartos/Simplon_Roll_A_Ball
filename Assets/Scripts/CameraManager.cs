using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CameraManager : MonoBehaviour
{
    Player player;
    Transform playerTransform;

    #region Orbit
    public float distance = 2f;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    public float xMinLimit = -35;
    public float xMaxLimit = 35;

    float x = 0.0f;
    float y = 0.0f;

    float prevDistance;
    bool OrbitTarget = true;

    #endregion Orbit

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerTransform = player.transform;

        #region Orbit
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // hide mouse cursor 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        #endregion Orbit
    }

    // Update is called once per frame      
    void Update()
    {
     //   if (!player.Ended && player.Started)
    //    {
            CameraOrbit();
    //    }
    }

    private void CameraOrbit()
    {
        if (playerTransform && OrbitTarget)
        {
            // get mouse input and apply the mouse speed offset
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            // limit rotation of the camera
            x = ClampAngle(x, xMinLimit, xMaxLimit);
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            // apply rotation and position of the camera
            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0.0f, 0.0f, -distance) + playerTransform.transform.position;
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
        // limit the canera angle values to - and + 360 degrees
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        // return camera rotation depending on min and max values 
        return Mathf.Clamp(angle, min, max);
    }
}

