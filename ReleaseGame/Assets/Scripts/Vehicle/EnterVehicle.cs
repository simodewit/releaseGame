using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : MonoBehaviour
{
    #region variables

    [Header("Variables")]
    [Tooltip("Offset that is added up to the players position when exiting the vehicle")]
    public Vector3 leaveOffset;
    [Tooltip("The range at witch you can enter the vehicle")]
    public float range;

    [Header("Refrences")]
    public GameObject cam;
    public Movement movementScript;
    public Rigidbody rb;
    public Collider col;
    public GameObject panelFIcon;

    //privates
    bool isDriving;
    RaycastHit hit;

    #endregion

    #region standard functions

    public void Update()
    {
        EnterTheVehicle();
    }

    #endregion

    #region enter vehicle code

    public void EnterTheVehicle()
    {
        if (isDriving)
        {
            transform.rotation = hit.transform.rotation;
            transform.localPosition = Vector3.zero;
            panelFIcon.SetActive(false);

            if (Input.GetKeyDown(KeyCode.F))
            {
                isDriving = false;
                rb.useGravity = true;
                col.enabled = true;

                movementScript.canMove = true;
                movementScript.transform.parent = null;
                movementScript.transform.position += leaveOffset;

                cam.transform.localRotation = Quaternion.identity;
                hit.transform.GetComponent<Vehicle>().driving = false;
            }
        }
        else
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                if (hit.transform.GetComponent<Vehicle>() != null)
                {
                    panelFIcon.SetActive(true);
                }
                else
                {
                    panelFIcon.SetActive(false);
                    return;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    isDriving = true;
                    rb.useGravity = false;
                    col.enabled = false;

                    movementScript.canMove = false;
                    transform.parent = hit.transform.GetComponent<Vehicle>().driverPlace;
                    transform.forward = hit.transform.forward;

                    cam.transform.localRotation = Quaternion.identity;
                    hit.transform.GetComponent<Vehicle>().driving = true;
                }
            }
            else
            {
                panelFIcon.SetActive(false);
            }
        }
    }

    #endregion
}
