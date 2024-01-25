using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("The speed the cam lerps into a shop")]
    public float lerpSpeed;

    [Header("Refrences")]
    public Rigidbody rb;
    public Collider col;
    public Movement movementScript;
    public Camera cam;
    [Tooltip("The place to hold items")]
    public Transform holdPlace;
    public OptionsWheel optionsWheel;
    public GameObject shopPanel;

    //private variables
    states state;
    RaycastHit hit;
    GameObject lastInteractable;
    bool lerping;

    #endregion

    #region Update

    public void Update()
    {
        Switch();

        if (state == states.notInteracting)
        {
            optionsWheel.enabled = true;
        }
        else
        {
            optionsWheel.enabled = false;
        }
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
            lastInteractable.transform.localPosition = Vector3.zero;
            lastInteractable.transform.localRotation = Quaternion.identity;

            if (!Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            Drop(hit.transform);
        }
        else if (state == states.shop)
        {
            if (!Input.GetKeyDown(KeyCode.F))
            {
                return;
            }

            ExitShop(lastInteractable.GetComponent<Shop>());
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
                    EnterShop(hit.transform.GetComponent<Shop>());
                    break;
                case type.item:
                    Pickup(hit.transform);
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

        car.enabled = true;
        lastInteractable = car.gameObject;

        state = states.driving;
    }

    public void ExitCar(Vehicle car)
    {
        rb.useGravity = true;
        col.enabled = true;

        movementScript.enabled = true;
        movementScript.transform.parent = null;

        car.enabled = false;
        lastInteractable = null;

        state = states.notInteracting;
    }

    #endregion

    #region Pickup specific

    public void Pickup(Transform item)
    {
        item.parent = holdPlace;
        item.localPosition = Vector3.zero;

        item.GetComponent<Collider>().enabled = false;
        item.GetComponent<Rigidbody>().useGravity = false;

        state = states.carrying;
        lastInteractable = item.gameObject;
    }

    public void Drop(Transform item)
    {
        item.parent = null;

        item.GetComponent<Collider>().enabled = true;
        item.GetComponent<Rigidbody>().useGravity = true;

        state = states.notInteracting;
        lastInteractable = null;
    }

    #endregion

    #region Shop

    public void EnterShop(Shop shop)
    {
        movementScript.enabled = false;
        cam.transform.parent = null;

        shop.panel.SetActive(true);
    }

    public void ExitShop(Shop shop)
    {
        movementScript.enabled = true;
        cam.transform.parent = transform;

        shop.panel.SetActive(false);
    }

    #endregion
}
