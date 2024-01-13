using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    #region variables

    [Header("variables")]
    public string floorTag;
    public float walkSpeed;
    public float runSpeed;
    public float sens;
    public float jumpForce;
    [Tooltip("the speed at wich the sprinting counter recharges")]
    public float runRechargeSpeed;
    [Tooltip("the speed at wich the sprinting counter decharges")]
    public float runDechargeSpeed;
    [Tooltip("the time after sprinting needed before recharging")]
    public float runRechargeInterval;
    [Tooltip("clamps the looking of up and down between the normal and - value of this float")]
    public float lookClamp;
    [Tooltip("This should be: playerHeight % 2 + 0.1. only change the height to something different if you know it has to be something different")]
    public float jumpCheckLength;

    [Header("refrences")]
    public Rigidbody rb;
    public GameObject cam;
    public Slider sprintSlider;
    public bool inUI;

    //privates
    RaycastHit hit;
    public float moveSpeed;
    bool timerEnabled;
    float timer;

    #endregion

    #region start and update etc

    public void Start()
    {
        moveSpeed = walkSpeed;
    }

    public void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public void Update()
    {
        Jump();
        Running();
    }

    #endregion

    #region moving code

    public void Move()
    {
        Vector3 input = new Vector3();

        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        input = transform.TransformDirection(input);
        rb.MovePosition(transform.position + input * moveSpeed * Time.deltaTime);
    }

    public void Rotate()
    {
        if (inUI)
        {
            return;
        }

        Vector3 mouseInput = new Vector3();

        mouseInput.y = Input.GetAxis("Mouse X");
        transform.Rotate(mouseInput * sens);

        mouseInput = Vector3.zero;

        mouseInput.x = -Input.GetAxis("Mouse Y");
        cam.transform.Rotate(mouseInput * sens);
    }

    public void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintSlider.value -= runDechargeSpeed * Time.deltaTime;

            if (sprintSlider.value > 0)
            {
                timerEnabled = false;
                timer = runRechargeInterval;
                moveSpeed = runSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            moveSpeed = walkSpeed;

            if (timer < 0)
            {
                timerEnabled = true;
            }

            if (timerEnabled)
            {
                sprintSlider.value += runRechargeSpeed * Time.deltaTime;
            }
        }
    }

    #endregion

    #region jumping code

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, jumpCheckLength))
            {
                Debug.DrawRay(transform.position, -transform.up, Color.red, 10);

                if (hit.transform.tag == floorTag)
                {
                    Vector3 jump = new Vector3(0, jumpForce, 0);

                    rb.AddRelativeForce(jump, ForceMode.Impulse);
                }
            }
        }
    }

    #endregion
}
