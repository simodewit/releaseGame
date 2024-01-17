using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : MonoBehaviour
{
    public GameObject cam;
    public float range;
    public Movement movementScript;
    public Vector3 leaveOffset;
    public Rigidbody rb;
    public Collider col;
    public GameObject panelFIcon;

    bool isDriving;
    RaycastHit hit;

    public void Update()
    {
        EnterTheVehicle();
    }

    public void EnterTheVehicle()
    {
        if (isDriving)
        {
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

                hit.transform.GetComponent<Vehicle>().drives = false;
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
                    
                    hit.transform.GetComponent<Vehicle>().drives = true;
                }
            }
            else
            {
                panelFIcon.SetActive(false);
            }
        }
    }
}
