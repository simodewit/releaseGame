using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public enum states
{
    notInteracting,
    driving,
    carrying,
    shop
}

public class Interactor : MonoBehaviour
{
    #region Variables

    [Header("Variables")]
    [Tooltip("The range that the interactor will check for a interactable")]
    public float range;
    //public Vector3 leaveOffset;

    [Header("Refrences")]
    public Rigidbody rb;
    public Collider col;
    public Movement movementScript;
    public Camera cam;

    //private variables
    states state;
    RaycastHit hit;
    GameObject lastInteractable;

    #endregion

    #region Update

    public void Update()
    {
        Switch();
    }

    #endregion

    #region Switch

    public void Switch()
    {
        if (state == states.notInteracting)
        {
            if (!Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            Check();
        }
        else if (state == states.driving)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            cam.transform.localRotation = Quaternion.identity;

            if (!Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            ExitCar(lastInteractable.GetComponent<Vehicle>());
        }
        else if (state == states.carrying)
        {
            if (!Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            //drop code
        }
        else if (state == states.shop)
        {
            if (!Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            //exit shop code
        }
    }

    #endregion

    #region Check

    public void Check()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (!hit.transform.GetComponent<Interactable>())
            {
                return;
            }

            Interactable inter = hit.transform.GetComponent<Interactable>();

            switch (inter.type)
            {
                case type.car:
                    EnterCar(hit.transform.GetComponent<Vehicle>());
                    break;
                case type.shop:
                    //shop enter code
                    break;
                case type.item:
                    //pickup code
                    break;
            }
        }
    }

    #endregion

    #region Car specific

    public void EnterCar(Vehicle car)
    {
        rb.useGravity = false;
        col.enabled = false;

        movementScript.enabled = false;
        movementScript.transform.parent = car.seat1;

        car.isDriving = true;
        lastInteractable = car.gameObject;

        state = states.driving;
    }

    public void ExitCar(Vehicle car)
    {
        rb.useGravity = true;
        col.enabled = true;

        movementScript.enabled = true;
        movementScript.transform.parent = null;

        car.isDriving = false;
        lastInteractable = null;

        state = states.notInteracting;
    }

    #endregion

    #region Pickup specific

    public void Pickup()
    {

    }

    public void Drop()
    {

    }

    #endregion
}
