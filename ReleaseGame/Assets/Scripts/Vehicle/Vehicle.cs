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

    [Header("Wheel mesh refrences")]
    [Tooltip("Left front wheel mesh")]
    public GameObject meshLF;
    [Tooltip("Right front wheel mesh")]
    public GameObject meshRF;
    [Tooltip("Left rear wheel mesh")]
    public GameObject meshLR;
    [Tooltip("Right rear wheel mesh")]
    public GameObject meshRR;

    [Header("Variables")]
    [Range(0, 90)][Tooltip("The maximum amount of turning that the wheels can turn")]
    public float maxTurning = 5;
    [Tooltip("The speed at witch the car turns")]
    public float turningStrength = 1;
    [Tooltip("the strength of snapping back to driving straight")]
    public float snapBackStrength = 1;
    [Tooltip("The maximum speed at witch the car can travel")]
    public float topSpeed = 100;
    [Tooltip("The speed at witch the car accelerates")]
    public float accelerationSpeed = 1;
    [Tooltip("The speed at witch the car accelerates backwards")]
    public float rearAccelerationSpeed = 1;
    [Tooltip("The speed at witch the car deccelerates")]
    public float brakeForce = 1;
    [Tooltip("The balance of the power output between the front and back wheels")]
    public torqueBalance balanceOfTorque;
    [Tooltip("The balance of the brakes")]
    public brakeBalance balanceOfBrakes;
    [Tooltip("Place for the driver to sit")]
    public Transform driverPlace;
    [Tooltip("If true the car is driven")]
    public bool driving;
    public Rigidbody rb;

    //privates
    float turning;

    public enum torqueBalance
    {
        RearWheelDrive,
        AllWheelDrive,
        FrontWheelDrive
    }

    public enum brakeBalance
    {
        RearWheels,
        AllWheels,
        FrontWheels
    }

    #endregion

    #region update

    public void Update()
    {
        if (!driving)
        {
            return;
        }

        Driving();
        Turning();
    }

    #endregion

    #region driving

    public void Driving()
    {
        float drivingInput = Input.GetAxis("Vertical");

        if (drivingInput > 0)
        {
            if (balanceOfTorque == torqueBalance.FrontWheelDrive)
            {
                colliderLF.motorTorque = drivingInput * accelerationSpeed;
                colliderRF.motorTorque = drivingInput * accelerationSpeed;
            }
            if (balanceOfTorque == torqueBalance.AllWheelDrive)
            {
                colliderLF.motorTorque = drivingInput * accelerationSpeed * .5f;
                colliderRF.motorTorque = drivingInput * accelerationSpeed * .5f;
                colliderLR.motorTorque = drivingInput * accelerationSpeed * .5f;
                colliderRR.motorTorque = drivingInput * accelerationSpeed * .5f;
            }
            if (balanceOfTorque == torqueBalance.RearWheelDrive)
            {
                colliderLR.motorTorque = drivingInput * accelerationSpeed;
                colliderRR.motorTorque = drivingInput * accelerationSpeed;
            }
        }
        if (drivingInput < 0)
        {
            if (balanceOfTorque == torqueBalance.FrontWheelDrive)
            {
                colliderLF.motorTorque = drivingInput * rearAccelerationSpeed;
                colliderRF.motorTorque = drivingInput * rearAccelerationSpeed;
            }
            if (balanceOfTorque == torqueBalance.AllWheelDrive)
            {
                colliderLF.motorTorque = drivingInput * rearAccelerationSpeed * .5f;
                colliderRF.motorTorque = drivingInput * rearAccelerationSpeed * .5f;
                colliderLR.motorTorque = drivingInput * rearAccelerationSpeed * .5f;
                colliderRR.motorTorque = drivingInput * rearAccelerationSpeed * .5f;
            }
            if (balanceOfTorque == torqueBalance.RearWheelDrive)
            {
                colliderLR.motorTorque = drivingInput * rearAccelerationSpeed;
                colliderRR.motorTorque = drivingInput * rearAccelerationSpeed;
            }
        }
    }

    #endregion

    #region turning

    public void Turning()
    {
        if (!driving)
        {
            return;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            float steeringInput = Input.GetAxis("Horizontal");
            turning += steeringInput * turningStrength * Time.deltaTime;

            turning = Mathf.Clamp(turning, -maxTurning, maxTurning);
        }
        else
        {
            if (turning > 0)
            {
                turning -= snapBackStrength * Time.deltaTime;
            }
            else
            {
                turning += snapBackStrength * Time.deltaTime;
            }
        }

        colliderLF.steerAngle = turning;
        colliderRF.steerAngle = turning;

        meshLF.transform.rotation = new Quaternion(0, turning, 0, 0);
        meshRF.transform.rotation = new Quaternion(0, turning, 0, 0);
    }

    #endregion
}
