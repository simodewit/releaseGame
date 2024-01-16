using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    #region variables

    [Header("Wheel collider refrences")]
    [Tooltip("Left front wheel collider")]
    public WheelCollider colliderLF;
    [Tooltip("Right front wheel collider")]
    public WheelCollider colliderRF;
    [Tooltip("Left rear wheel collider")]
    public WheelCollider colliderLR;
    [Tooltip("Right rear wheel collider")]
    public WheelCollider colliderRR;

    [Header("Variables")]
    [Range(0, 90)][Tooltip("The maximum amount of turning that the wheels can turn")]
    public float maxTurning = 15;
    [Tooltip("The speed at witch the car turns")]
    public float turningStrength = 1;
    [Tooltip("The maximum speed at witch the car can travel")]
    public float topSpeed = 100;
    [Tooltip("The speed at witch the car accelerates")]
    public float accelerationSpeed = 1;
    [Range(0, 2)][Tooltip("The balance of the power output between the front and back wheels (1 = awd, 0 = rwd, 2 = fwd)")]
    public float torqueBalance = 1;
    [Tooltip("Place for the driver to sit")]
    public Transform driverPlace;

    //privates
    float turning;
    bool drives;

    #endregion

    #region update

    public void Update()
    {
        if (!drives)
        {
            return;
        }

        Speed();
        Braking();
        Rotation();
    }

    #endregion

    #region code

    public void Speed()
    {
        float input = Input.GetAxis("Vertical");

        if (input <= 0)
        {
            input = 0;
        }

        colliderLF.motorTorque = input * Time.deltaTime;
        colliderRF.motorTorque = input * Time.deltaTime;
        colliderLR.motorTorque = input * Time.deltaTime;
        colliderRR.motorTorque = input * Time.deltaTime;
    }

    public void Braking()
    {
        float input = Input.GetAxis("Vertical");

        if (input >= 0)
        {
            input = 0;
        }

        colliderLF.brakeTorque = input * Time.deltaTime;
        colliderRF.brakeTorque = input * Time.deltaTime;
        colliderLR.brakeTorque = input * Time.deltaTime;
        colliderRR.brakeTorque = input * Time.deltaTime;
    }

    public void Rotation()
    {
        float input = Input.GetAxis("Horizontal");
        turning += input * turningStrength * Time.deltaTime;

        Mathf.Clamp(turning, -maxTurning, maxTurning);

        colliderLF.gameObject.transform.Rotate(0, turning, 0);
        colliderRF.gameObject.transform.Rotate(0, turning, 0);
    }

    #endregion
}
