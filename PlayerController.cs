using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 7f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;

    [Header("Screen position based values")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Control throw based values")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = 5f;

    float xThrow, yThrow;

    bool isControlEnabled = true;

    // Use this for initialization
    void Start ()
    {
        
	}

   
    // Update is called once per frame
    void Update ()
    {
        if (isControlEnabled == true)
        {
            ProcessTranslation(); //Movement
            ProcessRotation(); //Rotation
            ProcessFiring();
        }
    }

    private void ProcessRotation()
    {
        SetRotationValues();
    }

    private void SetRotationValues()
    {
        float pitch = ProcessPitch();
        float yaw = ProcessYaw();
        float roll = ProcessRoll();
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private float ProcessRoll()
    {
        float RollDueToControlThrow = transform.localPosition.x * controlRollFactor;
        float roll = RollDueToControlThrow;
        return roll;
    }

    private float ProcessYaw()
    {
        float YawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yaw = YawDueToPosition;
        return yaw;
    }

    private float ProcessPitch()
    {
        float pitchDueToControlThrow = yThrow * controlPitchFactor; //rotation changes due to yThrow (if buttons are pressed)
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor; //rotation in x (pitch) changes due to y-location
        float pitch = pitchDueToControlThrow + pitchDueToPosition;
        return pitch;
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z); // sets the transform to only xAxis
    }

    void OnPlayerDeath() //called by other script
    {
        isControlEnabled = false;
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }

        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {

        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
   

