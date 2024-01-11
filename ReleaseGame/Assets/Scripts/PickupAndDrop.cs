using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAndDrop : MonoBehaviour
{
    public GameObject cam;
    public float distance;
    public Transform gunPosition;

    bool hasItem;
    GameObject gun;
    RaycastHit hit;

    public void Update()
    {
        PickupAndDrops();
    }

    public void PickupAndDrops()
    {
        if (hasItem && Input.GetKeyDown(KeyCode.G))
        {
            gun.GetComponent<Rigidbody>().useGravity = true;
            gun.transform.parent = null;
            gun = null;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Gun>() != null)
                {
                    gun = hit.transform.gameObject;
                    gun.GetComponent<Rigidbody>().useGravity = false;
                    gun.transform.SetParent(cam.transform);
                    gun.transform.localPosition = gunPosition.transform.localPosition;
                }
            }
        }
    }
}
