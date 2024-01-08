using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    #region variables

    [Header("variables")]
    public float moveSpeed;
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

    [Header("refrences")]
    public Rigidbody rb;
    public GameObject cam;
    public Slider sprintSlider;

    //privates
    bool isGrounded;
    float normalSpeed;
    bool timerEnabled;
    float timer;

    #endregion

    #region start and update etc

    public void Start()
    {
        normalSpeed = moveSpeed;
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

        rb.AddRelativeForce(input * moveSpeed, ForceMode.Impulse);
    }

    public void Rotate()
    {
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
        }
        else
        {
            timer -= Time.deltaTime;
            moveSpeed = normalSpeed;

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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector3 jump = new Vector3(0, jumpForce, 0);

            rb.AddForce(jump, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.material != null)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.material != null)
        {
            isGrounded = false;
        }
    }

    #endregion
}
