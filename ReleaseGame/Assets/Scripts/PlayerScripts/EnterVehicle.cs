using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : MonoBehaviour
{
    public GameObject cam;
    public float range;
    public Movement movementScript;
    public Vector3 leaveOffset;
    
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                movementScript.canMove = true;

                movementScript.transform.parent = null;
                movementScript.transform.position += leaveOffset;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
                {
                    if (hit.transform.GetComponent<Vehicle>() != null)
                    {
                        movementScript.canMove = false;

                        movementScript.transform.parent = hit.transform.GetComponent<Vehicle>().driverPlace;
                        movementScript.transform.localPosition = Vector3.zero;
                    }
                }
            }
        }
    }
}
