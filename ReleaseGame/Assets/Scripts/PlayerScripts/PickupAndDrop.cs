using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAndDrop : MonoBehaviour
{
    #region variables

    [Header("variables")]
    public GameObject cam;
    public float distance;
    public Transform interactablePosition;
    public string interactablesTag;

    //privates
    bool hasItem;
    GameObject interactable;
    RaycastHit hit;

    #endregion

    #region update

    public void Update()
    {
        PickupAndDrops();
    }

    #endregion

    #region pickup and drop

    public void PickupAndDrops()
    {
        if (hasItem && Input.GetKeyDown(KeyCode.X))
        {
            interactable.GetComponent<Rigidbody>().useGravity = true;
            interactable.transform.parent = null;
            interactable = null;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
            {
                if (hit.transform.tag == interactablesTag)
                {
                    interactable = hit.transform.gameObject;
                    interactable.GetComponent<Rigidbody>().useGravity = false;
                    interactable.transform.SetParent(interactablePosition);
                    interactable.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    #endregion
}
