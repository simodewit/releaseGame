using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsWheel : MonoBehaviour
{
    public GameObject optionsWheel;
    public Movement movementScript;
    public List<OptionInWheel> options = new List<OptionInWheel>();

    public void Update()
    {
        Options();
    }

    public void Options()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.None;
            movementScript.inUI = true;
            optionsWheel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.Locked;
            movementScript.inUI = false;
            optionsWheel.SetActive(false);
        }
    }
}
