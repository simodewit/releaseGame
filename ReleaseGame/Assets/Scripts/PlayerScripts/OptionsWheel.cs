using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsWheel : MonoBehaviour
{
    public GameObject optionsWheel;
    public Movement movementScript;
    public Transform placeToInstantiate;
    public List<OptionInWheel> options = new List<OptionInWheel>();
    public OptionInWheel optionInWheel;

    public void Update()
    {
        Options();
    }

    public void Options()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Cursor.lockState = CursorLockMode.None;
            movementScript.inUI = true;
            optionsWheel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            Cursor.lockState = CursorLockMode.Locked;
            movementScript.inUI = false;
            optionsWheel.SetActive(false);

            Switch();
        }
    }

    public void Switch()
    {
        if (placeToInstantiate.childCount > 0)
        {
            foreach (Transform t in placeToInstantiate.transform)
            {
                Destroy(t.gameObject);
            }
        }

        if (optionInWheel == null)
        {
            return;
        }

        if (optionInWheel.gunToInstantiate != null)
        {
            GameObject gun = Instantiate(optionInWheel.gunToInstantiate, placeToInstantiate);
            gun.transform.localPosition = Vector3.zero;
        }

    }
}
